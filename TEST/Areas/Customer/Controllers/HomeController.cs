using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TEST.DAO;
using TEST.Models;

namespace TEST.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var schedules = _unitOfWork.vaccineScheduleRepository.GetAll("Vaccine");
            return View(schedules);
        }

        public IActionResult Details(int id)
        {
            var schedules = _unitOfWork.vaccineScheduleRepository.GetEntities(i => i.Id == id, "Vaccine.Type").FirstOrDefault();
            if (schedules is null)
            {
                return NotFound();
            }
            return View(schedules);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}