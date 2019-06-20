using System.Collections.Generic;
using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IMaterialsRepository
    {
        IEnumerable<Material> GetAllMaterials(bool includeDirectory = false);
        Material GetMaterial(int materialId, bool includeDirectory);
        void SaveMaterial(Material material);
        void DeleteMaterial(Material material);
    }
}