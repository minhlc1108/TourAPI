using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Helpers
{
    public class TourQueryObject
    {
         public string? Id { get; set; } = null;

        public string? Name { get; set; } = null;
        public string? Detail {get; set;} = null;

        public string? City {get;set;} = null;

        // public string? categoryID {get;set} = null
    }
}