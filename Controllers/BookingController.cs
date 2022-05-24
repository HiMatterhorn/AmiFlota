using AmiFlota.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmiFlota.Controllers
{
    public class BookingController : Controller
    {

        private readonly AmiFlotaContext _context;

        public BookingController(AmiFlotaContext context)
        {
            _context = context;
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Book()
        {
            return View();
        }

    }
}
