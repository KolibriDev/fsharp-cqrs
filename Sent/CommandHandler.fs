namespace Sent

open Sent.Shipper
// The discard pile command handler is the link
// between the command, the event store and the aggregate
// This version loads the aggregate from scratch for each command
// This is usually ok for aggregates with a small number of events
module Shippers =
    let shipmentId =
        function
        | ShipmentRequest {Id = ShipmentId id } -> id


    let create readStream appendToStream =

        // this is the "repository"
        let streamId id = sprintf "ShipmentRequest-%d" id 
        let load shipmentId =
            let rec fold state version =
                let events, lastEvent, nextEvent = readStream (streamId shipmentId) version 500
                let state = List.fold evolve state events
                match nextEvent with
                | None -> lastEvent, state
                | Some n -> fold state n
            fold State.initial 0

        let save id expectedVersion events = appendToStream (streamId id) expectedVersion events

        // the mapsnd function works on a pair.
        // It applies the function on the second element.
        let inline mapsnd f (v,s) = v, f s    

        fun command ->
            let id = shipmentId command

            load id
            |> mapsnd (handle command)
            ||> save id

