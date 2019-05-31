using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/sql/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IDataRepository<Author> _dataRepository;

        public AuthorsController(IDataRepository<Author> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get()
        {
            var authors = _dataRepository.GetAll();
            return Ok(authors);
        }

        [HttpGet]
        [Route("[action]/{id:int}", Name = "GetAuthorsById")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAuthorsById(int id)
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
        [ProducesResponseType(typeof(Author), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(author);
            
            return CreatedAtRoute("GetAuthorsById", new { id = author.Id }, author);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest("Author cannot be null");
            }

            var result = await _dataRepository.Update(author);
            if (result != "Updated")
            {
                return BadRequest(result);
            }
            
            return CreatedAtRoute("GetAuthorsById", new { id = author.Id }, author);
        }

        [HttpPost]
        [Route("[action]/{id:int}")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAuthor(int id)
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