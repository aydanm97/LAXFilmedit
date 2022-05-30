using System.ComponentModel.DataAnnotations;
namespace LAXFilm.Controllers
{
    public class SendMailDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
       
      
    }
}
