using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using AppPickingMinhasCompras.Services;
using AppPickingMinhasCompras.Helpers;
using AppPickingMinhasCompras.Views;
using AppPickingMinhasCompras.ViewModels;

namespace AppPickingMinhasCompras;

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

		// Register Database Service - SQLiteDataBaseHelpers implementa IDbService
		builder.Services.AddSingleton<IDbService, SQLiteDataBaseHelpers>();
		
		// Register Pages
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<ProdutosPage>();
		builder.Services.AddSingleton<CategoriasPage>();
		
		// Register ViewModels
		builder.Services.AddSingleton<ProdutoViewModel>();
		builder.Services.AddSingleton<CategoriaViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
