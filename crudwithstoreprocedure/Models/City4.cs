using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudwithstoreprocedure.Models
{
    public class City4
    {
       [Key]
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }



    }
}