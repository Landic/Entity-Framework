using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsDapper
{
    internal class PromotionVM
    {
        public string? PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDateDate { get; set; }
        public string? Country { get; set; }
    }
}
