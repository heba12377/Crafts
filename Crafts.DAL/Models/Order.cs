﻿using Crafts.DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafts.DAL.Models;

public class Order
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public double TotalPrice { get; set; }
    public double TaxPrice { get; set; }
    public double ShippingPrice { get; set; }
    public DateTime PaidAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsPaid { get; set; }

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

    public int CartId { get; set; }
    public Cart? Cart { get; set; }
}