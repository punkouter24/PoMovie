using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoMovie.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Year { get; set; }

        [Required]
        public string UserId { get; set; } // Relation to ASP.NET Core Identity User

        // Additional fields like Rating can be added here
    }
}