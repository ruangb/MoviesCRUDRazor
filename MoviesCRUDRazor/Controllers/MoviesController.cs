using InvestManager.Services;
using Microsoft.AspNetCore.Mvc;
using MoviesCRUDRazor.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoviesCRUDRazor.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _MovieService;

        public MoviesController(MovieService MovieService)
        {
            _MovieService = MovieService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _MovieService.FindAllAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie Movie)
        {
            if (!ModelState.IsValid)
                return View(Movie);

            await _MovieService.InsertAsync(Movie);
            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var obj = await _MovieService.FindByIdAsync(id.Value);

                if (obj != null)
                    return View(obj);
            }

            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _MovieService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var obj = await _MovieService.FindByIdAsync(id.Value);

                if (obj != null)
                    return View(obj);
            }

            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var obj = await _MovieService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie Movie)
        {
            if (!ModelState.IsValid)
                return View(Movie);

            if (id != Movie.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                await _MovieService.UpdateAsync(Movie);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
