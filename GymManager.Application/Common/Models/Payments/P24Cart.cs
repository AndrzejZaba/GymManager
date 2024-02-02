﻿

namespace GymManager.Application.Common.Models.Payments;

public class P24Cart
{
    public string SellerId { get; set; }
    public string SellerCategory { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int Number { get; set; }
}
