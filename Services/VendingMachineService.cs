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
        internal static SortedDictionary<double, int> moneyForChange = new SortedDictionary<double, int>();
        internal static double moneyForChangeTotal = 0;
        internal static double depositMoney = 0;

        static VendingMachineService()
        {
            var rng = new Random();
            var denominations = new double[] { 0.01, 0.05, 0.1, 0.5, 1, 2, 5, 10, 50, 100, 200, 500, 1000, 2000, 5000 };

            foreach (var nominal in denominations.Reverse())
            {
                moneyForChange.Add(nominal, rng.Next(5, 10));
            }

            CalculateTotalChangeAmountAvailable();

            stocks = new Dictionary<string, Stock>()
            {
                { "beer", new Stock { Product = new Product { Name = "beer", Price = 40.5 }, Quantity = 2 } },
                { "condoms", new Stock { Product = new Product { Name = "condoms", Price = 200.3 }, Quantity = 2 }},
                { "cigarettes", new Stock { Product = new Product { Name = "cigarettes", Price = 150.43 }, Quantity = 2 } }
            };
        }

        static void CalculateTotalChangeAmountAvailable()
        {
            moneyForChangeTotal = 0;

            foreach (var item in moneyForChange)
            {
                moneyForChangeTotal += item.Key * item.Value;
            }
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

        public string GiveChange()
        {
            if (depositMoney == 0)
            {
                return "Current deposit money is zero.";
            }

            if (moneyForChangeTotal < depositMoney)
            {
                return $"Sorry we can not provide change for your current deposit {depositMoney}. Please purchase something";
            }

            var remainder = depositMoney;
            var currentDenominationIndex = 0;

            foreach (var denomination in moneyForChange)
            {
                // If banknote of current denomination is ended in VM.
                if (denomination.Value < 1)
                {
                    continue;
                }
                
            }

            if (remainder > 0)
            {
                return $"Sorry we can not provide change for your current deposit {depositMoney}. Please purchase something";
            }

            return string.Empty;
        }
    }
}
