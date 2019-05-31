using System;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/sql/bookauthors")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IDataRepository<BookAuthors> _dataRepository;

        public BookAuthorsController(IDataRepository<BookAuthors> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("[action]/{id:int}", Name = "GetBookAuthorsById")]
        [ProducesResponseType(typeof(BookAuthors), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBookAuthorsById(int id)
        {
            var author = _dataRepository.Get(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            return Ok(author);
        }
        
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookAuthors), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBookAuthors(BookAuthors author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(author);
            
            return CreatedAtRoute("GetBookAuthorsById", new { id = author.AuthorId }, author);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookAuthors), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAuthor(BookAuthors author)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("[action]/{id:int}")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBookAuthor(int id)
        {
            throw new NotImplementedException();
        }
    }
}