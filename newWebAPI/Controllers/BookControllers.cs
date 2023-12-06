using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newWebAPI.Controllers;
using newWebAPI.Models;

using AutoMapper;

namespace newWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]

    public async Task<IEnumerable<BookUpdateDTO>> Get()
    {
        var test = await _context.Books.ToListAsync();
        var bookMap = _mapper.Map<Book>(test);
        return bookMap;
    }

    [HttpGet("ById/{Id}", Name=nameof(GetBook))]
    public async Task<ActionResult<Book>> GetBook(int Id)
    {
        var book = await _context.Books.FindAsync(Id);
        return book == null ? NotFound() : book;
    }

    /* [HttpGet("ByAuthor/{Author}")]
    public async Task<ActionResult<Book>> GetBookByAuthor(string Author)
    {
        var book = _context.Books.Where(b=>Author==b.Author).ToListAsync();
        return book == null ? NotFound() : book;
    }
 */
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

    


    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> PutBook([FromBody] BookUpdateDTO book, int id)
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
        var bookMap = _mapper.Map<Book>(book);
        book2.Title = bookMap.Title;
        book2.Author = bookMap.Author;
        book2.Genre = bookMap.Genre;
        book2.Price = bookMap.Price;
        book2.PublishDate = bookMap.PublishDate;
        book2.Description = bookMap.Description;
        book2.Remarks = bookMap.Remarks;
        await _context.SaveChangesAsync();
        return NoContent();
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
        return NoContent();
    }

}