namespace AppPickingMinhasCompras;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
		CounterBtn.Text = $"Clicado {count} vez(es)";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
