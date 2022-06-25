using eTickets.Data.Base;
using eTickets.Data.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM 
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
        [Display(Name = "Price ")]
        [Required(ErrorMessage = "Price is required!")]
        public double Price { get; set; }
        [Display(Name = "Movie poster url ")]
        [Required(ErrorMessage = "Movie poster url is required!")]
        public string ImageUrl { get; set; }
        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Start Date required!")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "End Date required!")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "Movie Category is required!")]
        public MovieCategory MovieCategory { get; set; }
        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Movie actors are required!")]
        public List<int> ActorIds { get; set; }
        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie Cinema is required!")]
        public int CinemaId { get; set; }
        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie Producer is required!")]
        public int ProducerId { get; set; }
    }
}
