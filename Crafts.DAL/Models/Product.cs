﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafts.DAL.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    public float Rating { get; set; }
    public string Image { get; set; } = string.Empty;
  //  public IFormFile? Image { get; set; } 
    public int Quantity { get; set; }
    public bool IsSale { get; set; }
    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    public int? WishlistId { get; set; }
    public Wishlist? Wishlist { get; set; }

}
