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

    private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var MyList = (ListView)sender;
		TodoItem MyItem = (TodoItem)MyList.SelectedItem;

        if (MyItem.IsComplete == true)
            MyItem.IsComplete = false;
		else
		{
            MyItem.IsComplete = true;
		}
		App.ApiConnectors.Update(MyItem.Key, MyItem);
		OnPropertyChanged(("MyItems"));
    }
}

