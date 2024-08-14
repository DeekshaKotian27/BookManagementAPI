using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        public async Task<Publisher> CreatePublisher(Publisher publisher)
        {
            return await _publisherRepository.CreatePublisher(publisher);   
        }

        public async Task<List<ResponseMultiplePublisherDTO>> GetAllAsync()
        {
            var getAllPublishers= await _publisherRepository.GetAllAsync();
            if (getAllPublishers == null)
            {
                return null;
            }
            
            var data=new List<ResponseMultiplePublisherDTO>();
            foreach(var publisher in getAllPublishers)
            {
                var publisherData = GetPublisherData(publisher);
                data.Add(publisherData);
            }
            return data;
        }

        private static ResponseMultiplePublisherDTO GetPublisherData(Publisher publisher)
        {
            var publisherData = new ResponseMultiplePublisherDTO();
            publisherData.PublisherID = publisher.PublisherId;
            publisherData.PublisherName = publisher.PublisherName;
            publisherData.PublisherPhoneNumber= publisher.PublisherPhoneNumber;
            publisherData.PublisherEmailId= publisher.PublisherEmailId;
            publisherData.PublisherAddress = publisher.PublisherAddress;
            var booksList = new List<ResponsePublisherBookDTO>();
            foreach (var book in publisher.Books)
            {
                var books = new ResponsePublisherBookDTO();
                books.BookID = book.BookId;
                books.Title = book.Title;
                books.AuthorName = book.Author.FirstName + " " + book.Author.LastName;
                books.CategoryName = book.Category.Name;
                booksList.Add(books);
            }
            publisherData.Books = booksList;
            return publisherData;
        }

        public async Task<ResponseMultiplePublisherDTO> GetPublisherById(int id)
        {
            var getPublisherByID= await _publisherRepository.GetPublisherById(id);
            if (getPublisherByID == null)
            {
                return null;
            }
            var publisherData = GetPublisherData(getPublisherByID);
            return publisherData;
        }

        public async Task<Publisher> UpdatePublisher(int id, Publisher publisher)
        {
            return await _publisherRepository.UpdatePublisher(id, publisher);
        }

        public Task<int> DeletePublisher(int id)
        {
            return _publisherRepository.DeletePublisher(id);
        }
    }
}
