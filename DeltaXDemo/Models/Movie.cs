using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.Models
{
    public class Movie:BaseModel
    {
        public Movie()
        {
            this.Actors = new List<MovieActorMapping>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        public byte[] Poster { get; set; }

        [Required]
        public IEnumerable<MovieActorMapping> Actors { get; set; }

        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        [NotMapped]
        public string PosterSRC
        {
            get
            {
                return this.Poster != null ? String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(this.Poster)) : string.Empty;
            }
        }
        [NotMapped]
        public IEnumerable<int> ActorIds { get; set; }
    }
}
