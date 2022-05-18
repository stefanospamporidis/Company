using CompanyClassLibrary.Model;
using FluentValidation;


namespace CompanyClassLibrary.Validators
{
    public class EquipmentCategoryValidator : AbstractValidator<EquipmentCategoryPersist>
    {
        public EquipmentCategoryValidator()
        {
            //EquipmentCategory Name is required and it should be between 1-50 characters
            RuleFor(e => e.Name).Length(1, 50).WithMessage("Equipment Category name should be between 1-50 characters");
        }
    }
}
