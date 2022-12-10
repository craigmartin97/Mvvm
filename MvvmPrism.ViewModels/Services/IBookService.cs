using MvvmPrism.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvmPrism.ViewModels.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksAsync();
}