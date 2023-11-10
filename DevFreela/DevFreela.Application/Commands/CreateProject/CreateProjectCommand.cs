using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<int>
    {
        [Required(ErrorMessage="Titulo não foi preenchido")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Titulo não foi preenchido")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Titulo não foi preenchido")]
        public int IdClient { get; set; }

        public int IdFreelancer { get; set; }

        public decimal TotalCost { get; set; }
    }
}
