using System.ComponentModel.DataAnnotations;


namespace ControleDeContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        public string Email { get; set; }
    }
}
