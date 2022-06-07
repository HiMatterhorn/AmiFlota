using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public class BookingService : IBookingService
    {
        private readonly AmiFlotaContext _db;

        public BookingService(AmiFlotaContext db)
        {
            _db = db;
        }


        public async Task<AvailableCarsVM> GetAvailableCars(DateTime startDate, DateTime endDate)
        {

            List<CarModel> availableCars = await GetCarsInDates(startDate, endDate);

            AvailableCarsVM availableCarsVM = new AvailableCarsVM()
            {
                AvailableCars = availableCars,
                StartDate = startDate,
                EndDate = endDate,
            };

            return availableCarsVM;
       }

        public async Task<List<CarModel>> GetAllCars()
        {
            List<CarModel> cars = await _db.Cars.ToListAsync();

            return cars;
        }

        public async Task<List<CarModel>> GetCarsInDates(DateTime startDate, DateTime endDate)
        {
            List<CarModel> availableCars = new List<CarModel>();
            IEnumerable<CarModel> cars = await GetAllCars();

            foreach (var car in cars)
            {
                var bookings = _db.Bookings
                    .Where(x => x.CarVIN.Equals(car.VIN))
                    .Where(s => s.StartDate <= endDate)
                    .Where(e => e.EndDate >= startDate).ToList();


                if (bookings.Count() == 0)
                {
                    availableCars.Add(car);
                }
            };
            return availableCars;
        }

        public IEnumerable<CarModel> GetCarByVIN(string VIN)
        {
            return _db.Cars.Where(x => x.VIN.Equals(VIN));
        }

        public void BookCar (BookingVM bookingVM)
        {
/*            _db.Bookings.Add(bookingVM.Booking);
            _db.SaveChanges();*/
        }

    }
}
