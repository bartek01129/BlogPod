using BlogPod.MVC.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPod.MVC.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDBContext _context;


        public CityController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: City
        public async Task<IActionResult> Index()
        {


            return View(await _context.BlogValue.ToListAsync());
        }

        public async Task<IActionResult> Country(string? country)
        {

            var cityEntity = await _context.BlogValue.Where(c => c.Country == country).ToListAsync();

            return View(cityEntity);

        }

        // GET: City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var city = _context.BlogValue.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return PartialView("Details", city);
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
            var countries = await _context.BlogValue.Select(b => b.Country).Distinct().ToListAsync();



            cityEntity.Country = countries.FirstOrDefault();



            return View(cityEntity);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var city = _context.BlogValue.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return PartialView("Edit", city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CityEntity cityEntity, IFormFile imageFile)
        {
            if (id != cityEntity.Id)
            {
                return NotFound();
            }

            if (imageFile != null)
            {
                try
                {
                    // Konwertuj plik na tablicę bajtów
                    using (var ms = new MemoryStream())
                    {
                        imageFile.CopyTo(ms);
                        cityEntity.Image = ms.ToArray();
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
            }
            else
            {

                var existingEntity = await _context.BlogValue.FindAsync(cityEntity.Id);

                cityEntity.Image = existingEntity.Image;


                if (_context.Entry(existingEntity).State != EntityState.Detached)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(cityEntity);
                }
                else
                {
                    _context.Update(cityEntity);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var city = _context.BlogValue.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return PartialView("Delete", city);
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
