using System;
using System.ComponentModel.DataAnnotations;
using CompanyClassLibrary.Data;

namespace CompanyClassLibrary.Model
{
    public class EquipmentCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IsActive IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
