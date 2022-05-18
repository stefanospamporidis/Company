using CompanyClassLibrary.Data;
using CompanyClassLibrary.Services.EquipmentCategoryData;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CompanyClassLibrary.Services.EquipmentData
{
    public class EquipmentDeleter : IEquipmentDeleter
    {
        //dependency injections
        private CompanyContext _context;
        private IEquipmentCategoryModelBuilder _builder;
        private ILogger<EquipmentDeleter> _logger;

        public EquipmentDeleter(CompanyContext context, IEquipmentCategoryModelBuilder builder, ILogger<EquipmentDeleter> logger)
        {
            _context = context;
            _builder = builder;
            _logger = logger;
        }
        public void Delete(Guid equipmentcategoryid)
        {
            var equipments = (from equipm in _context.Equipments
                              where equipm.CategoryId == equipmentcategoryid
                              select equipm).ToList();
            if (equipments != null)
            {
                foreach (var equipment in equipments)
                {
                    Equipment equipmentdata = _context.Equipments.Find(equipment.Id);
                    equipmentdata.IsActive = IsActive.NO;
                    equipmentdata.UpdatedAt = DateTime.UtcNow;
                    _context.SaveChanges();
                }
            }
            else
            {
                _logger.LogDebug("No equipent of this category was found");
            }
        }
    }
}
