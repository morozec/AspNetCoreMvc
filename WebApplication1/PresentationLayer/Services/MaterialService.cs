using System.Linq;
using BusinessLayer;
using DataLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class MaterialService
    {
        private DataManager _dataManager;

        public MaterialService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public MaterialViewModel MaterialDBModelToViewModel(int materialId)
        {
            var model = new MaterialViewModel()
            {
                Material = _dataManager.MaterialsRepository.GetMaterialById(materialId, true)
            };

            var dir = _dataManager.DirectoriesRepository.GetDirectoryById(model.Material.DirectoryId, true);
            var materialIndex = dir.Materials.FindIndex(m => m.Id == materialId);
            if (materialIndex != dir.Materials.Count - 1)
            {
                model.NextMaterial = dir.Materials.ElementAt(materialIndex + 1);
            }

            return model;
        }


        public MaterialEditModel GetMaterialEditModel(int materialId)
        {
            if (materialId != 0)
            {
                var dbMaterial = _dataManager.MaterialsRepository.GetMaterialById(materialId);
                var editModel = new MaterialEditModel()
                {
                    Id = dbMaterial.Id,
                    Title = dbMaterial.Title,
                    Html = dbMaterial.Html,
                    DirectoryId = dbMaterial.DirectoryId
                };
                return editModel;
            }
            return new MaterialEditModel();
        }

        public MaterialViewModel SaveMaterialEditModelToDb(MaterialEditModel editModel)
        {
            var material = editModel.Id != 0
                ? _dataManager.MaterialsRepository.GetMaterialById(editModel.Id)
                : new Material();

            material.Title = editModel.Title;
            material.Html = editModel.Html;
            material.DirectoryId = editModel.DirectoryId;
            _dataManager.MaterialsRepository.SaveMaterial(material);
            return MaterialDBModelToViewModel(material.Id);

        }

        public MaterialEditModel CreateNewMaterialEditModel(int directoryId)
        {
            return new MaterialEditModel() {DirectoryId = directoryId};
        }

    }
}