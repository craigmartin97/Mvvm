using System;
using MvvmBasic.ViewModels.Commands;

namespace MvvmBasic.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public RelayCommand PressMeCommand { get; }

    public MainWindowViewModel()
    {
        PressMeCommand = new RelayCommand(param =>
        {
            Console.WriteLine("Pressed!");
        });
    }
}