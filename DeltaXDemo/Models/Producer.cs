using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.Models
{
    public class Producer:BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Sex Sex { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Bio { get; set; }
    }
}
