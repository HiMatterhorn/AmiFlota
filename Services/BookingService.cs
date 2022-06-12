using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUserModel> _userManager;

        public BookingService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
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

        public bool ValidateBooking(DateTime startDate, DateTime endDate, string carVin)
        {

            //TODO Check if carVin exists in database?
            var bookings = _db.Bookings
                .Where(x => x.CarVIN.Equals(carVin))
                .Where(s => s.StartDate <= endDate)
                .Where(e => e.EndDate >= startDate).ToList();

            if (bookings.Count() == 0)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<CarModel> GetCarByVIN(string VIN)
        {
            return _db.Cars.Where(x => x.VIN.Equals(VIN));
        }

        public void BookCar(BookingVM bookingVM)
        {
            //Validate booking
            bool validatationResult = ValidateBooking(bookingVM.Booking.StartDate, bookingVM.Booking.EndDate, bookingVM.Booking.CarVIN);

            //Save to database
            if (validatationResult)
            {
                _db.Bookings.Add(bookingVM.Booking);
                _db.SaveChanges();
            }

            else
            {
                //TODO Booking not validated successfully
                Console.WriteLine("booking not validated");
            }
        }

        public async Task<IEnumerable<BookingModel>> GetPendingBookingsByUserId(string userId)
        {
            var results = await _db.Bookings.
                Where(x => x.UserId == userId).
                Where(a => a.isApproved == false).
                ToListAsync();

            return results;
        }

        public async Task<IEnumerable<BookingModel>> GetApprovedBookingsByUserId(string userId)
        {
            var results = await _db.Bookings.
                Where(x => x.UserId == userId).
                Where(a => a.isApproved == true).
                ToListAsync();

            return results;
        }

        public List<BookingVM> BookingsByCarVIN(string carVIN)
        {
            return _db.Bookings.Where(x => x.CarVIN == carVIN).ToList().Select(c => new BookingVM()
            {
                //TODO Change BookingVM structure
            }).ToList();
        }

    }
}
