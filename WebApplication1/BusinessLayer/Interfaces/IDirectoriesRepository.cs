using System.Collections.Generic;
using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IDirectoriesRepository
    {
        IEnumerable<Directory> GetAllDirectories(bool includeMaterials = false);
        Directory GetDirectoryById(int directoryId, bool includeMaterials);
        void SaveDirectory(Directory directory);
        void DeleteDirectory(Directory directory);
    }
}