using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/booksSQL")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDataRepository<Book> _dataRepository;

        public BooksController(IDataRepository<Book> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/booksSQL/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var book = _dataRepository.Get(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        // GET: api/booksSQL/GetAll
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var books = _dataRepository.GetAll();

            return Ok(books);
        }

        // POST: api/booksSQL/AddBook
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
            
            return CreatedAtRoute(nameof(GetById), new { id = book.Id }, book);
        }

        // POST: api/booksSQL/UpdateBook
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
            
            return CreatedAtRoute(nameof(GetById), new { id = book.Id }, book);
        }

        // POST: api/booksSQL/DeleteBook
        [HttpPost]
        [Route("[action]")]
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
