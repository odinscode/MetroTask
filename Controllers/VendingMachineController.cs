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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var stocks = vmService.GetAllAvailableProductsInStock();

            return stocks.Select(_ => (_).ToString());
        }

        [HttpPut("[action]")]
        public double DepositMoney(double banknote)
        {
            return vmService.DepositMoney(banknote);
        }

        [HttpPost("[action]")]
        public string Buy(string productName)
        {
            return vmService.Buy(productName);
        }

        [HttpPost("[aciton]")]
        public string GiveChange()
        {
            return vmService.GiveChange();
        }
    }
}
