using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.Models
{
    public class MovieActorMapping : BaseModel
    {
        public MovieActorMapping()
        {

        }
        public MovieActorMapping(int id)
        {
            this.Actor = new Actor { Id = id };
        }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
