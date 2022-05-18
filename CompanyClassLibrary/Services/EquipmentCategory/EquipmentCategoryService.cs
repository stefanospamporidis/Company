using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyClassLibrary.Services.EquipmentCategoryData
{
    public class EquipmentCategoryService : IEquipmentCategoryService
    {
        //dependency injections
        private CompanyContext _context;
        private IEquipmentCategoryModelBuilder _builder;
        private readonly ILogger<EquipmentCategoryService> _logger;

        public EquipmentCategoryService(CompanyContext context, 
            IEquipmentCategoryModelBuilder builder,
            ILogger<EquipmentCategoryService> logger)
        {
            _context = context;
            _builder = builder;
            _logger = logger;
        }

        //services
        public List<EquipmentCategory> GetEquipmentCategories()
        {
            return _context.EquipmentCategories.ToList();
        }

        public EquipmentCategoryModel Search(Guid id)
        {
            EquipmentCategory equipmentcategory = _context.EquipmentCategories.Find(id);
            if (equipmentcategory != null)
            {
                EquipmentCategoryModel model = _builder.Build(equipmentcategory);
                _logger.LogInformation("Finished: Searching for a specific equipment category");
                return model;
            }
            else
            {
                _logger.LogDebug("No equipent category with this ID was found");
                // a key not found exception that will return a 404 response
                throw new KeyNotFoundException("Equipment Category not found");
            }

        }

        public EquipmentCategoryModel Add(EquipmentCategoryPersist persistmodel)
        {
            EquipmentCategory data = new EquipmentCategory
            {
                //copy the model to the real entity
                Id = Guid.NewGuid(),
                Name = persistmodel.Name,
                IsActive = IsActive.YES,
                CreatedAt = DateTime.UtcNow
            };
            _context.EquipmentCategories.Add(data);
            _context.SaveChanges();
            _logger.LogInformation("Finished: Adding an equipment category in the database");
            return _builder.Build(data);
        }

        public EquipmentCategoryModel Edit(Guid id,EquipmentCategoryPersist persistmodel)
        {
            EquipmentCategory existingdata = _context.EquipmentCategories.Find(id);
            existingdata.Name = persistmodel.Name;
            existingdata.UpdatedAt = DateTime.Now;
            _context.EquipmentCategories.Update(existingdata);
            _context.SaveChanges();
            _logger.LogInformation("Finished: Editing an equipment category inside the database");
            return _builder.Build(existingdata);
        }

        public List<EquipmentCategoryModel> Select(EquipmentCategoryQueryObject queryInfo)
        {
            IQueryable<EquipmentCategory> query = _context.EquipmentCategories;
            List<EquipmentCategoryModel> alltypes = new List<EquipmentCategoryModel>();

            //Type filter
            if (queryInfo.Type != null)
            {
                query = query.Where(x => x.Name == queryInfo.Type);
            }

            //CreatedAt filter
            if (queryInfo.CreatedAtAfter != null && queryInfo.CreatedAtBefore != null)
            {
                //Single Date or between two Dates
                if (queryInfo.CreatedAtAfter == queryInfo.CreatedAtBefore)
                {
                    query = query.Where(x => x.CreatedAt == queryInfo.CreatedAtAfter);
                }
                else
                {
                    query = query.Where(x => x.CreatedAt >= queryInfo.CreatedAtAfter && x.CreatedAt <= queryInfo.CreatedAtBefore);
                }
            }
            else if (queryInfo.CreatedAtAfter != null)
            {
                query = query.Where(x => x.CreatedAt >= queryInfo.CreatedAtAfter);
            }
            else if (queryInfo.CreatedAtBefore != null)
            {
                query = query.Where(x => x.CreatedAt <= queryInfo.CreatedAtBefore);
            }

            //UpdatedAt filter
            if (queryInfo.UpdatedAtAfter != null && queryInfo.UpdatedAtBefore != null)
            {
                //Single Date or between two Dates
                if (queryInfo.UpdatedAtAfter == queryInfo.UpdatedAtBefore)
                {
                    query = query.Where(x => x.UpdatedAt == queryInfo.UpdatedAtAfter);
                }
                else
                {
                    query = query.Where(x => x.UpdatedAt >= queryInfo.UpdatedAtAfter && x.UpdatedAt <= queryInfo.UpdatedAtBefore);
                }
            }
            else if (queryInfo.UpdatedAtAfter != null)
            {
                query = query.Where(x => x.UpdatedAt >= queryInfo.UpdatedAtAfter);
            }
            else if (queryInfo.UpdatedAtBefore != null)
            {
                query = query.Where(x => x.UpdatedAt <= queryInfo.UpdatedAtBefore);
            }

            //Type filter
            if (queryInfo.IsActive != null)
            {
                if (queryInfo.IsActive == 0)
                {
                    query = query.Where(x => x.IsActive == IsActive.NO);
                }
                else if (queryInfo.IsActive == 1)
                {
                    query = query.Where(x => x.IsActive == IsActive.YES);
                }
            }

            //OrderAs && OrderBy filters
            //1st case : We are given the item we order our list and then if the order is ascending or descending
            if (queryInfo.OrderAs != null && queryInfo.OrderBy != null)
            {
                if (queryInfo.OrderAs == "ascending")
                {
                    if (queryInfo.OrderBy == "Name")
                    {
                        query = query.OrderBy(x => x.Name);
                    }
                    else if (queryInfo.OrderBy == "CreatedAt")
                    {
                        query = query.OrderBy(x => x.CreatedAt);
                    }
                    else if (queryInfo.OrderBy == "UpdatedAt")
                    {
                        query = query.OrderBy(x => x.UpdatedAt);
                    }
                }
                else if (queryInfo.OrderAs == "descending")
                {
                    if (queryInfo.OrderBy == "Name")
                    {
                        query = query.OrderByDescending(x => x.Name);
                    }
                    else if (queryInfo.OrderBy == "CreatedAt")
                    {
                        query = query.OrderByDescending(x => x.CreatedAt);
                    }
                    else if (queryInfo.OrderBy == "UpdatedAt")
                    {
                        query = query.OrderByDescending(x => x.UpdatedAt);
                    }
                }
            }
            //2nd case : We are given only if the List is ascending or descending, I will order my list by Name cause it isn't given
            else if (queryInfo.OrderAs != null && queryInfo.OrderBy == null)
            {
                if (queryInfo.OrderAs == "ascending")
                {
                    query = query.OrderBy(x => x.Name);
                }
                else if (queryInfo.OrderAs == "descending")
                {
                    query = query.OrderByDescending(x => x.Name);
                }
            }
            //3rd case : We are given only the item we order the list with , I chose the list to ascend as OrderBy isn't given
            else if (queryInfo.OrderAs == null && queryInfo.OrderBy != null)
            {
                if (queryInfo.OrderBy == "Name")
                {
                    query = query.OrderBy(x => x.Name);
                }
                else if (queryInfo.OrderBy == "CreatedAt")
                {
                    query = query.OrderBy(x => x.CreatedAt);
                }
                else if (queryInfo.OrderBy == "UpdatedAt")
                {
                    query = query.OrderBy(x => x.UpdatedAt);
                }
            }

            //Skip filter , in this filter we skip a certain ammount of records from the query
            if (queryInfo.Offset != 0)
            {
                query = query.Skip(queryInfo.Offset);
            }

            //Take filter, in this filter we take a certain ammount of records from the query
            if (queryInfo.Count != 0)
            {
                query = query.Take(queryInfo.Count);
            }

            foreach(var item in query)
            {
                alltypes.Add(new EquipmentCategoryModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsActive = item.IsActive,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                });
            }

            return alltypes;
        }
    }
}
