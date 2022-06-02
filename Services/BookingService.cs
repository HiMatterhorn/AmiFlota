using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmiFlota.Services
{
    public class BookingService : IBookingService
    {
        private readonly AmiFlotaContext _db;

        public BookingService(AmiFlotaContext db)
        {
            _db = db;
        }


        public List<CarModel> GetAllAvailableCars(DateTime startDate, DateTime endDate)
        {
            List<CarModel> availableCars = new List<CarModel>();  
            
            //Get list of all cars
            IEnumerable<CarModel> cars = GetAllCars();

            foreach (var car in cars)
            {
                var bookings = _db.Bookings
                    .Where(x => x.CarVIN.Equals(car.VIN))
                    .Where(s => s.StartDate <= endDate)
                    .Where(e => e.EndDate >= startDate).ToList();


                if (bookings.Count() == 0 )
                {
                    availableCars.Add(car);
                }
            };

            return availableCars;

        }

        public List<CarModel> GetAllCars()
        {
            List<CarModel> cars = _db.Cars.ToList();
            return cars;
        }

        public IEnumerable<CarModel> GetCarByVIN(string VIN)
        {
            return _db.Cars.Where(x => x.VIN.Equals(VIN));
        }

    }
}
