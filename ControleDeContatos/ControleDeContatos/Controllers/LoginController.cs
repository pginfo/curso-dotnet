﻿using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            /*
             * Se o usuário estiver logado, redirecionar para a home
             */
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index","Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioModel = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuarioModel != null)
                    {
                        if (usuarioModel.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuarioModel);
                            return RedirectToAction("Index","Home");
                        }                        
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s)";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, algo deu errado. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
