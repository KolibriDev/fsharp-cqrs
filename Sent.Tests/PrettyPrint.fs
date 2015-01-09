[<AutoOpen>]
module PrettyPrint
open Sent.Shipper
  
let gameId = function ShipmentId id -> id 
let printEvent (event: Event) =
    match event with
    | ShipmentRequested e -> sprintf "Shipment %d requested" (gameId e.ShipmentId)

let printCommand (command: Command) =
    match command with
    | ShipmentRequest e -> sprintf "Shipment request %d" (gameId e.Id)

let printGiven events =
    printfn "Given"
    events 
    |> List.map printEvent
    |> List.iter (printfn "\t%s")
   
let printWhen command =
    printfn "When"
    command |> printCommand  |> printfn "\t%s"

let printExpect events =
    printfn "Expect"
    events 
    |> List.map printEvent
    |> List.iter (printfn "\t%s")

let printExpectThrows ex =
    printfn "Expect"
    printfn "\t%A" ex    