using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Customer
{
    public class CustomerResultDto
    {
         public List<CustomerDto> Customers { get; set; }
         public int Total {get; set;}
    }
}