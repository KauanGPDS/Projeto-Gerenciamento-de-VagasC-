using System.ComponentModel.DataAnnotations.Schema;  // Importa para trabalhar com mapeamento de tabelas no banco
using System.ComponentModel.DataAnnotations;      // Importa para utilizar as anotações de validação de dados

namespace Vagas.Models.Entites
{
    // Classe que representa uma vaga de emprego (Job)
    public class Jobs
    {
        // A propriedade 'Id' é a chave primária da tabela de Jobs
        // O Entity Framework reconhece que 'Id' é o identificador único de cada Job
        public int Id { get; set; }

        // 'Title' armazena o título da vaga de emprego
        // O tipo 'string' é usado para representar o título como texto
        public string Title { get; set; }

        // 'Status' armazena o status da vaga (Aberta ou Finalizada)
        // Utiliza o tipo 'JobStatus', que é um enum definido abaixo
        public JobStatus Status { get; set; }

        // 'CreatedAt' armazena a data e hora de criação da vaga
        // Tipo 'DateTime' é utilizado para armazenar datas e horas
        public DateTime CreatedAt { get; set; }

        // 'UpdatedAt' armazena a data e hora da última atualização da vaga
        // Tipo 'DateTime' é utilizado para armazenar datas e horas
        public DateTime UpdatedAt { get; set; }
    }

    // Enum que define os possíveis status de uma vaga de emprego
    public enum JobStatus
    {
        // Status indicando que a vaga foi finalizada
        Finalizada,

        // Status indicando que a vaga está aberta e disponível
        Aberta
    }
}
