using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDTest2.Models;

public class Book
{
    [Key]
    public int IdBook { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime ReleaseDate { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    public ICollection<Genre> Genres { get; set; }
    
    [ForeignKey("PublishingHouseId")]
    public PublishingHouse PublishingHouse { get; set; }
}