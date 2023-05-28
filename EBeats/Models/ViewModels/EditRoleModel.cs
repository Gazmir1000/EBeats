using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EBeats.Models.ViewModels
{
    public class EditRoleModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string? Name { get; set; }
    }
}
