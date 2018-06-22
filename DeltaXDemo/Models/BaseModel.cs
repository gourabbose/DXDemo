using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXDemo.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }
}
