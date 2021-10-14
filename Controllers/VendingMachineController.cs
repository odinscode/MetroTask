using MetroTaskV2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachineController : ControllerBase
    {
        private readonly IVendingMachineService vmService;

        public VendingMachineController(
            IVendingMachineService vmService
            )
        {
            this.vmService = vmService;
        }

        /// <summary>
        /// Represenet the current stocks of vending machine.
        /// </summary>
        /// <returns>Collection of stocks with product type and available quantity.</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var stocks = vmService.GetAllAvailableProductsInStock();

            return stocks.Select(_ => (_).ToString());
        }

        /// <summary>
        /// Customer's analog for depositing money into VM.
        /// Purposly didn't make a validation for banknote denomination for easier testing purposes.
        /// </summary>
        /// <param name="banknote">Amount of money customer transfer on deposit of VM.</param>
        /// <returns>Current deposited amount of money.</returns>
        [HttpPut("[action]")]
        public double DepositMoney(double banknote)
        {
            return vmService.DepositMoney(banknote);
        }

        /// <summary>
        /// Buy selected product from VM.
        /// </summary>
        /// <param name="productName">Name of product to be purchased,</param>
        /// <returns>Result of operation.</returns>
        [HttpPost("[action]")]
        public string Buy(string productName)
        {
            return vmService.Buy(productName);
        }

        /// <summary>
        /// Give change from deposited money.
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public string GiveChange()
        {
            return vmService.GiveChange();
        }
    }
}
