using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        public readonly IPublisherService _publisherService;
        private readonly IHttpContextAccessor _contextAccessor;
        public PublisherController(IPublisherService publisherService,IHttpContextAccessor contextAccessor)
        {
            _publisherService = publisherService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Route("GetAllPublisher")]
        public async Task<IActionResult> GetAllAsync()
        {
            //using httpcontextAccesor to get the data for http request and response. Used mostly in the services and repositories.
            var httpcontext=_contextAccessor.HttpContext;
            var publisher= await _publisherService.GetAllAsync();
            if (publisher == null)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(publisher);
        }

        [HttpGet]
        [Route("GetPublisherById/{id}")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var publisher=await _publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound($"There is no data with ID {id}");
            }
            return Ok(publisher);
        }

        [HttpPost]
        [Route("CreatePublisher")]
        public async Task<IActionResult> CreateAsync(PublisherDTO publisherDTO)
        {
            if(publisherDTO==null)
            {
                return BadRequest();
            }
            var publisher = await _publisherService.CreatePublisher(publisherDTO);
            if (publisher == -1)
            {
                return Conflict("The resource already exists.");
            }
            if (publisher == -2)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return StatusCode(201, "Publisher created successfully");
        }

        [HttpPut]
        [Route("UpdatePublisher/{id}")]
        public async Task<IActionResult> Update(int id, PublisherDTO publisherDTO)
        {
            if (publisherDTO == null)
            {
                return BadRequest();
            }           
            var publisher = await _publisherService.UpdatePublisher(id,publisherDTO);
            if (publisher == 1)
            {
                return Ok();
            }
            else if (publisher == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("DeletePublisher/{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var data=await _publisherService.DeletePublisher(id);
            if (data == 1)
            {
                return Ok("deleted succesfully book");
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
