using Microsoft.EntityFrameworkCore;
using Vagas.Models.Entites;

namespace Vagas.Data
{
    // Define o contexto de dados para o banco de dados da aplicação, herdando de DbContext
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração para o contexto de banco de dados
        // Essas opções são passadas para a classe base (DbContext)
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // O construtor chama a classe base DbContext com as opções recebidas
            // Isso configura o contexto para usar o banco de dados e aplicar as configurações passadas
        }

        // DbSet representa uma tabela no banco de dados para a entidade Jobs
        // Isso permite que você execute operações de CRUD na tabela de Jobs do banco de dados
        public DbSet<Jobs> Jobs { get; set; }
    }
}
