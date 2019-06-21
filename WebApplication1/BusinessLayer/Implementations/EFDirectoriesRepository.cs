using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations
{
    public class EFDirectoriesRepository : IDirectoriesRepository
    {
        private EFDBContext _context;

        public EFDirectoriesRepository(EFDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Directory> GetAllDirectories(bool includeMaterials = false)
        {
            if (includeMaterials)
                return _context.Set<Directory>().Include(d => d.Materials).AsNoTracking().ToList();
            return _context.Directories.ToList();
        }

        public Directory GetDirectoryById(int directoryId, bool includeMaterials = false)
        {
            if (includeMaterials)
                return _context.Set<Directory>().Include(d => d.Materials).AsNoTracking()
                    .FirstOrDefault(d => d.Id == directoryId);
            return _context.Directories.FirstOrDefault(d => d.Id == directoryId);
        }

        public void SaveDirectory(Directory directory)
        {
            if (directory.Id == 0)
                _context.Directories.Add(directory);
            else
                _context.Entry(directory).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDirectory(Directory directory)
        {
            _context.Directories.Remove(directory);
            _context.SaveChanges();
        }
    }
}