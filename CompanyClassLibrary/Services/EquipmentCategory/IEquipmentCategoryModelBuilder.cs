using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;

namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public interface IEquipmentCategoryModelBuilder
    {
        EquipmentCategoryModel Build(EquipmentCategory obj);
    }
}
