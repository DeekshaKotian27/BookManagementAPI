using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher?> GetPublisherById(int id);
        Task<int> CreatePublisher(Publisher publisher);
        Task<int> UpdatePublisher(int id, Publisher publisher);
        Task<int> DeletePublisher(int id);
    }
}
