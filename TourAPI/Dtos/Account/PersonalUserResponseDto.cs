using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Account
{
    public class PersonalUserResponseDto
    {
        public string Name {get;set;}
        public int Id { get; set; }

        public int Sex { get; set; }
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
        public DateOnly BirthDay {get; set;}


    }
}