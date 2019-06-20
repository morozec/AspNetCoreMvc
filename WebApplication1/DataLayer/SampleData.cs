using System.Linq;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if (!EnumerableExtensions.Any(context.Directories))
            {
                context.Directories.Add(new Directory() {Title = "First directory", Html = "<b>Directory content 1</b>"});
                context.Directories.Add(new Directory() {Title = "Second directory", Html = "<b>Directory content 2</b>"});
                context.Directories.Add(new Directory() {Title = "Third directory", Html = "<b>Directory content 3</b>"});
                context.SaveChanges();

                context.Materials.Add(new Material()
                {
                    Title = "First material",
                    Html = "<i>Material content 1</i>",
                    DirectoryId = context.Directories.First().Id
                });
                context.Materials.Add(new Material()
                {
                    Title = "Second material",
                    Html = "<i>Material content 2</i>",
                    DirectoryId = context.Directories.First().Id
                });
                context.Materials.Add(new Material()
                {
                    Title = "Third material",
                    Html = "<i>Material content 3</i>",
                    DirectoryId = context.Directories.Last().Id
                });

                context.SaveChanges();
            }
        }
    }
}