using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newWebAPI.Controllers;
using newWebAPI.Models;

namespace newWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<IEnumerable<Book>> Get()
    {
        return await _context.Books.ToListAsync();
    }

    [HttpGet("{Id}", Name=nameof(GetBook))]
    public async Task<ActionResult<Book>> GetBook(int Id)
    {
        var book = await _context.Books.FindAsync(Id);
        return book == null ? NotFound() : book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
    {
        if(book == null)
        {
            return BadRequest();
        }
        Book? addedBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
        if (addedBook != null)
        {
            return BadRequest("Book already exists");
        }
        else
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        return CreatedAtRoute(
            routeName: nameof(GetBook),
            routeValues: new { id = book.Id },
            value: book);
        
    }

    /*[HttpPut]
    public async Task<ActionResult<Book>> PutBook([FromBody] Book book, int id)
    {
        var book = await _context.Books.FindAsync(Id);
        

    }*/

}