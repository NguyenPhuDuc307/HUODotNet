using HUODotNet.Data.Interfaces;

namespace HUODotNet.Data.Entities;

public class Product : IDateTracking
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}