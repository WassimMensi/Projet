using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newWebAPI.Models;
using newWebAPI.Models.DTOs;
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

    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        var test = await _context.Books.ToListAsync();
        var bookMap = _mapper.Map<IEnumerable<BookUpdateDTO>>(test);
        return Ok(bookMap);
    }

    [HttpGet("ById/{Id}", Name=nameof(GetBook))]
    public async Task<ActionResult<BookUpdateDTO>> GetBook(int Id)
    {
        try
        {
            Book book = await _context.Books.FindAsync(Id);
            Console.WriteLine(Id);

            if (book == null)
            {
                return NotFound();
            }

            var bookMap = _mapper.Map<BookUpdateDTO>(book);
            return Ok(bookMap);
        }
        catch (Exception ex)
        {
            // Log l'exception
            Console.WriteLine(ex);
            return StatusCode(500, "Une erreur interne du serveur s'est produite.");
        }
    }

    
    [HttpPost]
    public async Task<ActionResult<BookUpdateDTO>> PostBook([FromBody] BookUpdateDTO book)
    {
        if(book == null)
        {
            return BadRequest();
        }
        Book? bookAjouter = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
        var bookMap = _mapper.Map<Book>(book);
        if (bookAjouter != null)
        {
            return BadRequest("Book already exists");
        }
        else
        {
            await _context.Books.AddAsync(bookMap);
            await _context.SaveChangesAsync();
        }
        return CreatedAtRoute(
            routeName: nameof(GetBook),
            routeValues: new { id = bookMap.Id },
            value: bookMap);
        
    }

    


    [HttpPut("ChangeTitle/{id}/{title}")]
    public async Task<ActionResult<Book>> PutBookTitle(int id, string title)
    {
        Console.WriteLine(title);
        if(title == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Title = title;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangeGenre/{id}/{genre}")]
    public async Task<ActionResult<Book>> PutBookGenre(int id, string genre)
    {
        Console.WriteLine(genre);
        if(genre == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Genre = genre;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangeAuthor/{id}/{author}")]
    public async Task<ActionResult<Book>> PutBookAuthor(int id, string author)
    {
        Console.WriteLine(author);
        if(author == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Author = author;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangePrice/{id}/{price}")]
    public async Task<ActionResult<Book>> PutBookPrice(int id, float price)
    {
        Console.WriteLine(price);
        if(price == 0)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Price = price;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangeDescription/{id}/{description}")]
    public async Task<ActionResult<Book>> PutBookDesc(int id, string description)
    {
        Console.WriteLine(description);
        if(description == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Description = description;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangeRemarks/{id}/{remarks}")]
    public async Task<ActionResult<Book>> PutBookRemarks(int id, string remarks)
    {
        Console.WriteLine(remarks);
        if(remarks == null)
        {
            return BadRequest();
        }
        var book2 = await _context.Books.FindAsync(id);
        if(book2 == null)
        {
            return NotFound();
        }
        
        book2.Remarks = remarks;
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