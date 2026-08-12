# Guia Rápido de Inicialização

## Pré-requisitos

- **.NET 8 SDK** instalado
- **Visual Studio 2022** com workload MAUI ou **Visual Studio Code** com extensões C#
- **Android SDK** (para desenvolvimento Android)

## Instalação Inicial

### 1. Restaurar Pacotes NuGet

```bash
dotnet restore
```

### 2. Limpar Build Anterior

```bash
dotnet clean
```

## Executar o Projeto

### Android

```bash
dotnet maui run -f net8.0-android
```

### iOS (macOS apenas)

```bash
dotnet maui run -f net8.0-ios
```

### Windows

```bash
dotnet maui run -f net8.0-windows
```

## Estrutura Básica

```
AppPickingMinhasCompras/
├── Models/                      # Entidades de Banco de Dados
│   ├── Produto.cs
│   ├── Categoria.cs
│   └── Compra.cs
├── Services/                    # Serviços (DB, API, etc)
│   ├── IDbService.cs
│   └── DbService.cs             # SQLite
├── ViewModels/                  # Lógica de UI (MVVM)
│   ├── ProdutoViewModel.cs
│   └── CategoriaViewModel.cs
├── Views/                       # Páginas XAML
│   ├── MainPage.xaml
│   ├── ProdutosPage.xaml
│   └── CategoriasPage.xaml
├── Helpers/                     # Funções Utilitárias
│   └── ValidationHelper.cs
├── Constants/                   # Constantes da App
│   └── AppConstants.cs
├── App.xaml                     # Temas e Recursos Globais
├── AppShell.xaml                # Navegação
├── MauiProgram.cs               # Configuração da Aplicação
└── AppPickingMinhasCompras.csproj
```

## Banco de Dados SQLite

### Localização

O banco de dados é armazenado em:
- **Android**: `/data/data/com.companyname.apppicking/files/apppicking.db`
- **iOS**: App Documents Directory
- **Windows**: Local AppData

### Tabelas Criadas Automaticamente

1. **Categorias** - Armazena categorias de produtos
2. **Produtos** - Armazena produtos para compra
3. **Compras** - Armazena histórico de compras

## Funcionalidades Principais

### 📱 Home
Página inicial da aplicação com botão de teste.

### 📦 Produtos
- Listar todos os produtos
- Adicionar novo produto
- Editar produto
- Deletar produto
- Filtrar por categoria

### 📂 Categorias
- Listar todas as categorias
- Adicionar nova categoria
- Deletar categoria

## Operações Básicas

### Adicionar Produto

1. Vá para a aba "Produtos"
2. Preencha os campos:
   - Nome
   - Descrição
   - Preço
   - Quantidade
3. Clique em "Salvar Produto"

### Listar Produtos

Os produtos aparecem automaticamente quando você acessa a página de produtos.

### Deletar Produto

Clique no botão "Deletar" ao lado do produto desejado.

## Debugging

### Visual Studio

1. Abra `AppPickingMinhasCompras.csproj`
2. Selecione a plataforma (Android/iOS/Windows)
3. Pressione F5 para iniciar o debug

### Visual Studio Code

1. Instale a extensão C# Dev Kit
2. Abra o terminal integrado
3. Execute: `dotnet maui run -f net8.0-android`

## Troubleshooting

### "Erro ao carregar banco de dados"
- Certifique-se de que o diretório `FileSystem.AppDataDirectory` está acessível
- Verifique permissões de leitura/escrita

### "Erro de compilação"
- Execute: `dotnet clean && dotnet restore && dotnet build`
- Verifique se todos os NuGet packages foram restaurados

### "Página em branco"
- Verifique se o ViewModel foi configurado no MauiProgram.cs
- Confirme o XAML está correto (sem erros de sintaxe)

### "Erro ao conectar banco SQLite"
- Remova o arquivo `.db` anterior
- Reinicie a aplicação para recriar o banco
- Verifique a versão do SQLite-net-pcl

## Próximas Etapas

1. Personalizar cores e temas em `App.xaml`
2. Adicionar mais entidades em `Models/`
3. Implementar sincronização com servidor
4. Adicionar autenticação de usuário
5. Publicar na Play Store/App Store

## Recursos Úteis

- [Documentação MAUI Oficial](https://learn.microsoft.com/en-us/dotnet/maui)
- [SQLite-net Documentation](https://github.com/praeclarum/sqlite-net)
- [MVVM Toolkit](https://github.com/CommunityToolkit/dotnet)
- [App Shell Navigation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell)

## Contato e Suporte

Para dúvidas sobre o projeto, consulte:
- `ESTRUTURA_PROJETO.md` - Documentação completa
- `GUIA_DESENVOLVIMENTO.md` - Como adicionar novas funcionalidades
