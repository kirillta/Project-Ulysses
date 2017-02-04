open System
open ParseOptions
open MapParser
open DataComparer



[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let opts = ParseOptions()
    
    getPubList opts
    |> List.toArray
    |> updatePubListInDb

    Console.ReadLine() |> ignore
    0