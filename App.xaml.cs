using AppPickingMinhasCompras.Services;

namespace AppPickingMinhasCompras;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override async void OnStart()
	{
		base.OnStart();
		
		try
		{
			// Inicializar banco de dados
			var dbService = MauiProgram.CreateMauiApp()
				.Services.GetRequiredService<IDbService>();
			await dbService.InitializeAsync();
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Erro ao inicializar banco de dados: {ex}");
		}
	}
}
