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

        [HttpGet]
        public IActionResult PageEditor(int pageId, PageEnums.PageType pageType, int directoryId = 0)
        {
            PageEditModel editModel;
            switch (pageType)
            {
                case PageEnums.PageType.Directory:
                    editModel = pageId != 0
                        ? _servicesManager.DirectoryService.GetDirectoryEditModel(pageId)
                        : _servicesManager.DirectoryService.CreateNewDirectoryEditModel();
                    break;
                case PageEnums.PageType.Material:
                    editModel = pageId != 0
                        ? _servicesManager.MaterialService.GetMaterialEditModel(pageId)
                        : _servicesManager.MaterialService.CreateNewMaterialEditModel(directoryId);
                    break;
                default:
                    editModel = null;
                    break;
            }

            ViewBag.PageType = pageType;
            return View(editModel);
        }

        [HttpPost]
        public IActionResult SaveDirectory(DirectoryEditModel model)
        {
            _servicesManager.DirectoryService.SaveDirectoryEditModelToDb(model);
            return RedirectToAction("PageEditor",
                "Page",
                new {pageId = model.Id, pageType = PageEnums.PageType.Directory});
        }

        [HttpPost]
        public IActionResult SaveMaterial(MaterialEditModel model)
        {
            _servicesManager.MaterialService.SaveMaterialEditModelToDb(model);
            return RedirectToAction("PageEditor",
                "Page",
                new {pageId = model.Id, pageType = PageEnums.PageType.Material});
        }
    }
}