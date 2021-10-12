using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Models
{
    /// <summary>
    /// Defenition of product type in vending machine.
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"product: '{Name}' with price of '{Price}'";
        }
    }
}
