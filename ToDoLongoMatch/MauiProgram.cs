using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using TodoApi.Models;

using ToDoLongoMatch.Utils;
using ToDoLongoMatch.ViewModels;

namespace ToDoLongoMatch;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("ToDoLongoMatch.appsettings.json");
		builder.Configuration.AddJsonStream(stream);


        return builder.Build();
	}
}
