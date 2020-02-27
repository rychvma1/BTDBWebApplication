using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Settings
    {
        [Required (ErrorMessage = "Path to directory is required.")]
        public string DirPath { get; set; }

        public Settings()
        {
            
        }
        
        public Settings(Settings other)
        {
            DirPath = other.DirPath;
        }
    }
}