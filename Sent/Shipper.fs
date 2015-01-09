

module Sent.Shipper
open System

type ShipmentId = ShipmentId of int

type Address = {
    StreetNr: string
    PostalCode : int
    City: string}


/// Commands for the game discard pile
type Command =
    | ShipmentRequest of ShipmentRequest

and ShipmentRequest = {
    Id: ShipmentId
    From: Address
    To: Address 
    ShipmentDescription : string
    ShipmentWeight: Option<float>
    ShipmentHeight: Option<float>
    ShipmentWidth: Option<float>
    ShipmentDepth: Option<float> }

/// Events for the game discard pile
type Event =
    | ShipmentRequested of ShipmentRequested 
and ShipmentRequested = {
    ShipmentId: ShipmentId}

type State = {
        hasOffers : bool
    }
    with
    static member initial = {
        hasOffers = false
        }


let wantToShip (cmd : ShipmentRequest) state = 
    [ ShipmentRequested { ShipmentId = cmd.Id} ]
let handle =
    function
    | ShipmentRequest command -> wantToShip command 

// Applies state changes for events

let evolve state =
    function
    | ShipmentRequested e -> 
        {hasOffers = true}



