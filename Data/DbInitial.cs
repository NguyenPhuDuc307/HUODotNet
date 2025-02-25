using HUODotNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HUODotNet.Data;

public static class DbInitial
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            context.Products.AddRange(
                new Product
                {
                    Name = "Hoodie",
                    Description = "A hoodie is a type of casual wear, suitable for both men and women, made of soft and comfortable material, with a fashionable design, and a warm and cozy feeling.",
                    ImageUrl = "/user-content/hoodie.jpg"
                },
                new Product
                {
                    Name = "T-Shirt",
                    Description = "A casual wear, suitable for both men and women, made of soft and comfortable material, with a fashionable design, and a warm and cozy feeling.",
                    ImageUrl = "/user-content/t-shirt.jpg"
                },
                new Product
                {
                    Name = "Jeans",
                    Description = "A type of pants, suitable for both men and women, made of soft and comfortable material, with a fashionable design, and a warm and cozy feeling.",
                    ImageUrl = "/user-content/jeans.jpg"
                },
                new Product
                {
                    Name = "Sweater",
                    Description = "A type of casual wear, suitable for both men and women, made of soft and comfortable material, with a fashionable design, and a warm and cozy feeling.",
                    ImageUrl = "/user-content/sweater.jpg"
                },
                new Product
                {
                    Name = "Shirt",
                    Description = "A type of casual wear, suitable for both men and women, made of soft and comfortable material, with a fashionable design, and a warm and cozy feeling.",
                    ImageUrl = "/user-content/shirt.jpg"
                });
            await context.SaveChangesAsync();
        }
    }
}