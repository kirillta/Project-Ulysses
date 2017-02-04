module DataComparer

open System
open System.Net.Http
open System.Globalization
open ParseOptions
open MapParser
open FSharp.Data.Sql
open Pub



type Sql = SqlDataProvider<DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionStringName = "Nagoya">


let insertPubs (context: Sql.dataContext) (pubs: Pub array) = 
    pubs 
    |> Array.map (fun p -> 
            let row = context.Dbo.Pubs.Create()
            row.Title <- p.Title
            row.EnglishTitle <- p.EnglishTitle
            row.Type <- p.Type
            row.Latitude <- p.Latitude.ToString(CultureInfo.InvariantCulture)
            row.Longitude <- p.Longitude.ToString(CultureInfo.InvariantCulture)
            row.Address <- p.Address
            row.District <- p.District
            row.PostalCode <- p.PostalCode
            row.Phone <- p.Phone
            row.WebSite <- p.WebSite
            row.IsActive <- true
        ) |> ignore
    ()


let rec comparePubLists (context: Sql.dataContext) (pubs: Pub array) (dbPubs: Sql.dataContext.``dbo.PubsEntity`` list) = 
    match dbPubs with 
    | [] -> pubs
    | head::tail -> 
        let index = pubs |> Array.tryFindIndex (fun p -> p.Title = head.Title)
        match index with 
        | None -> 
            head.IsActive <- false
            comparePubLists context pubs tail
        | Some ix -> 
            let p = pubs.[ix]
            head.EnglishTitle <- p.EnglishTitle
            head.Type <- p.Type
            head.Latitude <- p.Latitude.ToString(CultureInfo.InvariantCulture)
            head.Longitude <- p.Longitude.ToString(CultureInfo.InvariantCulture)
            head.Address <- p.Address
            head.District <- p.District
            head.PostalCode <- p.PostalCode
            head.Phone <- p.Phone
            head.WebSite <- p.WebSite

            let pubs' = 
                match ix with 
                | 0 -> pubs.[1..]
                | _ -> Array.append pubs.[..ix - 1] pubs.[ix + 1..]
            
            comparePubLists context pubs' tail


let updatePubListInDb (pubs: Pub array) = 
    let context = Sql.GetDataContext()
    let registredPubs = context.Dbo.Pubs |> Seq.toList

    let newPubs = 
        match registredPubs with 
        | [] -> 
            match pubs with 
            | [||] -> [||]
            | _ -> 
                insertPubs context pubs
                [||]
        | _ -> comparePubLists context pubs registredPubs

    match newPubs with 
    | [||] -> ()
    | _ -> insertPubs context pubs

    context.SubmitUpdates()