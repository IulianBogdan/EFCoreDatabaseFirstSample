using System;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksMongoController : ControllerBase
    {
        private readonly IDataRepository<Book> _dataRepository;

        public BooksMongoController(IDataRepository<Book> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _dataRepository.GetAll();
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine("calllllllll");
            var book = _dataRepository.Get(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        // POST: api/Authors
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(book);
            return CreatedAtRoute("GetBook", new { Id = book.Id }, null);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book is null.");
            }

            var bookToUpdate = _dataRepository.Get(id);
            if (bookToUpdate == null)
            {
                return NotFound("The book record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(book);
            return NoContent();
        }
    }
}