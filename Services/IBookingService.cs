using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface IBookingService
    {
        public Task<List<CarModel>> GetAllCars();
        public IEnumerable<CarModel> GetCarByVIN(string VIN);
        public Task<AvailableCarsVM> GetAvailableCars (DateTime startDate, DateTime endDate);

        public void BookCar(BookingVM bookingVM);

    }
}
