using MetroTaskV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        internal static double depositMoney = 123.45;

        static VendingMachineService()
        {
            var denominations = new double[] { 5000, 2000, 1000, 500, 200, 100, 50, 10, 5, 2, 1, 0.5, 0.1, 0.05, 0.01 };

            foreach (var nominal in denominations)
            {
                moneyForChange.Add(nominal, 100);
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
            // TO DO: add a validation of denomination of incoming banknote
            // because customer can add only one banknote at time.
            // Increase a banknote quantity in "moneyForChange" correspondingly of each denomination
            // depending on incoming banknote denomination and execute "CalculateTotalChangeAmountAvailable()"

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

            var changeToGive = depositMoney;
            Dictionary<double, int> banknoteForChange = new Dictionary<double, int>();

            foreach (var denomination in moneyForChange.Reverse())
            {
                if (changeToGive < 0.01)
                {
                    break;
                }
                // If denomination of banknote is greater than remainder of change 
                // which should be paid - skip
                // example: 5000 (denomination of banknote) > 43 (change to pay) -> skip
                if (denomination.Key > changeToGive)
                {
                    continue;
                }

                // If banknote of current denomination is ended in VM.
                if (denomination.Value < 1)
                {
                    continue;
                }

                var remainderOfBanknoteDenominationDivision = (int)(changeToGive / denomination.Key);
                if (remainderOfBanknoteDenominationDivision > denomination.Value)
                {
                    banknoteForChange.Add(denomination.Key, denomination.Value);
                    changeToGive -= denomination.Key * denomination.Value;
                }
                else
                {
                    banknoteForChange.Add(denomination.Key, remainderOfBanknoteDenominationDivision);
                    changeToGive -= denomination.Key * remainderOfBanknoteDenominationDivision;
                }
            }

            // we can give change
            // update correspoding values in our banknote dictionary
            // return info
            if (changeToGive < 0.01)
            {
                var result = banknoteForChange
                    .Select(_ => $"{_.Key} Ruble: {_.Value}")
                    .Aggregate((a, b) => a + ", " + b);

                return "Your change: " + result;
            }

            if (changeToGive > 0.01)
            {
                return $"Sorry we can not provide change for your current deposit {depositMoney}. Please purchase something";
            }

            return "Something went wrong!";
        }
    }
}
