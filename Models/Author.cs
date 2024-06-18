using System.ComponentModel.DataAnnotations;

namespace APBDTest2.Models;

public class Author
{
    [Key]
    public int IdAuthor { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    public ICollection<Book> Books { get; set; }
}