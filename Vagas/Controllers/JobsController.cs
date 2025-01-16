using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Vagas.Data;
using Vagas.Models;
using Vagas.Models.Entites;

namespace Vagas.Controllers
{
    // Define o padrão de roteamento da API para este controlador (Jobs)
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Construtor que recebe o contexto da aplicação para interagir com o banco de dados
        public JobsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Método GET que retorna todas as vagas (jobs) cadastradas
        [HttpGet]
        public IActionResult GetAllJobs()
        {
            // Retorna a lista de todos os jobs do banco de dados
            return Ok(dbContext.Jobs.ToList());
        }

        // Método POST para adicionar uma nova vaga (job)
        [HttpPost]
        public IActionResult AddJobs(AddJobs addjobs)
        {
            // Cria um novo objeto de vaga com base nos dados recebidos na requisição
            var jobs = new Jobs()
            {
                Title = addjobs.Title,
                Status = addjobs.Status,
                CreatedAt = DateTime.Now,  // Define a data de criação como a data atual
                UpdatedAt = DateTime.Now,  // Define a data de atualização como a data atual
            };

            // Adiciona o novo job ao contexto e salva as alterações no banco de dados
            dbContext.Jobs.Add(jobs);
            dbContext.SaveChanges();

            // Retorna o job recém-criado com status 200 OK
            return Ok(jobs);
        }

        // Método GET que retorna todas as vagas com um status específico fornecido na URL
        [HttpGet]
        [Route("{status}")]
        public IActionResult GetJobs(JobStatus status)
        {
            // Filtra todos os Jobs com o status fornecido
            var jobs = dbContext.Jobs.Where(j => j.Status == status).ToList();

            // Se não encontrar jobs com o status fornecido, retorna 404
            if (jobs == null || jobs.Count == 0)
            {
                return NotFound();
            }

            // Caso contrário, retorna os jobs encontrados com status 200 OK
            return Ok(jobs);
        }

        // Método PUT que permite atualizar uma vaga (job) existente com base no id
        [HttpPut("{id}")]
        public IActionResult UpdateJobs(int id, [FromBody] UpdateJobs updateJobs)
        {
            // Busca a vaga com o id fornecido
            var job = dbContext.Jobs.Find(id);

            // Se não encontrar a vaga, retorna 404
            if (job == null)
            {
                return NotFound();
            }

            // Atualiza os dados da vaga com as informações fornecidas na requisição
            job.Title = updateJobs.Title;
            job.Status = updateJobs.Status;

            // Permite que a data de criação (CreatedAt) seja alterada sem validação específica
            job.CreatedAt = updateJobs.CreatedAt;  // Atribui a data recebida na requisição

            // Atualiza a data de modificação
            job.UpdatedAt = DateTime.Now;

            // Salva as alterações no banco de dados
            dbContext.SaveChanges();

            // Retorna o job atualizado com status 200 OK
            return Ok(job);
        }

        // Método DELETE para excluir uma vaga (job) pelo id
        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            // Busca a vaga com o id fornecido
            var job = dbContext.Jobs.Find(id);

            // Se não encontrar a vaga, retorna 404
            if (job == null)
            {
                return NotFound();
            }

            // Remove o job encontrado do banco de dados
            dbContext.Jobs.Remove(job);

            // Salva as alterações no banco de dados
            dbContext.SaveChanges();

            // Retorna um status de sucesso (200 OK) após a exclusão
            return Ok();
        }
    }
}
