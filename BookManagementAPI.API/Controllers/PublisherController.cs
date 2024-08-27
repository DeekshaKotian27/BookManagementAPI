using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        public readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var publisher= await _publisherService.GetAllAsync();
            return Ok(publisher);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var publisher=await _publisherService.GetPublisherById(id);
            if(publisher==null)
            {
                return BadRequest();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PublisherDTO publisherDTO)
        {
            if(publisherDTO==null)
            {
                return BadRequest();
            }
            var publisherData = new Publisher()
            {
                PublisherName = publisherDTO.PublisherName,
                PublisherAddress = publisherDTO.PublisherAddress,
                PublisherEmailId = publisherDTO.PublisherEmailId,
                PublisherPhoneNumber = publisherDTO.PublisherPhoneNumber,
            };
            var publisher = await _publisherService.CreatePublisher(publisherData);
            return Ok(publisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PublisherDTO publisherDTO)
        {
            if (publisherDTO == null)
            {
                return BadRequest();
            }
            var publisherData = new Publisher()
            {
                PublisherName = publisherDTO.PublisherName,
                PublisherAddress = publisherDTO.PublisherAddress,
                PublisherEmailId = publisherDTO.PublisherEmailId,
                PublisherPhoneNumber = publisherDTO.PublisherPhoneNumber,
            };
            var publisher = await _publisherService.UpdatePublisher(id,publisherData);
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var data=await _publisherService.DeletePublisher(id);
            if (data != -1)
            {
                return Ok("Sucessfullt deleted publisher");
            }
            return BadRequest();
        }
    }
}
