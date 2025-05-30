using CookShare.Data;
using CookShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookShare.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Recipe
        public async Task<IActionResult> Index(string searchString, string category)
        {
            Consolelog
            var recipes = _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ratings)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Title.Contains(searchString) || 
                                          r.Description.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category))
            {
                recipes = recipes.Where(r => r.Category == category);
            }

            return View(await recipes.ToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.User)
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipe/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe recipe, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                recipe.UserId = user.Id;
                recipe.CreatedAt = DateTime.UtcNow;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine("wwwroot", "uploads", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    recipe.ImageUrl = "/uploads/" + fileName;
                }

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        // GET: Recipe/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || recipe.UserId != user.Id)
            {
                return Unauthorized();
            }

            return View(recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe, IFormFile? imageFile)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingRecipe = await _context.Recipes.FindAsync(id);
                    if (existingRecipe == null)
                    {
                        return NotFound();
                    }

                    var user = await _userManager.GetUserAsync(User);
                    if (user == null || existingRecipe.UserId != user.Id)
                    {
                        return Unauthorized();
                    }

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine("wwwroot", "uploads", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        recipe.ImageUrl = "/uploads/" + fileName;
                    }

                    recipe.UserId = existingRecipe.UserId;
                    recipe.CreatedAt = existingRecipe.CreatedAt;
                    recipe.UpdatedAt = DateTime.UtcNow;

                    _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
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
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || recipe.UserId != user.Id)
            {
                return Unauthorized();
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || recipe.UserId != user.Id)
            {
                return Unauthorized();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
} 