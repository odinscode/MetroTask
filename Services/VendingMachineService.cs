using MetroTaskV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Services
{
    /// <summary>
    /// Implementation of IVendingMachineService.
    /// </summary>
    public class VendingMachineService : IVendingMachineService
    {
        internal static IDictionary<string, Stock> stocks = new Dictionary<string, Stock>();
        internal static double depositMoney = 0;

        static VendingMachineService()
        {
            stocks = new Dictionary<string, Stock>()
            {
                { "beer", new Stock { Product = new Product { Name = "beer", Price = 40.5 }, Quantity = 2 } },
                { "condoms", new Stock { Product = new Product { Name = "condoms", Price = 200.3 }, Quantity = 2 }},
                { "cigarettes", new Stock { Product = new Product { Name = "cigarettes", Price = 150.43 }, Quantity = 2 } }
            };
        }

        public IEnumerable<Stock> GetAllAvailableProductsInStock()
        {
            return stocks.Values;
        }

        public double DepositMoney(double banknote)
        {
            depositMoney += banknote;

            return depositMoney;
        }

        public string Buy(string productName)
        {
            if (!stocks.ContainsKey(productName))
            {
                return $"Product '{productName}' is not exist.";
            }

            if (stocks[productName].Quantity <= 0)
            {
                return $"Product '{productName}' is out of stock.";
            }

            var remainingFunds = depositMoney - stocks[productName].Product.Price;

            if (remainingFunds < 0)
            {
                return $"Insufficient deposited money! Please deposit at least {-remainingFunds} for purchasing '{productName}'";
            }
            
            stocks[productName].Quantity -= 1;
            depositMoney = remainingFunds;

            return $"Successful purchased '{productName}'! Remaining funds {depositMoney}";
        }
    }
}
