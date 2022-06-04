using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AmiFlota.Controllers
{
    public class BookingController : Controller
    {

/*        private readonly AmiFlotaContext _context;*/
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
/*            _context = context;*/
            _bookingService = bookingService;

        }

        public ActionResult Search()
        {
            return View();
        }

       public PartialViewResult FilterCars()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            List<CarModel> carList = _bookingService.GetAllAvailableCars(startDate, endDate);
            return PartialView("_FilteredCarsView", carList);
        }

        public PartialViewResult Test()
        {
            return PartialView("_Test");
        }


        public ActionResult Book()
        {
            return View();
        }

/*        public List<BookingModel> GetAllCars()
        {

        }*/

    }
}
