using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/sql/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDataRepository<Book> _dataRepository;

        public BooksController(IDataRepository<Book> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("[action]/{id:int}", Name = "GetBookById")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBookById(int id)
        {
            var book = _dataRepository.Get(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        // GET: api/sql/books/GetAll
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var books = _dataRepository.GetAll();

            return Ok(books);
        }

        // GET: api/sql/books/GetFictionBooks
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public IActionResult GetFictionBooks()
        {
            var books = _dataRepository.GetFictionBooks();

            return Ok(books);
        }

        // POST: api/sql/books/AddBook
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(book);
            
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }

        // POST: api/sql/books/UpdateBook
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null");
            }

            var result = await _dataRepository.Update(book);
            if (result != "Updated")
            {
                return BadRequest(result);
            }
            
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }

        // POST: api/sql/books/DeleteBook
        [HttpPost]
        [Route("[action]/{id:int}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }

            var result = await _dataRepository.Delete(id);
            if (result != "Deleted")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
