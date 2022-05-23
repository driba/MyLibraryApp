using System.ComponentModel.DataAnnotations;

namespace Ispit.Books.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
