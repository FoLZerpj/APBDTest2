using APBDTest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBDTest2.Controllers;

[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{

    private readonly AppDbContext _context;
    public BookController(AppDbContext context)
    { 
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddBookInfo book)
    {
        try
        {
            var publishingHouse = await _context.PublishingHouses.FindAsync(book.PublishingHouseId);
            if (publishingHouse == null)
            {
                return NotFound($"Publishing house with id {book.PublishingHouseId} does not exist");
            }

            var dbGenres = new List<Models.Genre>();
            foreach(var genre in book.Genres)
            {
                var existingGenre = await _context.Genres.FindAsync(genre.Id) ?? (await _context.Genres.AddAsync(
                    new Models.Genre
                    {
                        Name = genre.Name,
                        Books = new List<Book>()
                    })).Entity;
                dbGenres.Add(existingGenre);
            }

            var authors = new List<Author>();
            foreach (var authorId in book.AuthorsIds)
            {
                var author = await _context.Authors.FindAsync(authorId);

                if (author == null)
                {
                    return NotFound($"Author with id {authorId} does not exist");
                }
                
                authors.Add(author);
            }
            await Console.Out.WriteLineAsync($"Test1");

            await _context.Books.AddAsync(new Book
            {
                Name = book.BookName,
                ReleaseDate = DateTime.Now,
                Authors = authors,
                Genres = dbGenres,
                PublishingHouse = publishingHouse
            });

            await _context.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception e) { return BadRequest(e); }
    }

}

public record AddBookInfo(string BookName, int PublishingHouseId, int[] AuthorsIds, Genre[] Genres);
public record Genre(int Id, string Name);