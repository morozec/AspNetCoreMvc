using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class DataManager
    {
        public IDirectoriesRepository DirectoriesRepository { get; }
        public IMaterialsRepository MaterialsRepository { get; }

        public DataManager(IDirectoriesRepository directoriesRepository, IMaterialsRepository materialsRepository)
        {
            DirectoriesRepository = directoriesRepository;
            MaterialsRepository = materialsRepository;
        }
    }
}