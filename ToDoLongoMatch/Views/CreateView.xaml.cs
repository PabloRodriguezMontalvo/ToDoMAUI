using ToDoLongoMatch.ViewModels;

namespace ToDoLongoMatch.Views;

public partial class CreateView : ContentPage
{
	public CreateView()
	{
		InitializeComponent();
	}
	public CreateView(NewItemViewModel context)
	{
        BindingContext = context;
        InitializeComponent();

    }
}