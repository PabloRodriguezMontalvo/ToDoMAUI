using ToDoLongoMatch.Services;
using ToDoLongoMatch.Utils;

namespace ToDoLongoMatch;

public partial class App : Application
{
    public static IServiceProvider Services;
    public static IAlertService AlertSvc;
    private static ApiConnectors _apiConnector = new ApiConnectors();
    public static ApiConnectors ApiConnectors { get { return _apiConnector; } set {_apiConnector=value; } }
    public App(MainPage page, IServiceProvider provider)
	{
		InitializeComponent();

        Services = provider;
        AlertSvc = Services.GetService<IAlertService>();
        MainPage = page;
	}
}
