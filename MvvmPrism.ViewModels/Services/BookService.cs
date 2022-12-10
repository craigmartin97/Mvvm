using MvvmPrism.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvmPrism.ViewModels.Services;

public class BookService : IBookService
{
    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await Task.FromResult(new List<Book>
        {
            new()
            {
                Id = 1,
                Name = "Game Of Thrones"
            },
            new()
            {
                Id = 1,
                Name = "A clash of kings"
            },
            new()
            {
                Id = 1,
                Name = "A storm of swords"
            }
        });
    }
}