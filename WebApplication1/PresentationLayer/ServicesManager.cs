using BusinessLayer;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class ServicesManager
    {
        public DirectoryService DirectoryService { get; set; }
        public MaterialService MaterialService { get; set; }

        public ServicesManager(DataManager dataManager)
        {
           DirectoryService = new DirectoryService(dataManager);
           MaterialService = new MaterialService(dataManager);
        }
    }
}