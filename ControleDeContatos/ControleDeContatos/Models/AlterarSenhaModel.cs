using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a senha atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe a nova senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [Compare("NovaSenha", ErrorMessage ="Senha atual não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
