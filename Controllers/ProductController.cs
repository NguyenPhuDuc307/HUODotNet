using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HUODotNet.Data;
using HUODotNet.Data.Entities;
using HUODotNet.ViewModels;
using AutoMapper;
using HUODotNet.Services;
using System.Net.Http.Headers;

namespace HUODotNet.Controllers;

[Route("san-pham")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;
    private readonly string USER_CONTENT_FOLDER_NAME = "user-content";
    public ProductController(ApplicationDbContext context, IMapper mapper, IStorageService storageService)
    {
        _context = context;
        _mapper = mapper;
        _storageService = storageService;
    }

    // GET: Products
    [HttpGet("danh-sach")]
    // [Route("danh-sach")]
    // [HttpGet]
    public async Task<IActionResult> Index()
    {
        var listProduct = await _context.Products.ToListAsync();
        return View(_mapper.Map<IEnumerable<ProductViewModel>>(listProduct));
    }

    // GET: Products/Details/5
    [HttpGet("chi-tiet/{id}")]
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

        return View(product);
    }

    // GET: Products/Create
    [HttpGet("them-moi")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("them-moi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateRequest request)
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
            return RedirectToAction(nameof(Index));
        }
        return View(request);
    }

    // GET: Products/Edit/5
    [HttpGet("sua/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("sua/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
                await _storageService.DeleteFileAsync(product.ImageUrl.Replace("/" + USER_CONTENT_FOLDER_NAME + "/", ""));
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }
}
