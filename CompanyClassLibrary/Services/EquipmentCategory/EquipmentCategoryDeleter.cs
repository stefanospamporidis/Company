using CompanyClassLibrary.Data;
using CompanyClassLibrary.Services.EquipmentData;
using Microsoft.Extensions.Logging;
using System;

namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public class EquipmentCategoryDeleter : IEquipmentCategoryDeleter
    {
        //dependency injections
        private CompanyContext _context;
        private IEquipmentCategoryModelBuilder _builder;
        private IEquipmentDeleter _equipmentDeleter;
        private readonly ILogger<EquipmentCategoryDeleter> _logger;

        public EquipmentCategoryDeleter(CompanyContext context, 
            IEquipmentCategoryModelBuilder builder, 
            IEquipmentDeleter equipmentDeleter,
            ILogger<EquipmentCategoryDeleter> logger)
        {
            _context = context;
            _builder = builder;
            _equipmentDeleter = equipmentDeleter;
            _logger = logger;
        }

        public void Delete(Guid id)
        {
            EquipmentCategory equipmentcategory = _context.EquipmentCategories.Find(id);
            if (equipmentcategory != null)
            {
                equipmentcategory.IsActive = IsActive.NO;
                equipmentcategory.UpdatedAt = DateTime.UtcNow;
                _equipmentDeleter.Delete(id);
                _context.SaveChanges();
                _logger.LogInformation("Finished: Deleting an equipment category from the database");
            }
            else
            {
                _logger.LogDebug("No equipent with this Id was found");
            }
        }

    }
}
