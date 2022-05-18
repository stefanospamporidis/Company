using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyClassLibrary.Data
{
    [Table("Equipment")]
    public class Equipment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public IsActive IsActive { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }

        //relationship between Workstation->Equipment(foreign key)
        public Guid WorkstationId { get; set; }
        [ForeignKey(nameof(WorkstationId))]
        public Workstation Workstation { get; set; }

        //relationship between Equipment->EquipmentCategory(foreign key)
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public EquipmentCategory EquipmentCategory { get; set; }
    }
}
