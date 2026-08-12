# App Picking - Minhas Compras

Aplicativo de gerenciamento de compras desenvolvido em .NET MAUI com suporte a SQLite.

## Estrutura do Projeto

```
AppPickingMinhasCompras/
├── Models/                  # Modelos de dados
│   ├── Produto.cs          # Modelo da tabela Produtos
│   └── Categoria.cs        # Modelo da tabela Categorias
├── Services/               # Serviços de aplicação
│   ├── IDbService.cs       # Interface do serviço de banco de dados
│   └── DbService.cs        # Implementação do serviço SQLite
├── ViewModels/             # ViewModels (MVVM)
│   ├── ProdutoViewModel.cs
│   └── CategoriaViewModel.cs
├── Views/                  # Páginas XAML
│   ├── MainPage.xaml
│   ├── ProdutosPage.xaml
│   └── CategoriasPage.xaml
├── Platforms/              # Código específico da plataforma
│   └── Android/
│       ├── MainActivity.cs
│       └── AndroidManifest.xml
├── Constants/              # Constantes da aplicação
│   └── AppConstants.cs
├── App.xaml                # Recursos globais da aplicação
├── AppShell.xaml           # Shell/Navegação da aplicação
├── MauiProgram.cs          # Configuração da aplicação
└── AppPickingMinhasCompras.csproj
```

## Banco de Dados SQLite

### Tabelas

#### Categorias
```sql
CREATE TABLE Categorias (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL UNIQUE,
    Descricao TEXT,
    DataCriacao DATETIME NOT NULL,
    Ativo BOOLEAN NOT NULL DEFAULT 1
);
```

#### Produtos
```sql
CREATE TABLE Produtos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Descricao TEXT NOT NULL,
    Preco DECIMAL NOT NULL,
    Quantidade INTEGER,
    DataCriacao DATETIME NOT NULL,
    DataAtualizacao DATETIME,
    Ativo BOOLEAN NOT NULL DEFAULT 1
);
```

## Dependências

### NuGet Packages
- **Microsoft.Maui.Controls** (8.0.70) - Framework MAUI
- **sqlite-net-pcl** (1.8.116) - ORM para SQLite
- **SQLitePCLRaw.bundle_green** (2.1.8) - Bindings SQLite
- **CommunityToolkit.Mvvm** (8.2.2) - MVVM Toolkit para padrão MVVM

## Como Usar

### Inicializar o Banco de Dados

Na classe `App.xaml.cs`, adicione:

```csharp
protected override async void OnStart()
{
    var dbService = MauiProgram.CreateMauiApp()
        .Services.GetRequiredService<IDbService>();
    await dbService.InitializeAsync();
}
```

### Operações CRUD com Produtos

```csharp
// Obter todos os produtos
List<Produto> produtos = await _dbService.GetProdutosAsync();

// Obter um produto por ID
Produto? produto = await _dbService.GetProdutoAsync(1);

// Salvar novo produto
var novoProduto = new Produto 
{ 
    Nome = "Produto X",
    Descricao = "Descrição",
    Preco = 100.00m,
    Quantidade = 5
};
await _dbService.SaveProdutoAsync(novoProduto);

// Atualizar produto
produto.Nome = "Novo Nome";
await _dbService.UpdateProdutoAsync(produto);

// Deletar produto
await _dbService.DeleteProdutoAsync(produto);
```

### Operações CRUD com Categorias

As operações seguem o mesmo padrão dos Produtos, utilizando os métodos em `IDbService`:
- `GetCategoriasAsync()`
- `GetCategoriaAsync(id)`
- `SaveCategoriaAsync(categoria)`
- `UpdateCategoriaAsync(categoria)`
- `DeleteCategoriaAsync(categoria)`

## ViewModels

Os ViewModels utilizam o padrão MVVM com o Community Toolkit:

### ProdutoViewModel
- `LoadProdutosCommand` - Carrega lista de produtos
- `SalvarProdutoCommand` - Salva novo produto
- `DeleteProdutoCommand` - Deleta um produto

### CategoriaViewModel
- `LoadCategoriasCommand` - Carrega lista de categorias
- `SalvarCategoriaCommand` - Salva nova categoria
- `DeleteCategoriaCommand` - Deleta uma categoria

## Executar a Aplicação

### Android
```bash
dotnet maui run -f net8.0-android
```

### iOS
```bash
dotnet maui run -f net8.0-ios
```

### Windows
```bash
dotnet maui run -f net8.0-windows
```

## Estrutura do MauiProgram

O arquivo `MauiProgram.cs` configura:
- Temas e recursos da aplicação
- Injeção de dependência do `DbService`
- Páginas da aplicação
- Logging (em modo DEBUG)

## Próximos Passos

1. Configurar navegação entre páginas no `AppShell.xaml`
2. Adicionar mais modelos conforme necessário
3. Implementar validações adicionais
4. Adicionar sincronização com servidor (opcional)
5. Implementar testes unitários
