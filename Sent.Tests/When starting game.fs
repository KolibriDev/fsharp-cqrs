module Shipper.Tests.``When requesting shipment``

open Xunit
open System
open Specifications
open Sent.Shipper

[<Fact>]
let ``Requesting shippment sends the event``() =
    let sender = { 
            StreetNr = "asdasdads"
            PostalCode = 101
            City = "Rvk"}
    let receiver = { 
            StreetNr = "asdasdads"
            PostalCode = 101
            City = "Rvk"}
    Given []
    |> When ( ShipmentRequest{ 
                Id = ShipmentId 1
                From = sender
                To = receiver
                ShipmentDescription = "description"
                ShipmentWeight = None
                ShipmentWidth = None
                ShipmentHeight = None
                ShipmentDepth = None
                })
    |> Expect [ ShipmentRequested{ ShipmentId = ShipmentId 1} ]
