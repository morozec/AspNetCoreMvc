using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations
{
    public class EFMaterialsRepository : IMaterialsRepository
    {
        private EFDBContext _context;

        public EFMaterialsRepository(EFDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> GetAllMaterials(bool includeDirectory = false)
        {
            if (includeDirectory)
                return _context.Set<Material>().Include(m => m.Directory).AsNoTracking().ToList();
            return _context.Materials.ToList();
        }

        public Material GetMaterialById(int materialId, bool includeDirectory = false)
        {
            if (includeDirectory)
                return _context.Set<Material>().Include(m => m.Directory).AsNoTracking()
                    .FirstOrDefault(m => m.Id == materialId);
            return _context.Materials.FirstOrDefault(m => m.Id == materialId);
        }

        public void SaveMaterial(Material material)
        {
            if (material.Id == 0)
                _context.Materials.Add(material);
            else
                _context.Entry(material).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMaterial(Material material)
        {
            _context.Materials.Remove(material);
            _context.SaveChanges();
        }
    }
}