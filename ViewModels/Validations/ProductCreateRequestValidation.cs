using FluentValidation;

namespace HUODotNet.ViewModels.Validations;

public class ProductCreateRequestValidation : AbstractValidator<ProductCreateRequest>
{
    public ProductCreateRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm không được bỏ trống");
        RuleFor(x => x.Name).MaximumLength(10).WithMessage("Tên sản phẩm không được quá dài");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được bỏ trống");
    }
}