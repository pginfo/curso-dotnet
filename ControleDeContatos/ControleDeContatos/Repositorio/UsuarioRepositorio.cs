using ControleDeContatos.Data;
using ControleDeContatos.DTOs;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .OrderBy(u => u.Nome)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioDTO usuarioModel)
        {
            UsuarioModel usuario = ListarPorId(usuarioModel.Id);

            if (usuario == null) throw new Exception("Houve um erro na atualização do usuário.");

            usuario.Id = usuarioModel.Id;
            usuario.Nome = usuarioModel.Nome;
            usuario.Login = usuarioModel.Login;
            usuario.Email = usuarioModel.Email;
            usuario.Perfil = usuarioModel.Perfil;

            _bancoContext.Usuarios.Update(usuario);
            _bancoContext.SaveChanges();

            return usuario;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na remoção do usuário.");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

        
    }
}
