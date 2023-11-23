using ControleDeContatos.DTOs;
using ControleDeContatos.Models;
using System.Collections.Generic;


namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel ListarPorId(int id);

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioDTO usuario);

        bool Apagar(int id);
    }
}
