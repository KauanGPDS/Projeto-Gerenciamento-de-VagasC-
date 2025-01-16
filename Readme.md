

# Vagas API

Este projeto implementa uma API RESTful para gerenciamento de vagas de emprego. A API oferece funcionalidades de CRUD (Create, Read, Update, Delete) para permitir a manipulação de dados de vagas, incluindo informações como título, status (Aberta ou Finalizada) e datas de criação e atualização.

## Funcionalidades

- **GET /api/jobs**: Retorna todas as vagas cadastradas.
- **GET /api/jobs/{status}**: Retorna vagas com o status específico (Aberta ou Finalizada).
- **POST /api/jobs**: Adiciona uma nova vaga.
- **PUT /api/jobs/{id}**: Atualiza uma vaga existente com base no ID.
- **DELETE /api/jobs/{id}**: Exclui uma vaga com base no ID.

## Tecnologias Usadas

- **ASP.NET Core 6.0**: Framework para criação de APIs.
- **Entity Framework Core**: Biblioteca ORM (Object-Relational Mapper) para acesso ao banco de dados.
- **SQL Server**: Banco de dados para armazenar as vagas.
- **Swagger**: Ferramenta para documentação interativa da API.

## Estrutura do Código

### 1. `Controllers/JobsController.cs`

O arquivo `JobsController.cs` define os endpoints da API relacionados ao gerenciamento das vagas de emprego. Este controlador expõe os métodos que permitem realizar operações de CRUD (Create, Read, Update, Delete) nas vagas de emprego.

#### Método `GetAllJobs`

O método `GetAllJobs` é responsável por retornar todas as vagas registradas no banco de dados. Ao fazer uma requisição `GET` para o endpoint `/api/jobs`, ele recupera todas as vagas da tabela de `Jobs` e as retorna como uma lista. Caso a operação seja bem-sucedida, o código de status HTTP 200 (OK) é retornado com os dados da lista de vagas.

##### Código:

```csharp
[HttpGet]
public IActionResult GetAllJobs()
{
    return Ok(dbContext.Jobs.ToList());
}
```

#### Método `AddJobs`

O método `AddJobs` permite a adição de uma nova vaga de emprego ao banco de dados. Ao fazer uma requisição `POST` para o endpoint `/api/jobs`, este método recebe os dados necessários para criar uma nova vaga, como o título e o status da vaga. Em seguida, a vaga é inserida no banco de dados e uma resposta é retornada com os detalhes da vaga recém-criada.

##### Código:

```csharp
[HttpPost]
public IActionResult AddJobs(AddJobs addjobs)
{
    var jobs = new Jobs()
    {
        Title = addjobs.Title,
        Status = addjobs.Status,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
    };
    dbContext.Jobs.Add(jobs);
    dbContext.SaveChanges();
    return Ok(jobs);
}
```
#### Método `GetJobs`

O método `GetJobs` permite retornar vagas de emprego filtradas por status. Ao fazer uma requisição `GET` para o endpoint `/api/jobs/{status}`, o método consulta o banco de dados e retorna todas as vagas com o status correspondente, como "Aberta" ou "Finalizada". Se não houver vagas com o status fornecido, uma resposta com código HTTP 404 (Not Found) será retornada.

##### Código:

```csharp
[HttpGet]
[Route("{status}")]
public IActionResult GetJobs(JobStatus status)
{
    var jobs = dbContext.Jobs.Where(j => j.Status == status).ToList();
    if (jobs == null || jobs.Count == 0)
    {
        return NotFound();
    }
    return Ok(jobs);
}
```

#### Método `UpdateJobs`

O método `UpdateJobs` permite a atualização de uma vaga existente no banco de dados. Ao fazer uma requisição `PUT` para o endpoint `/api/jobs/{id}`, o método atualiza os dados da vaga identificada pelo `id` fornecido, incluindo o título, o status e a data de atualização. Importante: a data de criação pode ser alterada durante a atualização.

##### Código:

```csharp
[HttpPut]
[Route("{id}")]
public IActionResult UpdateJobs(int id, UpdateJobs updateJobs)
{
    var job = dbContext.Jobs.FirstOrDefault(j => j.Id == id);
    if (job == null)
    {
        return NotFound();
    }
    job.Title = updateJobs.Title;
    job.Status = updateJobs.Status;
    job.CreatedAt = updateJobs.CreatedAt;  // A data de criação pode ser alterada
    job.UpdatedAt = DateTime.Now;
    dbContext.SaveChanges();
    return Ok(job);
}
```
#### Método `DeleteJob`

O método `DeleteJob` permite a exclusão de uma vaga existente no banco de dados. Ao fazer uma requisição `DELETE` para o endpoint `/api/jobs/{id}`, o método exclui a vaga identificada pelo `id` fornecido. Se a vaga não for encontrada, o método retornará uma resposta informando que a vaga não existe.

##### Código:

```csharp
[HttpDelete("{id}")]
public IActionResult DeleteJob(int id)
{
    var job = dbContext.Jobs.Find(id);
    if (job == null)
    {
        return NotFound();
    }
    dbContext.Jobs.Remove(job);
    dbContext.SaveChanges();
    return Ok();
}
```
### Arquivo: `Data/ApplicationDbContext.cs`

Este arquivo define o contexto de dados (`ApplicationDbContext`) que representa a conexão com o banco de dados e as entidades que serão mapeadas para as tabelas no banco. O contexto de dados é uma parte fundamental da implementação de um sistema de persistência de dados com o Entity Framework Core.

#### **DbSet<Jobs> Jobs**

A propriedade `DbSet<Jobs> Jobs` representa a tabela `Jobs` no banco de dados, onde são armazenadas as informações sobre as vagas de emprego. O `DbSet` é uma coleção que permite realizar operações de leitura e gravação no banco de dados para a entidade `Jobs`.

##### Código:

```csharp
public DbSet<Jobs> Jobs { get; set; }
```
### Arquivo: `Models/Entities/Jobs.cs`

Este arquivo define a estrutura da entidade `Jobs` e o enum `JobStatus`, que são usados para representar as vagas de emprego e seus respectivos status no sistema.

#### **Classe `Jobs`**

A classe `Jobs` representa uma vaga de emprego e contém as propriedades necessárias para armazenar informações detalhadas sobre a vaga, como título, status e datas de criação e atualização.

##### Código:

```csharp
public class Jobs
{
    public int Id { get; set; }
    public string Title { get; set; }
    public JobStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```
### Enum `JobStatus`

O `enum` `JobStatus` define os dois status possíveis de uma vaga de emprego no sistema: **Aberta** e **Finalizada**. Este enum é utilizado para indicar o estado atual da vaga e facilita o gerenciamento de suas informações, permitindo que o sistema controle de maneira clara se uma vaga está disponível ou já foi encerrada.

#### Código:

```csharp
public enum JobStatus
{
    Finalizada,
    Aberta
}
```
### Classes `AddJobs` e `UpdateJobs`

As classes `AddJobs` e `UpdateJobs` são responsáveis por receber os dados nas requisições de criação e atualização de vagas de emprego, respectivamente. Ambas são usadas como modelos (DTOs - Data Transfer Objects) para transportar os dados entre o cliente e a API.

#### `AddJobs`: Usada para adicionar uma nova vaga

A classe `AddJobs` é utilizada para representar os dados necessários para criar uma nova vaga de emprego. Ela inclui propriedades essenciais como o título da vaga, o status e as datas de criação e atualização.

##### Código:

```csharp
public class AddJobs
{
    public string Title { get; set; }    // Título da vaga de emprego
    public JobStatus Status { get; set; } // Status da vaga (Aberta ou Finalizada)
    public DateTime CreatedAt { get; set; } // Data de criação da vaga
    public DateTime UpdatedAt { get; set; } // Data da última atualização da vaga
}

```
```csharp
public class UpdateJobs
{
    public string Title { get; set; }    // Título da vaga de emprego
    public JobStatus Status { get; set; } // Status da vaga (Aberta ou Finalizada)
    public DateTime CreatedAt { get; set; } // Data de criação da vaga
    public DateTime UpdatedAt { get; set; } // Data da última atualização da vaga
}
```

# Conclusão

Este projeto foi desenvolvido por Kauan Guilherme, com o objetivo de implementar uma API RESTful para o gerenciamento de vagas de emprego. A API permite realizar operações básicas de CRUD para adicionar, consultar, atualizar e excluir vagas de emprego, e foi construída utilizando as tecnologias ASP.NET Core, Entity Framework Core, SQL Server e Swagger para documentação.

A implementação segue boas práticas de design de APIs RESTful e foi pensada para ser escalável, permitindo a integração com outras plataformas e sistemas.

Se tiver dúvidas ou sugestões sobre o projeto, sinta-se à vontade para entrar em contato!
