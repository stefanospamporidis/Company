using CompanyClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClassLibrary.Model.Equipment
{
    public class EquipmentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IsActive IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
