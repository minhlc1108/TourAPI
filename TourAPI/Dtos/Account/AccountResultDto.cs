using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Account
{
    public class AccountResultDto
    {
         public List<AccountDto> Accounts { get; set; }
         public int Total {get; set;}
    }
}