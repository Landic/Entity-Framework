using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsDapper
{
    internal class PromotionGood
    {
        public int Id { get; set; }
        public string? GoodName { get; set; }
        public int? PromotionId { get; set; }
    }
}
