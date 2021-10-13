using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroTaskV2.Models
{
    /// <summary>
    /// Represent a banknote type with it's actual value.
    /// </summary>
    public class Banknote
    {
        public BanknoteEnum BanknoteType { get; set; }
        public double Value { get; set; }
    }
}
