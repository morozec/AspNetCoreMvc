using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace PresentationLayer.Models
{
    public class MaterialViewModel : PageViewModel
    {
        public Material Material { get; set; }
        public Material NextMaterial { get; set; }

    }

    public class MaterialEditModel : PageEditModel
    {
        [Required]
        public int DirectoryId { get; set; }
    }
}