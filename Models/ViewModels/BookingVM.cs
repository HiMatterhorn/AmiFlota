using System;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class BookingVM
    {
        // Booking properties

        public int? Id { get; set; }

        public string BookingUser { get; set; }

        public string CarVIN { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Destination { get; set; }

        public string ProjectCost { get; set; }

        public bool isApproved { get; set; }


    }
}
