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

    


    [HttpPut]
    public async Task<ActionResult<Book>> PutBook([FromBody] Book book, int id)
    {
        if(book == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        book2.Title = book.Title;
        book2.Author = book.Author;
        book2.Genre = book.Genre;
        book2.Price = book.Price;
        book2.PublishDate = book.PublishDate;
        book2.Description = book.Description;
        book2.Remarks = book.Remarks;
        await _context.SaveChangesAsync();
        return CreatedAtRoute(
            routeName: nameof(GetBook),
            routeValues: new { id = book.Id },
            value: book);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if(book == null)
        {
            return NotFound();
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return Ok();
    }

}