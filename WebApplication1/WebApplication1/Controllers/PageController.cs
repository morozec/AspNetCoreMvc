using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using DataLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace WebApplication1.Controllers
{
    public class PageController : Controller
    {
        private DataManager _dataManager;
        private ServicesManager _servicesManager;

        public PageController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _servicesManager = new ServicesManager(dataManager);
        }

        public IActionResult Index(int pageId, PageEnums.PageType pageType)
        {
            PageViewModel viewModel;
            switch (pageType)
            {
                case PageEnums.PageType.Directory:
                    viewModel = _servicesManager.DirectoryService.DirectoryDBModelToViewModel(pageId);
                    break;
                case PageEnums.PageType.Material:
                    viewModel = _servicesManager.MaterialService.MaterialDBModelToViewModel(pageId);
                    break;
                default:
                    viewModel = null;
                    break;
            }

            ViewBag.PageType = pageType;
            return View(viewModel);
        }

        public IActionResult PageEditor(int pageId, PageEnums.PageType pageType)
        {
            PageEditModel editModel;
            switch (pageType)
            {
                case PageEnums.PageType.Directory:
                    editModel = _servicesManager.DirectoryService.GetDirectoryEditModel(pageId);
                    break;
                case PageEnums.PageType.Material:
                    editModel = _servicesManager.MaterialService.GetMaterialEditModel(pageId);
                    break;
                default:
                    editModel = null;
                    break;
            }

            ViewBag.PageType = pageType;
            return View(editModel);
        }
    }
}