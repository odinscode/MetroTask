using MetroTaskV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Services
{
    /// <summary>
    /// Contract for main BL of vending machine.
    /// </summary>
    public interface IVendingMachineService
    {
        /// <summary>
        /// Get all available products loaded into vending machine.
        /// </summary>
        /// <returns>Available products in stock.</returns>
        IEnumerable<Stock> GetAllAvailableProductsInStock();

        /// <summary>
        /// Deposit money into machine.
        /// </summary>
        /// <param name="banknote">Amount of money to deposit.</param>
        /// <returns>Current deposit money amount.</returns>
        double DepositMoney(double banknote);

        /// <summary>
        /// Purchase selected product.
        /// </summary>
        /// <param name="productName">Product's name to be purchased.</param>
        /// <returns>Information message for selected product purchase process.</returns>
        string Buy(string productName);

        /// <summary>
        /// User ask a vending machine to give change.
        /// </summary>
        /// <returns>Result of change operation.</returns>
        string GiveChange();
    }
}
