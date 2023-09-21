using Microsoft.AspNetCore.Mvc;
using MovieListApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MovieListApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private MovieContext _movieContext { get; set; }

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add";
			ViewBag.Genres = _movieContext.Genres.OrderBy(g => g.Name).ToList();
			return View("Edit", new Movie());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			ViewBag.Genres = _movieContext.Genres.OrderBy(g => g.Name).ToList();
			var movie = _movieContext.Movies.Find(id);
			return View(movie);
		}

		[HttpPost]
		public IActionResult Edit(Movie movie)
		{
			if (ModelState.IsValid)
			{
				if (movie.MovieId == 0)
					_movieContext.Movies.Add(movie);
				else
					_movieContext.Movies.Update(movie);
				_movieContext.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Action =
					(movie.MovieId == 0) ? "Add" : "Edit";
				ViewBag.Genres = _movieContext.Genres.OrderBy(g => g.Name).ToList();
				return View(movie);
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var movie = _movieContext.Movies.Find(id);
			return View(movie);
		}

		[HttpPost]
		public IActionResult Delete(Movie movie)
		{
			_movieContext.Movies.Remove(movie);
			_movieContext.SaveChanges();
			return RedirectToAction("Index", "Home");
		}


		public HomeController(MovieContext ctx)
		{
			_movieContext = ctx;
		}

		public IActionResult Index()
		{

			var movies = _movieContext.Movies.Include(m => m.Genre)
				.OrderBy(m => m.Name).ToList();
			return View(movies);
/*			var movies = _movieContext.Movies.OrderBy(
				m => m.Name).ToList();
			return View(movies);*/
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}