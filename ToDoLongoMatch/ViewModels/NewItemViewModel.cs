using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using TodoApi.Models;

namespace ToDoLongoMatch.ViewModels
{
    public partial class NewItemViewModel : ObservableObject
    {

        private MainViewModel _viewmodel;
        [ObservableProperty]
        string newItemsText;

        public NewItemViewModel(MainViewModel viewmodel)
        {
            _viewmodel= viewmodel;
        }
        public ICommand AddNewItem => new Command(NewItem);

        private async void NewItem()
        {
            var NewItem = new TodoItem();
            NewItem.Name = newItemsText;
            NewItem.IsComplete = false;
            NewItem.Key = Guid.NewGuid().ToString();
            await App.ApiConnectors.Create(NewItem);
            _viewmodel.LoadItems();
            await App.Current.MainPage.Navigation.PopModalAsync();

        }
    }
}
