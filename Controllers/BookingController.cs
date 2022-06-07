using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Controllers
{
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<PartialViewResult> FilterCars(DateTime startDate, DateTime endDate)
        {
            AvailableCarsVM carList = await _bookingService.GetAvailableCars(startDate, endDate);
            return PartialView("_FilteredCarsView", carList);
        }

        //GET - create
        public IActionResult BookingDetails(string VIN, DateTime startDate, DateTime endDate)
        {
            BookingModel model = new BookingModel()
            {
                StartDate = startDate,
                EndDate = endDate,
                CarVIN = VIN,
                BookingUser = "empty",
            };

            BookingVM viewModel = new BookingVM()
            {
                Booking = model
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookingDetails(BookingVM bookingVM)
        {

            if (ModelState.IsValid)
            {
            _bookingService.BookCar(bookingVM);
            return (RedirectToAction("Index"));
            }
            /* TODO Get by parameter: VIN, startDate, endDate fom _FilteredCarsView.cshtml
             * Get by dependency injection userName
             * Return a view to fill Destination, Project Cost
             */

            return View(bookingVM);
        }

        public IActionResult Book()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
