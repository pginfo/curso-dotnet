using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRazorPages.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        public string? Nome {get; set;}

        [Required][DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

    }
}
