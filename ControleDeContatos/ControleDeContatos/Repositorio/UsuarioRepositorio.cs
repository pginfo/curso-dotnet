using ControleDeContatos.Data;
using ControleDeContatos.DTOs;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
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

        public UsuarioModel BuscarPorEmailElogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(
                u => u.Email.ToUpper() == email.ToUpper() && u.Login.ToUpper() == login.ToUpper()
                );
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .OrderBy(x => x.Nome)
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

        public UsuarioModel Atualizar(UsuarioModel usuarioModel)
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

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado.");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual.");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
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
