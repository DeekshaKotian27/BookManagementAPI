using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public interface IPublisherService
    {
        Task<List<ResponseMultiplePublisherDTO>> GetAllAsync();
        Task<ResponseMultiplePublisherDTO> GetPublisherById(int id);
        Task<Publisher> CreatePublisher(Publisher publisher);
        Task<Publisher> UpdatePublisher(int id,Publisher publisher);
        Task<int> DeletePublisher(int id);
    }
}
