using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HUODotNet.Data.Entities;

public class Product
{
    [DisplayName("Mã sản phẩm")]
    public int Id { get; set; }
    [DisplayName("Tên sản phẩm")]
    public string Name { get; set; } = String.Empty;
    [DisplayName("Mô tả")]
    public string Description { get; set; } = String.Empty;
    [DisplayName("Ngày tạo")]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}