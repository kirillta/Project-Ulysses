module MapParser


open System
open System.Net.Http
open System.Text.RegularExpressions
open ParseOptions
open Pub


type MapParser(options: ParseOptions, httpClient: HttpClient) = 
    let trimPubListString (opts: ParseOptions) (context: string) = 
        let s = context.Substring(context.IndexOf(opts.StartIndex) + opts.StartIndex.Length)
        s.Substring(0, s.IndexOf(opts.EndIndex))
            .Replace("\\n", String.Empty)


    let getPubListAsString (opts: ParseOptions) (client: HttpClient) = 
        Async.AwaitTask (client.GetStringAsync(opts.Url))
        |> Async.RunSynchronously
        |> trimPubListString opts


    let rec getTypeTags (acc: List<string>) count (matches: MatchCollection) =
        match count with 
        | 0 -> acc
        | _ -> 
            let item = matches.Item (count - 1)
            let acc' = 
                match item.Value = acc.Head with 
                | true -> acc
                | false -> [item.Value] @ acc

            getTypeTags acc' (count - 1) matches


    let rec getPubTypes acc (source: string) (tags: List<string>) = 
        match String.IsNullOrEmpty tags.Tail.Head with 
        | true -> 
            let source' = source.Substring(source.IndexOf(tags.Head) - 4)
            acc @ [source']
        | false -> 
            let source' = source.Substring(source.IndexOf(tags.Head) - 4)
            let acc' = acc @ [source'.Substring(0, source'.IndexOf(tags.Tail.Head) - 5)]
            getPubTypes acc' source' tags.Tail


    let getPubType (source: string) = 
        let s = source.Substring(34)
        s.Substring(0, s.IndexOf('\\'))



    let getEnglishTitle source =
        let m = Regex.Match(source, @"Name EN\S{6}([^\\]*)")
        match m.Success with 
        | true -> m.Groups.[1].Value
        | false -> String.Empty


    let getPostalCode source = 
        let m = Regex.Match(source, @"Индекс\S{6}(\d{6})")
        match m.Success with 
        | true -> m.Groups.[1].Value
        | false -> String.Empty


    let getAddress source = 
        let m = Regex.Match(source, @"Адрес\S{6}([^\\]*)")
        match m.Success with 
        | true -> m.Groups.[1].Value
        | false -> String.Empty


    let getDistrict source = 
        let m = Regex.Match(source, @"Район\S{6}([^\\]*)")
        match m.Success with 
        | true -> m.Groups.[1].Value
        | false -> String.Empty


    let getCoordinates source = 
        match Regex.IsMatch(source, @"Координаты\S{6}(\d{2}.\d{1,8}),\s?(\d{2}.\d{1,8})") with
        | true ->
            let m = Regex.Match (source, @"Координаты\S{6}(\d{2}.\d{1,8}),\s?(\d{2}.\d{1,8})")
            (m.Groups.[1].Value, m.Groups.[2].Value)
        | false -> (String.Empty, String.Empty)


    let rec filtrDigits acc index (source: char[]) = 
        match index < source.Length with
        | false -> acc 
        | true -> 
            match Regex.IsMatch((source.[index].ToString()), @"\d") with 
            | false -> filtrDigits acc (index + 1) source
            | true -> 
                let acc' = acc @ [source.[index]]
                filtrDigits acc' (index + 1) source


    let getPhone source = 
        let m = Regex.Match(source, @"Телефон\S{6}([^\\]*)")
        match m.Success with 
        | true -> 
            m.Groups.[1].Value.ToCharArray()
            |> filtrDigits [] 0
        | false -> []


    let getWebSite source = 
        let m = Regex.Match(source, @"WWW\S{6}([^\\]*)")
        match m.Success with 
        | true -> m.Groups.[1].Value
        | false -> String.Empty


    let getPubFromString (opts: ParseOptions) (pubType: string) (source: string) = 
        let title = source.Substring(0, source.IndexOf('\\'))
        let enTitle = getEnglishTitle source
        let long, lat = getCoordinates source
        let code = getPostalCode source
        let address = getAddress source
        let district = getDistrict source
        let site = getWebSite source
        let phone = 
            getPhone source
            |> List.toArray
            |> System.String

        {
            Title = title; 
            EnglishTitle = enTitle; 
            Type = pubType; 
            Latitude = lat; 
            Longitude = long; 
            Address = address;
            District = district;
            PostalCode = code;
            Phone = phone;
            WebSite = site;
        }


    let getPubs (opts: ParseOptions) (pubsOfOneType: string) = 
        let split = pubsOfOneType.Split([|opts.SubtypeDivider|], StringSplitOptions.None)
        let pubType = getPubType split.[0]

        split
        |> Array.skip 1
        |> Array.map (getPubFromString opts pubType)
        |> List.ofArray


    let getPubsByTypeStringList (opts: ParseOptions) (pubString: string) = 
        let matches = Regex.Matches(pubString, opts.TypeDivider + @"[^\\]*")
        getTypeTags [String.Empty] matches.Count matches
        |> getPubTypes [] pubString


    let getPubList opts client = 
        getPubListAsString opts client
        |> getPubsByTypeStringList opts
        |> List.collect (getPubs opts)


    member this.GetPubs = 
        getPubList options httpClient