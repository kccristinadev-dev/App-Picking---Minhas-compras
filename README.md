# App Picking - Minhas Compras

Aplicativo multiplataforma para gerenciamento de lista de compras desenvolvido com **NET.MAUI**, **SQLite** e **C#**.

## 📋 Resumo do Projeto

Um aplicativo intuitivo para gerenciar sua lista de compras, organizar produtos por categorias e acompanhar seu histórico de compras.

## ✨ Características

- ✅ Interface intuitiva com múltiplas abas
- ✅ Banco de dados local SQLite
- ✅ Gerenciamento de Produtos e Categorias
- ✅ Histórico de Compras
- ✅ Validação de dados
- ✅ Suporte a múltiplas plataformas (Android, iOS, Windows)
- ✅ Padrão MVVM com Community Toolkit

## 🛠️ Stack Tecnológico

| Componente | Versão | Descrição |
|-----------|--------|-----------|
| .NET | 8.0 | Framework base |
| MAUI | 8.0.70 | UI Framework multiplataforma |
| SQLite | 1.8.116 | Banco de dados local |
| MVVM Toolkit | 8.2.2 | Padrão MVVM |

## 🚀 Como Começar

### Pré-requisitos
- .NET 8 SDK instalado
- Visual Studio 2022 ou Visual Studio Code
- Android SDK (para desenvolvimento Android)

### Instalação

1. **Clonar o repositório**
```bash
git clone <url-do-repositorio>
cd App-Picking---Minhas-compras
```

2. **Restaurar dependências**
```bash
dotnet restore
```

3. **Executar a aplicação**

Para Android:
```bash
dotnet maui run -f net8.0-android
```

Para iOS (macOS):
```bash
dotnet maui run -f net8.0-ios
```

Para Windows:
```bash
dotnet maui run -f net8.0-windows
```

## 📁 Estrutura do Projeto

```
AppPickingMinhasCompras/
├── Models/                    # Classes de dados
│   ├── Produto.cs
│   ├── Categoria.cs
│   └── Compra.cs
├── Services/                  # Serviços
│   ├── IDbService.cs
│   └── DbService.cs          # SQLite
├── ViewModels/               # Lógica de apresentação
│   ├── ProdutoViewModel.cs
│   └── CategoriaViewModel.cs
├── Views/                    # Páginas XAML
│   ├── MainPage.xaml
│   ├── ProdutosPage.xaml
│   └── CategoriasPage.xaml
├── Helpers/                  # Utilitários
├── Constants/                # Constantes
├── Platforms/                # Configurações específicas
└── App.xaml                  # Temas globais
```

## 📚 Documentação

| Documento | Descrição |
|-----------|-----------|
| [GUIA_RAPIDO.md](GUIA_RAPIDO.md) | Instruções rápidas para iniciantes |
| [ESTRUTURA_PROJETO.md](ESTRUTURA_PROJETO.md) | Documentação técnica completa |
| [GUIA_DESENVOLVIMENTO.md](GUIA_DESENVOLVIMENTO.md) | Como estender o projeto |

## 🗄️ Banco de Dados SQLite

O banco de dados é criado automaticamente com as seguintes tabelas:

- **Produtos**: Itens a comprar
- **Categorias**: Categorias dos produtos
- **Compras**: Histórico de compras

Localização do banco de dados:
- Android: `/data/data/com.companyname.apppicking/files/apppicking.db`
- iOS: App Documents Directory
- Windows: Local AppData

## 🎯 Funcionalidades

### Implementadas ✅
- [x] CRUD de Produtos
- [x] CRUD de Categorias
- [x] Banco de dados SQLite funcional
- [x] Interface com navegação por abas
- [x] Padrão MVVM

### Em Desenvolvimento 🔄
- [ ] Edição de Produtos
- [ ] Busca e filtros
- [ ] Sincronização com servidor

### Planejadas 🎁
- [ ] Modo offline
- [ ] Compartilhamento de listas
- [ ] Análise de gastos
- [ ] Notificações

## 📱 Suporte a Plataformas

- ✅ Android 21+
- ✅ iOS 14.2+
- ✅ Windows 10.0+
- ✅ macOS 13.1+

## 🔧 Dependências Principais

```xml
<PackageReference Include="Microsoft.Maui.Controls" Version="8.0.70" />
<PackageReference Include="sqlite-net-pcl" Version="1.8.116" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
```

## 🐛 Troubleshooting

### Erro ao restaurar pacotes
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Erro de compilação
```bash
dotnet clean
dotnet restore
dotnet build
```

### Banco de dados não encontrado
- Verifique as permissões de leitura/escrita
- Recrie o banco removendo o arquivo `.db`

## 📞 Suporte

Para dúvidas e sugestões, consulte a documentação ou abra uma issue no repositório.

## 📄 Licença

MIT License

## ✍️ Desenvolvido por

Desenvolvido para gerenciamento de compras pessoais com .NET MAUI.

---

**Versão**: 1.0.0  
**Última atualização**: Agosto de 2026  
**Status**: ✨ Em desenvolvimento
