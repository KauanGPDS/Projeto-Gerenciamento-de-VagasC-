using Microsoft.EntityFrameworkCore;  // Importa o namespace para utilizar o Entity Framework Core, uma biblioteca ORM para acesso a banco de dados
using Vagas.Data;  // Importa o namespace onde o DbContext 'ApplicationDbContext' está definido

// Cria o builder para a aplicação Web, permitindo configurar os serviços e a pipeline de requisições
var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços necessários para o container da aplicação

// Adiciona o serviço de controladores MVC (API) ao contêiner de dependências
builder.Services.AddControllers();
// Configuração do Swagger/OpenAPI para gerar documentação da API
// Mais informações sobre configuração do Swagger em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();  // Permite que o Swagger descubra e exiba os endpoints da API
builder.Services.AddSwaggerGen();  // Gera a documentação da API

// Adiciona o serviço de DbContext ao contêiner, configurando a conexão com o banco de dados SQL Server
// 'DefaultConnection' é o nome da string de conexão que será buscada no arquivo de configuração (appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();  // Cria a aplicação a partir do builder configurado

// Configura o pipeline de requisições HTTP

// Se a aplicação estiver em ambiente de desenvolvimento, ativa o Swagger para documentação da API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita o uso do Swagger na aplicação
    app.UseSwaggerUI();  // Habilita a interface do Swagger para navegar pela documentação
}

app.UseHttpsRedirection();  // Força o redirecionamento de requisições HTTP para HTTPS

app.UseAuthorization();  // Habilita a autorização (autenticação de usuários)

app.MapControllers();  // Mapeia os controladores da aplicação, ou seja, define quais rotas serão mapeadas para métodos nos controladores

app.Run();  // Inicia a aplicação e começa a escutar por requisições HTTP
