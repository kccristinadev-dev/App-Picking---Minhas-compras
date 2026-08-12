# Guia de Desenvolvimento - App Picking

## Como Adicionar uma Nova Entidade

### 1. Criar o Modelo

Crie um novo arquivo em `Models/` com a classe que representa a tabela:

```csharp
using SQLite;

namespace AppPickingMinhasCompras.Models;

[Table("NovaTabela")]
public class NovaEntidade
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Nome { get; set; } = string.Empty;

    [NotNull]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public bool Ativo { get; set; } = true;
}
```

### 2. Adicionar Métodos na Interface IDbService

Abra `Services/IDbService.cs` e adicione:

```csharp
Task<List<NovaEntidade>> GetNovasEntidadesAsync();
Task<NovaEntidade?> GetNovaEntidadeAsync(int id);
Task<int> SaveNovaEntidadeAsync(NovaEntidade entidade);
Task<int> UpdateNovaEntidadeAsync(NovaEntidade entidade);
Task<int> DeleteNovaEntidadeAsync(NovaEntidade entidade);
```

### 3. Implementar na Classe DbService

Abra `Services/DbService.cs` e adicione as implementações:

```csharp
public async Task<List<NovaEntidade>> GetNovasEntidadesAsync()
{
    await EnsureConnectionAsync();
    return await _connection!.Table<NovaEntidade>().ToListAsync();
}

public async Task<NovaEntidade?> GetNovaEntidadeAsync(int id)
{
    await EnsureConnectionAsync();
    return await _connection!.Table<NovaEntidade>()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
}

public async Task<int> SaveNovaEntidadeAsync(NovaEntidade entidade)
{
    await EnsureConnectionAsync();
    entidade.DataCriacao = DateTime.Now;
    return await _connection!.InsertAsync(entidade);
}

public async Task<int> UpdateNovaEntidadeAsync(NovaEntidade entidade)
{
    await EnsureConnectionAsync();
    return await _connection!.UpdateAsync(entidade);
}

public async Task<int> DeleteNovaEntidadeAsync(NovaEntidade entidade)
{
    await EnsureConnectionAsync();
    return await _connection!.DeleteAsync(entidade);
}
```

### 4. Criar o ViewModel

Crie `ViewModels/NovaEntidadeViewModel.cs`:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppPickingMinhasCompras.Models;
using AppPickingMinhasCompras.Services;

namespace AppPickingMinhasCompras.ViewModels;

public partial class NovaEntidadeViewModel : ObservableObject
{
    private readonly IDbService _dbService;

    [ObservableProperty]
    private List<NovaEntidade> entidades = new();

    [ObservableProperty]
    private bool isLoading = false;

    public NovaEntidadeViewModel()
    {
        _dbService = MauiProgram.CreateMauiApp()
            .Services.GetRequiredService<IDbService>();
    }

    [RelayCommand]
    public async Task LoadEntidades()
    {
        try
        {
            IsLoading = true;
            Entidades = await _dbService.GetNovasEntidadesAsync();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!
                .DisplayAlert("Erro", $"Erro ao carregar: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
```

### 5. Criar a Page (View)

Crie `Views/NovaEntidadePage.xaml`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="AppPickingMinhasCompras.Views.NovaEntidadePage"
             Title="Nova Entidade">

    <CollectionView ItemsSource="{Binding Entidades}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <StackLayout Padding="10">
                    <Label Text="{Binding Nome}" FontAttributes="Bold" FontSize="16"/>
                </StackLayout>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>

</ContentPage>
```

### 6. Registrar a Página no Shell

Atualize `AppShell.xaml`:

```xml
<TabBar>
    <ShellContent Title="Home" ContentTemplate="{DataTemplate local:MainPage}" Route="MainPage" />
    <ShellContent Title="Nova Entidade" ContentTemplate="{DataTemplate local:NovaEntidadePage}" Route="NovaEntidadePage" />
</TabBar>
```

## Padrões do Projeto

### Nomenclatura
- **Classes**: PascalCase (ex: `Produto`, `ProdutoViewModel`)
- **Métodos**: PascalCase (ex: `GetProdutos()`)
- **Variáveis**: camelCase (ex: `_dbService`, `isLoading`)
- **Constantes**: UPPER_CASE (ex: `MAX_LENGTH`)

### Pasta Organization
```
Classes de Modelo → Models/
Serviços/Dados → Services/
ViewModels → ViewModels/
Páginas XAML → Views/
Helpers/Utilitários → Helpers/
Constantes → Constants/
```

## Dependency Injection

Registre novos serviços em `MauiProgram.cs`:

```csharp
builder.Services.AddSingleton<INovoServico, NovoServico>();
```

## Async/Await

Sempre use `async/await` para operações de banco de dados:

```csharp
var dados = await _dbService.GetProdutosAsync();
```

## Tratamento de Erros

Sempre envolva operações de banco de dados em try-catch:

```csharp
try
{
    // Sua operação
}
catch (Exception ex)
{
    await Application.Current!.MainPage!
        .DisplayAlert("Erro", ex.Message, "OK");
}
```

## Testes

Para adicionar testes unitários, crie um projeto de testes:

```bash
dotnet new mstest -n AppPickingMinhasCompras.Tests
```

## Compilação e Publicação

### Debug
```bash
dotnet maui run -f net8.0-android
```

### Release
```bash
dotnet publish -f net8.0-android -c Release
```

## Troubleshooting

### Erro de Banco de Dados
- Verifique o caminho do banco em `DbService.GetDatabasePath()`
- Certifique-se de que o `AppDataDirectory` está acessível

### Erro de Binding
- Verifique se o ViewModel está configurado como `BindingContext`
- Verifique a sintaxe XAML de binding

### Erro de NuGet
- Limpe o cache: `dotnet nuget locals all --clear`
- Restaure os pacotes: `dotnet restore`
