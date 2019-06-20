using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private EFDBContext _context;
        //private IDirectoriesRepository _directoriesRepository;
        private DataManager _dataManager;

        public HomeController(/*EFDBContext context, IDirectoriesRepository directoriesRepository,*/ DataManager dataManager)
        {
            //_context = context;
            //_directoriesRepository = directoriesRepository;
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            //var helloModel = new HelloModel() {HelloMessage = "Hello world"};
            //var dirs = _context.Directories.Include(x => x.Materials).ToList();
            //var dirs = _directoriesRepository.GetAllDirectories(true).ToList();
            var dirs = _dataManager.DirectoriesRepository.GetAllDirectories(true).ToList();
            return View(dirs);
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
