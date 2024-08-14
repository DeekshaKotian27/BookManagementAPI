using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetPublisherById(int id);
        Task<Publisher> CreatePublisher(Publisher publisher);
        Task<Publisher> UpdatePublisher(int id, Publisher publisher);
        Task<int> DeletePublisher(int id);
    }
}
