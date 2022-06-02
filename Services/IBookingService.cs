using AmiFlota.Models;
using System;
using System.Collections.Generic;

namespace AmiFlota.Services
{
    public interface IBookingService
    {
        public List<CarModel> GetAllCars();
        public IEnumerable<CarModel> GetCarByVIN(string VIN);
        public List<CarModel> GetAllAvailableCars (DateTime startDate, DateTime endDate);

    }
}
