using DeltaXDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.DataAccess
{
    public interface IDataAccess
    {
        ICollection<Movie> GetAllMovies();
        ICollection<Actor> GetActorList();
        ICollection<Producer> GetProducerList();
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        Actor AddActor(Actor actor);
        Producer AddProducer(Producer producer);
        Movie GetMovie(int id);
        void Seed();
    }
}
