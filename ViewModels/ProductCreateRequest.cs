using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HUODotNet.ViewModels;

public class ProductCreateRequest
{
    [DisplayName("Tiêu đề")]
    public string? Name { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }
}