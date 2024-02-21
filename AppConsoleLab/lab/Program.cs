using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            static string ConverterObjetoEmQueryString(object objeto)
            {
                var propriedades = objeto.GetType().GetProperties();
                var parChaveValor = new List<string>();

                foreach (var prop in propriedades)
                {
                    var valor = prop.GetValue(objeto);

                    if (valor is DateTime data) valor = data.ToString("yyyy-MM-ddHH:mm:ss");

                    var valorEncodado = HttpUtility.UrlEncode(valor.ToString());

                    parChaveValor.Add($"{prop.Name}={valorEncodado}");
                }

                string queryString = string.Join("&",parChaveValor);

                return queryString;
            }


            // Objeto para exemplificação
            var pessoa = new Pessoa
            {
                Nome = "Genildo Virginio dos Santos Martins",
                Idade = 46,
                DataNascimento = new DateTime(1977, 03, 25)
            };

            string url = ConverterObjetoEmQueryString(pessoa);

            Console.WriteLine(url);
        }
    }

    class Pessoa
    {
        private string _nome;

        public string Nome { get { return _nome; } set { _nome = Normaliza(value); } }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }

        private string Normaliza(string texto)
        {
            return texto.Trim().ToUpper();
        }
    }
}
