﻿using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.BookingDetail
{
    public class BookingDetailDto
    {

        public int Id { get; set; }
           public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public int Price { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public string? Detail { get; set; } = String.Empty;
        public int Status { get; set; }
        public TourAPI.Models.Booking? Booking { get; set; }
        public TourAPI.Models.Customer? Customer { get; set; }
    }
}
