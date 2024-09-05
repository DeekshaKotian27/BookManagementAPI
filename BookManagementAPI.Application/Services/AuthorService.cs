using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;

namespace BookManagementAPI.Application.Services
{
    public class AuthorService : IAuthorService
    {
        
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<int> CreateAsync(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
            };
            return await _authorRepository.CreateAsync(author);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }

        public async Task<List<ResponseAuthorDTO>> GetAllAsync()
        {
            var getAllAuthors= await _authorRepository.GetAllAsync();
            if (getAllAuthors == null)
            {
                return null;
            }
            var listAuthors=new List<ResponseAuthorDTO>();
            foreach (var author in getAllAuthors)
            {
                var authorDTO = GetAuthorData(author);
                listAuthors.Add(authorDTO);
            }
            return listAuthors;
        }

        public async Task<ResponseAuthorDTO?> GetByIdAsync(int id)
        {
            var authorByID = await _authorRepository.GetByIdAsync(id);
            if (authorByID == null)
            {
                return null;
            }
            var result = GetAuthorData(authorByID);
            return result;
        }

        private static ResponseAuthorDTO GetAuthorData(Author authorByID)
        {
            var books = new List<ResponseAuthorBookDTO>();
            foreach (var book in authorByID.Books)
            {
                var item = new ResponseAuthorBookDTO()
                {
                    BookID = book.BookId,
                    Title = book?.Title,
                    PublisherName = book?.Publisher?.PublisherName,
                    CategoryName = book?.Category?.Name,
                };
                books.Add(item);
            }
            var result = new ResponseAuthorDTO()
            {
                AuthorId = authorByID.AuthorId,
                FirstName = authorByID.FirstName,
                LastName = authorByID.LastName,
                Books = books
            };
            return result;
        }

        public async Task<int> UpdateAsync(int id, AuthorDTO authorDTO)
        {
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
            };
            return await _authorRepository.UpdateAsync(id, author);
        }

    }
}
