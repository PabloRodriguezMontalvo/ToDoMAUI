using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Win32;

using TodoApi.Models;

using ToDoLongoMatch.Views;

using static SQLite.SQLite3;

namespace ToDoLongoMatch.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        //private ITodoRepository _repositoryService;

        [ObservableProperty]
        bool cargando;

        [ObservableProperty]
        ObservableCollection<TodoItem> myItems;
      

        public ICommand CreateNewItem => new Command(NewItem);
        public ICommand Completed => new Command<TodoItem>((TodoItem item)=> { CompleteTask(item); });

        public ICommand Delete => new Command<TodoItem>((TodoItem item) => { DeleteTask(item); });
        public ICommand RefreshTable => new Command(LoadItems);
        
        public MainViewModel()
        {
            //_repositoryService = repositoryService;

            LoadItems();
        }

        [RelayCommand]
        private async void NewItem()
        {
            var CreateItem = new NewItemViewModel( this);
            var NewView = new CreateView(CreateItem);
            await App.Current.MainPage.Navigation.PushModalAsync(NewView);

        }

        private void CompleteTask(TodoItem CompletedItem) {
            if (CompletedItem != null)
            {
                if (CompletedItem.IsComplete)
                    CompletedItem.IsComplete = false;
                else
                {
                    CompletedItem.IsComplete = true;

                }
                App.ApiConnectors.Update(CompletedItem.Key, CompletedItem);
                //_repositoryService.Update(CompletedItem);
                LoadItems();
            }
        }
        private void DeleteTask(TodoItem CompletedItem)
        {
            if (CompletedItem != null)
            {
               App.ApiConnectors.Remove(CompletedItem);
                LoadItems();
            }
            
                 App.AlertSvc.ShowAlert("Deleted:", $"{CompletedItem.Name}");

             
           
        }
        [RelayCommand]
        public void LoadItems()
        {
          
                cargando=true;
                myItems = new ObservableCollection<TodoItem>(App.ApiConnectors.GetAll().Result);
                cargando=false;
                OnPropertyChanged("MyItems");
                OnPropertyChanged("Cargando");

  
          
        }
    }
}
