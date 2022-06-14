﻿using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmiFlota.Controllers
{
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId;
        private readonly string userName;
        public BookingController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public IActionResult Search()
        {
            return View();
        }

        public PartialViewResult FilterCars(DateTime startDate, DateTime endDate)
        {
            AvailableCarsVM carList = _bookingService.GetAvailableCars(startDate, endDate);
            return PartialView("_FilteredCarsView", carList);
        }

        public PartialViewResult PendingBookingsCurrentUser()
        {
            var bookingsList = _bookingService.GetPendingBookingsByUserId(userId);
            return PartialView("_BookingList", bookingsList);
        }

        public  PartialViewResult ApprovedBookingsCurrentUser()
        {
            ;
            var bookingsList = _bookingService.GetApprovedBookingsByUserId(userId);
            return PartialView("_BookingList", bookingsList);
        }

        //GET - create
        public IActionResult BookingDetails(string VIN, DateTime startDate, DateTime endDate)
        {
            BookingVM viewModel = new BookingVM()
            {
                UserName = userName,
                RegistrationNumber = _bookingService.GetRegistrationNumberByCarVin(VIN),
                StartDate = startDate,
                EndDate = endDate,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookingDetails(BookingVM viewModel)
        {
            if (ModelState.IsValid)
            {
                _bookingService.BookCar(viewModel);
                return (RedirectToAction("Index"));
            }
            return View(viewModel);
        }



        public IActionResult UserDashboard()
        {
            return View();
        }


        public IActionResult Calendar()
        {
            ViewBag.CarList = _bookingService.GetAllCars();

            return View();
        }

    }
}
