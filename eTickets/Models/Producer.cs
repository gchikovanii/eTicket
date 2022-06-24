using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="Profile picture is required!")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required!")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Full name must be between 2 and 50")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required!")]

        public string Bio { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
