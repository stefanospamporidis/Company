using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClassLibrary.Services.EquipmentData
{
    public interface IEquipmentDeleter
    {
        void Delete(Guid equipmentcategoryid);
    }
}
