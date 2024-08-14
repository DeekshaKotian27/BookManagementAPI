using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using BookManagementAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Infrastructure.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        public readonly AuthorDBContext _authorDBContext;
        public PublisherRepository(AuthorDBContext authorDBContext)
        {
            _authorDBContext = authorDBContext;
        }
        public async Task<Publisher> CreatePublisher(Publisher publisher)
        {
            await _authorDBContext.Publisher.AddAsync(publisher);
            await _authorDBContext.SaveChangesAsync();
            return publisher;
        }

        public async Task<int> DeletePublisher(int id)
        {
            var data= await _authorDBContext.Publisher.FindAsync(id);
            if(data != null)
            {
                _authorDBContext.Publisher.Remove(data);
                return await _authorDBContext.SaveChangesAsync();
            }
            return -1;
        }

        public  Task<List<Publisher>> GetAllAsync()
        {
            return  _authorDBContext.Publisher
                .Include(b=>b.Books).ThenInclude(a=>a.Author)
                .Include(b=>b.Books).ThenInclude(c=>c.Category)
                .ToListAsync();
        }

        public async Task<Publisher> GetPublisherById(int id)
        {
            var publisher = await _authorDBContext.Publisher
                .Include(b => b.Books).ThenInclude(a => a.Author)
                .Include(b => b.Books).ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(p => p.PublisherId == id);
            return publisher;
        }

        public async Task<Publisher> UpdatePublisher(int id, Publisher publisher)
        {
            var publisherData = await _authorDBContext.Publisher.FindAsync(id);
            if (publisherData == null)
            {
                return null;
            }
            publisherData.PublisherName = publisher.PublisherName;
            publisherData.PublisherPhoneNumber= publisher.PublisherPhoneNumber;
            publisherData.PublisherAddress = publisher.PublisherAddress;
            publisherData.PublisherEmailId= publisher.PublisherEmailId;
            _authorDBContext.Publisher.Update(publisherData);
            await _authorDBContext.SaveChangesAsync();
            return publisherData;
        }
    }
}
