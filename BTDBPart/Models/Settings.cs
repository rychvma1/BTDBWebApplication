using System.ComponentModel.DataAnnotations;

namespace BTDBPart.Models
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