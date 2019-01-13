using System;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using RestSharp;

namespace MovieManager.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (movie.ReleaseDate > DateTime.Now)
            {
                TempData["dataError"] = "Release date should not be future date!";
                return View();
            }

            var client = new RestClient($"{this.Request.Scheme}://{this.Request.Host}");
            var request = new RestRequest($"api/movies", Method.POST);
            request.AddJsonBody(movie);

            var response = client.Execute<MovieViewModel>(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Details", new { id = response.Data.Id });
            }

            TempData["apiError"] = "Opps! something happend!";
            return View();
        }

        public ActionResult Details(int id)
        {
            var client = new RestClient($"{this.Request.Scheme}://{this.Request.Host}");

            var request = new RestRequest($"api/movies/{id}", Method.GET);
            var response = client.Execute<MovieViewModel>(request);
            if (response.IsSuccessful)
            {
                return View(response.Data);
            }

            TempData["apiError"] = "Opps! something happend!";
            return View();
        }

        public IActionResult Update(int id)
        {
            var client = new RestClient($"{this.Request.Scheme}://{this.Request.Host}");

            var request = new RestRequest($"api/movies/{id}", Method.GET);
            var response = client.Execute<MovieViewModel>(request);
            if (response.IsSuccessful)
            {
                return View(response.Data);
            }

            TempData["apiError"] = "Opps! something happend!";
            return View();
        }

        [HttpPost]
        public IActionResult Update(MovieViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            if (movie.ReleaseDate > DateTime.Now)
            {
                TempData["dataError"] = "Release date should not be future date!";
                return View(movie);
            }

            var client = new RestClient($"{this.Request.Scheme}://{this.Request.Host}");
            var request = new RestRequest($"api/movies/{movie.Id}", Method.PUT);
            request.AddJsonBody(movie);

            var response = client.Execute<MovieViewModel>(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Details", new { id = movie.Id });
            }

            return View();
        }
    }
}