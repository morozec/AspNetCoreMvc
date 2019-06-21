using System.Collections.Generic;
using System.Linq;
using BusinessLayer;
using DataLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class DirectoryService
    {
        private DataManager _dataManager;
        private MaterialService _materialService;

        public DirectoryService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _materialService = new MaterialService(dataManager);
        }

        public List<DirectoryViewModel> GetDirectoriesList()
        {
            var dirs = _dataManager.DirectoriesRepository.GetAllDirectories();
            var modelsList = dirs.Select(d => DirectoryDBModelToViewModel(d.Id)).ToList();
            return modelsList;

        }

        public DirectoryViewModel DirectoryDBModelToViewModel(int directoryId)
        {
            var directory = _dataManager.DirectoriesRepository.GetDirectoryById(directoryId, true);
            var materialsViewModels = directory.Materials.Select(m => _materialService.MaterialDBModelToViewModel(m.Id)).ToList();
            return new DirectoryViewModel()
            {
                Directory = directory,
                Materials = materialsViewModels
            };
        }

        public DirectoryEditModel GetDirectoryEditModel(int directoryId = 0)
        {
            if (directoryId != 0)
            {
                var dirDb = _dataManager.DirectoriesRepository.GetDirectoryById(directoryId, true);
                var dirEditModel = new DirectoryEditModel()
                {
                    Id = dirDb.Id,
                    Title = dirDb.Title,
                    Html = dirDb.Html
                };
                return dirEditModel;

            }
           
            return new DirectoryEditModel();
        }

        public DirectoryViewModel SaveDirectoryEditModelToDb(DirectoryEditModel directoryEditModel)
        {
            var directoryDbModel = directoryEditModel.Id != 0
                ? _dataManager.DirectoriesRepository.GetDirectoryById(directoryEditModel.Id)
                : new Directory();
            directoryDbModel.Title = directoryEditModel.Title;
            directoryDbModel.Html = directoryEditModel.Html;

            _dataManager.DirectoriesRepository.SaveDirectory(directoryDbModel);

            return DirectoryDBModelToViewModel(directoryDbModel.Id);
        }


    }
}