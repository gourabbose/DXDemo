using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaXDemo.DataAccess;
using DeltaXDemo.Models;
using DeltaXDemo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeltaXDemo.Controllers
{
    public class MoviesController : Controller
    {
        private IDataAccess _dataAccess;

        public MoviesController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult Index()
        {
            return View(_dataAccess.GetAllMovies());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if(TempData["Error"]!=null)
            {
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            }
            var producers = (IEnumerable<Producer>) _dataAccess.GetProducerList().ToList();
            producers = producers.Prepend(new Producer { Id = -1, Name = "-" });
            CreateEditMovieViewModel model = new CreateEditMovieViewModel
            {
                ActorList = new SelectList(_dataAccess.GetActorList(), "Id", "Name", "-"),
                ProducerList = new SelectList(producers, "Id", "Name", "-")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEditMovieViewModel viewModel, IFormFile Poster)
        {
            if (ModelState.IsValid)
            {
                byte[] posterContent;
                using (var ms = new System.IO.MemoryStream())
                {
                    Poster.CopyTo(ms);
                    posterContent = ms.ToArray();
                }
                Movie movie = new Movie
                {
                    Name = viewModel.Name,
                    Actors = (from actorid in viewModel.Actors
                              select new MovieActorMapping {ActorId=actorid }).ToList(),
                    ProducerId = viewModel.ProducerId,
                    Plot = viewModel.Plot,
                    ReleaseYear = viewModel.ReleaseYear,
                    Poster = posterContent
                };
                var result = _dataAccess.UpdateMovie(movie);
                if (result.Id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Could not save Movie to database!";
                    return View(viewModel);
                }
            }
            else
            {
                TempData["Error"] = "Could not save Movie to database!";
                return View(viewModel);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _dataAccess.GetMovie(id);
            var viewModel = new CreateEditMovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Plot = movie.Plot,
                Actors = from actor in movie.Actors select actor.ActorId,
                ReleaseYear = movie.ReleaseYear,
                ProducerId = movie.ProducerId,
                ActorList = new SelectList(_dataAccess.GetActorList(), "Id", "Name", "-"),
                ProducerList = new SelectList(_dataAccess.GetProducerList(), "Id", "Name", "-")
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(CreateEditMovieViewModel viewModel, IFormFile Poster)
        {
            if (ModelState.IsValid)
            {
                byte[] posterContent = new byte[0];
                if (Poster != null && Poster.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        Poster.CopyTo(ms);
                        posterContent = ms.ToArray();
                    }
                }
                Movie movie = _dataAccess.GetMovie(viewModel.Id);
                movie.Id = viewModel.Id;
                movie.Name = viewModel.Name;
                movie.Actors = (from actorid in viewModel.Actors
                                select new MovieActorMapping { ActorId = actorid }).ToList();
                movie.ProducerId = viewModel.ProducerId;
                movie.Plot = viewModel.Plot;
                movie.ReleaseYear = viewModel.ReleaseYear;
                if(posterContent.Length>0)
                {
                    movie.Poster = posterContent;
                }
                var result = _dataAccess.UpdateMovie(movie);
                if (result.Id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Could not save Movie to database!";
                    return View(viewModel);
                }
            }
            else
            {
                TempData["Error"] = "Could not save Movie to database!";
                return View(viewModel);
            }
        }
 
    }
}