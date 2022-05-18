using CompanyClassLibrary.Services.EquipmentCategoryData;
using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CompanyClassLibrary.Validators;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CompanyWebApi.Helpers;

namespace CompanyWebApi.Controllers
{
    [ApiController]
    [Route("api/equipment-category")]
    public class EquipmentCategoryController : ControllerBase
    {
        private IEquipmentCategoryService _equipmentCategoryService;
        private IEquipmentCategoryDeleter _equipmentCategoryDeleter;
        private readonly ILogger<EquipmentCategoryController> _logger;

        public EquipmentCategoryController(IEquipmentCategoryService equipmentCategoryService,
            IEquipmentCategoryDeleter equipmentCategoryDeleter, 
            ILogger<EquipmentCategoryController> logger)
        {
            _equipmentCategoryService = equipmentCategoryService;
            _equipmentCategoryDeleter = equipmentCategoryDeleter;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetEquipmentCategories()
        {
            _logger.LogInformation("Started: Searching for equipment categories");
            return Ok(_equipmentCategoryService.GetEquipmentCategories());
        }
        
        [HttpGet]
        [Route("{id}")]
        public EquipmentCategoryModel GetEquipmentCategory(Guid id)
        {
            _logger.LogInformation("Started: Searching for a specific equipment category");

            EquipmentCategoryModel equipmentCategory = _equipmentCategoryService.Search(id);

            return equipmentCategory;
        }
        
        [HttpPost]
        public EquipmentCategoryModel AddEquipmentCategory(EquipmentCategoryPersist persistmodel)
        {
            _logger.LogInformation("Started: Adding an equipment category in the database");

            EquipmentCategoryValidator validator = new EquipmentCategoryValidator();
            ValidationResult result = validator.Validate(persistmodel);

            if (!result.IsValid)
            {
                foreach (ValidationFailure rslt in result.Errors)
                {
                    ModelState.AddModelError(rslt.PropertyName,rslt.ErrorMessage);
                    throw new AppException("EquipmentCategory Name is incorrect");
                }
            }

            EquipmentCategoryModel model = _equipmentCategoryService.Add(persistmodel);
            return model;

        }

        [HttpDelete]
        [Route("{id}")]
        public EquipmentCategoryModel DeleteEquipmentCategory(Guid id)
        {
            _logger.LogInformation("Started: Deleting an equipment category from the database");

            var model = _equipmentCategoryService.Search(id);
            _equipmentCategoryDeleter.Delete(id);
            return model;
            //return NotFound($"Equipment with Id: {id} was not found");
        }

        
        [HttpPatch]
        [Route("{id}")]
        public EquipmentCategoryModel EditEquipmentCategory(Guid id,EquipmentCategoryPersist persistmodel)
        {
            _logger.LogInformation("Started: Editing an equipment category inside the database");

            EquipmentCategoryValidator validator = new EquipmentCategoryValidator();
            ValidationResult result = validator.Validate(persistmodel);
            if (!result.IsValid)
            {
                foreach (ValidationFailure rslt in result.Errors)
                {
                    ModelState.AddModelError(rslt.PropertyName, rslt.ErrorMessage);
                    throw new AppException("EquipmentCategory Name is incorrect");
                }
            }

            var existingequipmentcategory = _equipmentCategoryService.Search(id);
            EquipmentCategoryModel model = _equipmentCategoryService.Edit(id,persistmodel);

            return model;
            //return NotFound($"Equipment with Id: {id} was not found");
        }

        [HttpPost("Select")]
        public List<EquipmentCategoryModel> GetSelect(EquipmentCategoryQueryObject queryInfo)
        {
            List<EquipmentCategoryModel> allequipmentcategories = _equipmentCategoryService.Select(queryInfo);
            return allequipmentcategories;
        }

    } 
}
