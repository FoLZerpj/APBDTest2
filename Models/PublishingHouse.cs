using System.ComponentModel.DataAnnotations;

namespace APBDTest2.Models;

public class PublishingHouse
{
    [Key]
    public int IdPublishingHouse { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Country { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string City { get; set; }

    //public ICollection<Book> Books { get; set; }
}