using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/sql/authorcontacts")]
    [ApiController]
    public class AuthorContactsController : ControllerBase
    {
        private readonly IDataRepository<AuthorContact> _dataRepository;

        public AuthorContactsController(IDataRepository<AuthorContact> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authorContacts = _dataRepository.GetAll();
            return Ok(authorContacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthorContact), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var authorContact = _dataRepository.Get(id);
            if (authorContact == null)
            {
                return NotFound("Author contact not found.");
            }

            return Ok(authorContact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorContact), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAuthorContact(AuthorContact authorContact)
        {
            if (authorContact == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(authorContact);

            return CreatedAtRoute(nameof(GetById), new { id = authorContact.AuthorContactId }, authorContact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorContact), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAuthorContact(AuthorContact authorContact)
        {
            if (authorContact == null)
            {
                return BadRequest("Author contact cannot be null");
            }

            var result = await _dataRepository.Update(authorContact);
            if (result != "Updated")
            {
                return BadRequest(result);
            }

            return CreatedAtRoute(nameof(GetById), new { id = authorContact.AuthorContactId }, authorContact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorContact), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAuthorContact(int id)
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
