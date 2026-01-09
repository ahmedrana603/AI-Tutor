using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AITutorWebsite.Data;
using AITutorWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AITutorWebsite.Controllers
{
    public class AIImagesController : Controller
    {
        private readonly AppDbContext _context;

        public AIImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AIImages (Visitor+)
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _context.AIImages.OrderByDescending(i => i.UploadDate).ToListAsync();
                return View(items);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading images: " + ex.Message;
                return View(new List<AIImage>());
            }
        }

        // GET: AIImages/Details/5 (Admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.AIImages.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: AIImages/Create (Member+)
        [Authorize(Roles = "Member,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AIImages/Create (Member+)
        [Authorize(Roles = "Member,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Prompt,ImageGenerator,Like,canIncreaseLike")] AIImage aiImage, IFormFile ImageFile)
        {
            try
            {
                ModelState.Remove("UserId");
                ModelState.Remove("UserName");
                ModelState.Remove("Filename");
                ModelState.Remove("UploadDate");

                if (!ModelState.IsValid) return View(aiImage);

                if (ImageFile == null || ImageFile.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Please select an image file.");
                    return View(aiImage);
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "Only JPG, JPEG, and PNG files are allowed.");
                    return View(aiImage);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                aiImage.Filename = "/uploads/" + fileName;
                aiImage.UploadDate = DateTime.UtcNow;
                aiImage.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                aiImage.UserName = User.Identity?.Name ?? "Unknown User";

                _context.Add(aiImage);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Image uploaded successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error uploading image: " + ex.Message);
                return View(aiImage);
            }
        }

        // GET: AIImages/Edit/5 (Admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.AIImages.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: AIImages/Edit/5 (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prompt,ImageGenerator,UploadDate,Like,canIncreaseLike")] AIImage aiImage, IFormFile ImageFile)
        {
            if (id != aiImage.Id) return NotFound();

            try
            {
                var existingImage = await _context.AIImages.FindAsync(id);
                if (existingImage == null) return NotFound();

                existingImage.Prompt = aiImage.Prompt;
                existingImage.ImageGenerator = aiImage.ImageGenerator;
                existingImage.Like = aiImage.Like;
                existingImage.canIncreaseLike = aiImage.canIncreaseLike;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ImageFile", "Only JPG, JPEG, and PNG files are allowed.");
                        return View(aiImage);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    existingImage.Filename = "/uploads/" + fileName;
                }

                _context.Update(existingImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error updating image: " + ex.Message);
                return View(aiImage);
            }
        }

        // GET: AIImages/Delete/5 (Admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.AIImages.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: AIImages/Delete/5 (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.AIImages.FindAsync(id);
            if (item != null)
            {
                _context.AIImages.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: AIImages/ToggleLike (Member+)
        [Authorize(Roles = "Member,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLike(int id)
        {
            try
            {
                var item = await _context.AIImages.FindAsync(id);
                if (item == null)
                    return NotFound();

                var sessionKey = $"liked_aiimage_{id}";
                var isLiked = HttpContext.Session.GetString(sessionKey) == "1";

                if (!isLiked)
                {
                    item.Like += 1;
                    HttpContext.Session.SetString(sessionKey, "1");
                }
                else
                {
                    if (item.Like > 0)
                        item.Like -= 1;
                    HttpContext.Session.SetString(sessionKey, "0");
                }

                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
