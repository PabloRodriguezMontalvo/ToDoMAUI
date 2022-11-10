using TodoApi.Models;

using ToDoLongoMatch.ViewModels;

namespace ToDoLongoMatch;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}
	public MainPage(MainViewModel context)
	{
		BindingContext= context;
        InitializeComponent();

    }

    private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var MyList = (ListView)sender;
		TodoItem MyItem = (TodoItem)MyList.SelectedItem;

        if (MyItem.IsComplete == true)
            MyItem.IsComplete = false;
		else
		{
            MyItem.IsComplete = true;
		}
		await App.ApiConnectors.Update(MyItem.Key, MyItem);
		OnPropertyChanged(("MyItems"));
		MyList.ItemsSource = App.ApiConnectors.GetAll().Result;
        OnPropertyChanged(("MyList"));

    }
}

