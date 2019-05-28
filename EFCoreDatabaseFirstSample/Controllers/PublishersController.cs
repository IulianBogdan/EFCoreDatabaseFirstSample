using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IDataRepository<Publisher> _dataRepository;

        public PublishersController(IDataRepository<Publisher> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authors = _dataRepository.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Publisher), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var publisher = _dataRepository.Get(id);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }

            return Ok(publisher);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Publisher), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(publisher);
            
            return CreatedAtRoute(nameof(GetById), new { id = publisher.Id }, publisher);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Publisher), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest("Book cannot be null");
            }

            var result = await _dataRepository.Update(publisher);
            if (result != "Updated")
            {
                return BadRequest(result);
            }
            
            return CreatedAtRoute(nameof(GetById), new { id = publisher.Id }, publisher);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePublisher(int id)
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