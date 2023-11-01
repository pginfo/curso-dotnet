using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

    }
}
