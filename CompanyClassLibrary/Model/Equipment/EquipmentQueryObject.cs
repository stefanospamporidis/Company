using CompanyClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClassLibrary.Model.Equipment
{
    public class EquipmentQueryObject
    {
        public string Name { get; set; }
        public string EquipmentCategoryName { get; set; }
        public string WorkstationName { get; set; }
        public IsActive IsActive { get; set; }
        public DateTime CreatedAtBefore { get; set; }
        public DateTime CreatedAtAfter { get; set; }
        public DateTime UpdatedAtBefore { get; set; }
        public DateTime UpdatedAtAfter { get; set; }
        public string OrderAs { get; set; }
        public string OrderBy { get; set; }
        public int Count { get; set; } = 0;
        public int Offset { get; set; } = 0;

    }
}
