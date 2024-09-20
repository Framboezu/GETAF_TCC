﻿using GETAF.Models.Context;
using Microsoft.AspNetCore.Mvc;
using GETAF.Models;
using GETAF.Helper;
using GETAF.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web.Mvc;

namespace GETAF.Models.ViewModel {
    public class LoginModel {

        private readonly ISessao _sessao;
        public string Email { get; set; }
        public string Password { get; set; }

        public Resposta ValidarLogin() {
            try {
                using (var _context = new AppDbContext()) {
                    var user = _context.Usuarios.Where(x => x.Email == Email).FirstOrDefault();
                    if (user == null) {
                        return new Resposta(false, "Login/senha inválidos.");
                    }

                    if (Hash.ValidarHash(Password, user.Salt, user.Senha)) 
                    {
                        return new Resposta(true, "Login bem-sucedido.");
                    }
                    return new Resposta(false, "Login/senha inválidos.");
                }
            } catch (Exception ex) {
                return new Resposta(false, ex.Message);
            }
        }
    }
}
