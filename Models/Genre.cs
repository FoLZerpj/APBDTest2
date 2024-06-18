using System.ComponentModel.DataAnnotations;

namespace APBDTest2.Models;

public class Genre
{
    [Key]
    public int IdGenre { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    
    public ICollection<Book> Books { get; set; }
}