using Microsoft.EntityFrameworkCore;  // Importa o namespace para utilizar o Entity Framework Core, uma biblioteca ORM para acesso a banco de dados
using Vagas.Data;  // Importa o namespace onde o DbContext 'ApplicationDbContext' est� definido

// Cria o builder para a aplica��o Web, permitindo configurar os servi�os e a pipeline de requisi��es
var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os necess�rios para o container da aplica��o

// Adiciona o servi�o de controladores MVC (API) ao cont�iner de depend�ncias
builder.Services.AddControllers();
// Configura��o do Swagger/OpenAPI para gerar documenta��o da API
// Mais informa��es sobre configura��o do Swagger em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();  // Permite que o Swagger descubra e exiba os endpoints da API
builder.Services.AddSwaggerGen();  // Gera a documenta��o da API

// Adiciona o servi�o de DbContext ao cont�iner, configurando a conex�o com o banco de dados SQL Server
// 'DefaultConnection' � o nome da string de conex�o que ser� buscada no arquivo de configura��o (appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();  // Cria a aplica��o a partir do builder configurado

// Configura o pipeline de requisi��es HTTP

// Se a aplica��o estiver em ambiente de desenvolvimento, ativa o Swagger para documenta��o da API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita o uso do Swagger na aplica��o
    app.UseSwaggerUI();  // Habilita a interface do Swagger para navegar pela documenta��o
}

app.UseHttpsRedirection();  // For�a o redirecionamento de requisi��es HTTP para HTTPS

app.UseAuthorization();  // Habilita a autoriza��o (autentica��o de usu�rios)

app.MapControllers();  // Mapeia os controladores da aplica��o, ou seja, define quais rotas ser�o mapeadas para m�todos nos controladores

app.Run();  // Inicia a aplica��o e come�a a escutar por requisi��es HTTP
