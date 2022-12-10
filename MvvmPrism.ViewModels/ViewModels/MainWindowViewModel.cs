using MvvmPrism.Models;
using MvvmPrism.ViewModels.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MvvmPrism.ViewModels.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private DelegateCommand? _errorToastCommand;
    private DelegateCommand? _successToastCommand;

    private readonly IBookService _bookService;

    public DelegateCommand ErrorToastCommand =>
        _errorToastCommand ??= new DelegateCommand(ShowErrorToastMessage);

    public DelegateCommand SuccessToastCommand =>
            _successToastCommand ??= new DelegateCommand(ShowSuccessToastMessage);


    public ObservableCollection<Book> Books { get; } = new();

    public Task Initialize { get; set; }

    public MainWindowViewModel(IBookService bookService)
    {
        _bookService = bookService;
        Initialize = LoadBooks();
    }

    /*
     * You can get the _bookService like this as well.
     * You can get the book service from the ContainerLocator instead of injecting in the constructor
     * Look up ServiceLocator pattern, it's great :)
     *
     * You'd do it this way if you needed to inject a service into a lower level ViewModel
     * I.e. if you created a ViewModel at run-time you could set it in the ctor
     */
    //public MainWindowViewModel()
    //{
    //    _bookService = ContainerLocator.Current.Resolve<IBookService>();
    //}

    private static void ShowErrorToastMessage()
    {
        NotificationMessageHandler.AddError("Error", "I'm an error toast message!");
    }

    private static void ShowSuccessToastMessage()
    {
        NotificationMessageHandler.AddSuccess("Success", "I'm a success toast message!");
    }

    private async Task LoadBooks()
    {
        IEnumerable<Book> books = await _bookService.GetBooksAsync();
        Books.AddRange(books);
    }
}