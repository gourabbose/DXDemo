using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaXDemo.DataAccess;
using DeltaXDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeltaXDemo.Controllers
{
    public class PersonController : Controller
    {
        private IDataAccess _dataAccess;

        public PersonController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        [HttpPost]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            try
            {
                _dataAccess.AddActor(actor);
                return Json(new { Status = true });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult AddProducer([FromBody] Producer producer)
        {
            try
            {
                _dataAccess.AddProducer(producer);
                return Json(new { Status = true });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }
        public IActionResult GetActors()
        {
            try
            {
                var actors = from actor in _dataAccess.GetActorList()
                             select new { Id = actor.Id, Name = actor.Name };
                return Json(new { Status = true, Data = actors });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }
        public IActionResult GetProducers()
        {
            try
            {
                var producers = from producer in _dataAccess.GetProducerList()
                                select new { Id = producer.Id, Name = producer.Name };
                return Json(new { Status = true, Data = producers });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }
    }
}