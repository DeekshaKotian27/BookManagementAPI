﻿using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

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
            if (publisher == null)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(publisher);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
