using AmiFlota.Models;
using AmiFlota.Services;
using AmiFlota.Utilities;
using AppointmentScheduler.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AmiFlota.Controllers.api
{
    public class BookingApiController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingApiController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetAllAvailableCars")]
        public IActionResult GetAllAvailableCars(DateTime startDate, DateTime endDate)
        {
            CommonResponse<List<CarModel>> commonResponse = new CommonResponse<List<CarModel>>();
            try
            {
                commonResponse.dataenum = _bookingService.GetAllAvailableCars(startDate, endDate);
                commonResponse.status = ApiResponses.success_code;
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }
    }
}
