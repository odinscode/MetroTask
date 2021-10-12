using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Models
{
    /// <summary>
    /// Represent the stock of vending machine for specified product.
    /// </summary>
    public class Stock
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Stock of {Product} with quantity of '{Quantity}'";
        }
    }
}
