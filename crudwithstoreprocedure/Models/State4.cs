using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudwithstoreprocedure.Models
{
    public class State4
    {
        [Key]
        public int StateId { get; set; }

       public string StateName { get; set; }

    }
}