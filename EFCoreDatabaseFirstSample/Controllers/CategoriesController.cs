using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/sql/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataRepository<BookCategory> _dataRepository;

        public CategoriesController(IDataRepository<BookCategory> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var category = _dataRepository.Get(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<BookCategory>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var categories = _dataRepository.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookCategory), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory(BookCategory category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            await _dataRepository.Add(category);

            return CreatedAtRoute(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookCategory), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory(BookCategory category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }

            var result = await _dataRepository.Update(category);
            if (result != "Updated")
            {
                return BadRequest(result);
            }

            return CreatedAtRoute(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory(int id)
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
