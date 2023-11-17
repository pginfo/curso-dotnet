﻿using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [EmailAddress(ErrorMessage = "Email não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o número do celular")]
        [Phone(ErrorMessage = "Número do celular não é válido")]
        public string Celular { get; set; }
    }
}
