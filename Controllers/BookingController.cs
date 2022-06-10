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

        public BookingController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<PartialViewResult> PendingBookingsCurrentUser()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookingsList = await _bookingService.GetPendingBookingsByUserId(currentUser);
            return PartialView("_BookingList", bookingsList);
        }

        public async Task<PartialViewResult> ApprovedBookingsCurrentUser()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookingsList = await _bookingService.GetApprovedBookingsByUserId(currentUser);
            return PartialView("_BookingList", bookingsList);
        }

        //GET - create
        public IActionResult BookingDetails(string VIN, DateTime startDate, DateTime endDate)
        {
            BookingModel model = new BookingModel()
            {
                StartDate = startDate,
                EndDate = endDate,
                CarVIN = VIN,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  will give the user's userId
            //userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            //email = User.FindFirstValue(ClaimTypes.Email);

            BookingVM viewModel = new BookingVM()
            {
                Booking = model,
                UserName = User.FindFirstValue(ClaimTypes.Name)
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
