using System.ComponentModel.DataAnnotations;

namespace ProjektApp.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }  

        public string? UserId { get; set; }  

    }
}
