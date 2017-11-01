using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crudwithstoreprocedure.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string address { get; set; }
       
        public string State { get; set; }
        public string City { get; set; }


    }
}