using CompanyClassLibrary.Model;
using System;

namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public interface IEquipmentCategoryDeleter
    {
        void Delete(Guid id);

    }
}
