using DeltaXDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.ViewModels
{
    public class CreateEditMovieViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1800, 2018, ErrorMessage = "Please provide a valid release year!")]
        [Display(Name = "Year of Release")]
        public int ReleaseYear { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 50, ErrorMessage = "Please provide 50 to 100 characters!")]
        public string Plot { get; set; }
        
        [Required]
        [Display(Name="Producer")]
        [Range(0,int.MaxValue,ErrorMessage ="Please select Producer!")]
        public int ProducerId { get; set; }

        [BindRequired]
        public IEnumerable<int> Actors { get; set; }

        public SelectList ProducerList { get; set; }
        public SelectList ActorList { get; set; }
    }
}
