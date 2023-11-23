﻿using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuarioModel);

        void RemoverSessaoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();
    }
}
