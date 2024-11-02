using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Error
{
    public class ErrorResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}