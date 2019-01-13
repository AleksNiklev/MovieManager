using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.Models;
using MovieManager.Models;
using RestSharp;
using X.PagedList;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient($"{this.Request.Scheme}://{this.Request.Host}");

            var request = new RestRequest("api/movies", Method.GET);
            var response = client.Execute<List<Movie>>(request);

            var viewModel = response.Data.Select(m => new MovieViewModel(m));
            return View(viewModel);
        }

    }
}

