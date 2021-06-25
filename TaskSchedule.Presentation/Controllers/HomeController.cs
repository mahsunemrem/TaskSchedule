using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Business.Utility;
using TaskSchedule.Presentation.Domain.Entities;
using TaskSchedule.Presentation.Domain.Intercaces;
using TaskSchedule.Presentation.Infrastructure;
using TaskSchedule.Presentation.Models;

namespace TaskSchedule.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Obsolete]
        public IActionResult Index()
        {
            
            return View();
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
