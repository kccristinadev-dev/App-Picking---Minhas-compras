using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using AppPickingMinhasCompras.Services;
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

		// Register Database Service
		builder.Services.AddSingleton<IDbService, DbService>();
		
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
