using System.Net.Http.Headers;
using AutoMapper;
using HUODotNet.Data;
using HUODotNet.Data.Entities;
using HUODotNet.Services;
using HUODotNet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HUODotNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;
    private readonly string USER_CONTENT_FOLDER_NAME = "user-content";
    public ProductsController(ApplicationDbContext context, IMapper mapper, IStorageService storageService)

    {
        _context = context;
        _mapper = mapper;
        _storageService = storageService;
    }

    // GET: Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var listProduct = await _context.Products.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ProductViewModel>>(listProduct));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FindAsync(id);
        // .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(request);

            if (request.ImageFile != null)
            {
                product.ImageUrl = await SaveFile(request.ImageFile);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
        }
        return Ok(request);
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }
}