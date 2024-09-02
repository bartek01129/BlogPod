using BlogPod.MVC.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPod.MVC.Controllers
{

    public class UserController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: City
        public async Task<IActionResult> IndexUser()
        {
            return View(await _context.BlogValue.ToListAsync());
        }

        // GET: City/Polska/?


        public async Task<IActionResult> Polska(string? country)
        {

            var cityEntity = await _context.BlogValue.Where(c => c.Country == country).ToListAsync();

            return View(cityEntity);

        }


        // GET: City/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityEntity = await _context.BlogValue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityEntity == null)
            {
                return NotFound();
            }

            return View(cityEntity);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,City,Image,Country")] CityEntity cityEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cityEntity);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityEntity = await _context.BlogValue.FindAsync(id);
            if (cityEntity == null)
            {
                return NotFound();
            }
            return View(cityEntity);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,City,Country")] CityEntity cityEntity, IFormFile imageFile)
        {
            if (id != cityEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Sprawdź, czy użytkownik wybrał plik
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Konwertuj plik na tablicę bajtów
                        using (var ms = new MemoryStream())
                        {
                            imageFile.CopyTo(ms);
                            cityEntity.Image = ms.ToArray();
                        }
                    }

                    _context.Update(cityEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityEntityExists(cityEntity.Id))
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
            return View(cityEntity);
        }


        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityEntity = await _context.BlogValue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityEntity == null)
            {
                return NotFound();
            }

            return View(cityEntity);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityEntity = await _context.BlogValue.FindAsync(id);
            if (cityEntity != null)
            {
                _context.BlogValue.Remove(cityEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityEntityExists(int id)
        {
            return _context.BlogValue.Any(e => e.Id == id);
        }
    }
}
