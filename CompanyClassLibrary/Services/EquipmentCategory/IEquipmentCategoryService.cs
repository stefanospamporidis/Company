using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;
using System;
using System.Collections.Generic;


namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public interface IEquipmentCategoryService
    {

        List<EquipmentCategory> GetEquipmentCategories();

        EquipmentCategoryModel Search(Guid id);

        EquipmentCategoryModel Add(EquipmentCategoryPersist persistmodel);

        EquipmentCategoryModel Edit(Guid id,EquipmentCategoryPersist persistmodel);

        List<EquipmentCategoryModel> Select(EquipmentCategoryQueryObject queryInfo);

    }
}
