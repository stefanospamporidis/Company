using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;

namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public class EquipmentCategoryModelBuilder : IEquipmentCategoryModelBuilder
    {
        public EquipmentCategoryModel Build(EquipmentCategory data)
        {
            EquipmentCategoryModel model = new EquipmentCategoryModel
            {
                Id = data.Id,
                Name = data.Name,
                IsActive = data.IsActive,
                CreatedAt = data.CreatedAt,
                UpdatedAt = data.UpdatedAt
            };
            return model;
        }
    }
}
