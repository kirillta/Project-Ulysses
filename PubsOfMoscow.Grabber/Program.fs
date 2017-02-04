open System
open System.Net.Http
open ParseOptions
open MapParser
open FSharp.Data.Sql
open Pub


type Sql = SqlDataProvider<DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionStringName = "Nagoya">


//let rec insertPubs (registredPubs: List<PubsEntity>) (pubs: List<Pub>) = 
//    match pubs with 
//    | [] -> ()
//    | head::tail -> 
//        ()
//    ()


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let opts = ParseOptions()
    use httpClient = new HttpClient()
    let parser = new MapParser(opts, httpClient)
    
    let pubs = parser.GetPubs

    let context = Sql.GetDataContext()
    let registredPubs = context.Dbo.Pubs

    match (registredPubs |> Seq.toList) with 
    | [] -> 
        match pubs with 
        | [] -> ()
        | _ -> 
            pubs 
            |> List.map (fun p -> 
                let row = registredPubs.Create()
                row.Title <- p.Title
                row.EnglishTitle <- p.EnglishTitle
                row.Type <- 0
                row.Latitude <- p.Latitude
                row.Longitude <- p.Longitude
                row.Address <- p.Address
                row.District <- p.District
                row.PostalCode <- p.PostalCode
                row.Phone <- p.Phone
                row.WebSite <- p.WebSite
                row.IsActive <- true)
            |> ignore
    | head::tail -> ()

    Console.ReadLine() |> ignore
    0