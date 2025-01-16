using Microsoft.AspNetCore.Mvc;  // Importa o namespace para trabalhar com a funcionalidade do ASP.NET Core MVC
using Vagas.Models.Entites;      // Importa o namespace onde a classe 'JobStatus' está definida

namespace Vagas.Models
{
    // Classe que representa os dados necessários para adicionar uma nova vaga (Job)
    // Essa classe é usada para receber os dados de uma vaga quando a aplicação recebe um pedido HTTP
    public class AddJobs
    {
        // Propriedade 'Title' armazena o título da vaga de emprego
        // O tipo 'string' é usado para representar o título da vaga
        public string Title { get; set; }

        // Propriedade 'Status' armazena o status da vaga (Aberta ou Finalizada)
        // Utiliza o tipo 'JobStatus', que é um enum com os possíveis status de uma vaga
        public JobStatus Status { get; set; }

        // Propriedade 'CreatedAt' armazena a data e hora de criação da vaga
        // Tipo 'DateTime' é utilizado para armazenar informações de data e hora
        public DateTime CreatedAt { get; set; }

        // Propriedade 'UpdatedAt' armazena a data e hora da última atualização da vaga
        // Tipo 'DateTime' é utilizado para armazenar informações de data e hora
        public DateTime UpdatedAt { get; set; }
    }
}
