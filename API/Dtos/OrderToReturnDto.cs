using System;
using System.Collections.Generic;

namespace API.DTOs;

public class OrderToReturnDTO
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public AddressDTO ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}
