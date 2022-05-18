using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClassLibrary.Model
{
    public class EquipmentCategoryQueryObject
    {
        public string Type { get; set; }
        public DateTime? CreatedAtAfter { get; set; }
        public DateTime? CreatedAtBefore { get; set; }
        public DateTime? UpdatedAtAfter { get; set; }
        public DateTime? UpdatedAtBefore { get; set; }
        public int? IsActive { get; set; }
        public string OrderAs { get; set; }
        public string OrderBy { get; set; }
        public int Count { get; set; } = 0;
        public int Offset { get; set; } = 0;
    }
}
