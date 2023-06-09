﻿using Crafts.DAL.Models;
using Crafts.DAL.Repos.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafts.DAL.Repos.CartItemsRepo
{
    public interface ICartItemRepo :IGenericRepo<CartItem>
    {
        void DeleteAllCartItemsByCartId(int cartId);
        CartItem GetByCartIdAndProductId(int cartId, int productId);

    }
}
