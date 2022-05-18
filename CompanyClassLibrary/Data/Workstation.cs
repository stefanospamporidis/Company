using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClassLibrary.Data
{
    public class Workstation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int IsActive { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }

        //relationship between Branch->Workstation(foreign key)
        public Guid BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }

        //relationship between Workstation->WorkstationEmployeeLink(primary key)
        public ICollection<WorkstationEmployeeLink> WorkstationEmployeeLinks { get; set; }
        //relationship between Workstation->Equipment(primary key)
        public ICollection<Equipment> Equipments { get; set; }

    }
}
