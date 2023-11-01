using System;

namespace CSharpDiretoAoPontoHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            long maxLong = long.MaxValue;
            decimal nroDecimal = 15.43m;
            double nroDouble = 12.86;
            string strTexto = "Verificado";
            char indAtivo = 'S';
            DateTime dtAdmissao = new DateTime(2023,10,22,11,10,00);
            TimeSpan periodoTrabalhado = DateTime.Now - dtAdmissao;

            Console.WriteLine(
                $"Código: {maxLong}\nDecimal: {nroDecimal}\n" +
                $"Double: {nroDouble}\nString: {strTexto}\nChar: {indAtivo}\n" +
                $"DateTime: {dtAdmissao}\nPeríodo trabalhado: {periodoTrabalhado}"
                );

            /*
             * Conversão de tipos de variáveis
             * 
             * Implícita
             * Explícita
             * Utilizando a classe convert
             * Utilizando os métodos parse
             */

            int indice = 64;

            /*
             * Conversão Implícita
             * pois com o tipo double 
             * conseguimos repesentar os números inteiros
             */
            double indiceDouble = indice;

            /*
             * Conversão Explícita, 
             * pois nesse caso o inteiro 
             * não possibilita o uso de casas decimais.
             * Então deve-se usar (int)
             */
            int indiceInt = (int)indiceDouble;

            /*
             * Conversão utilizando "convert"
             */
            string notaString = "10";
            int notaConvert = Convert.ToInt32(notaString);
           
            /*
             * Conversão utilizando "parse"
             */
            int notaParse = int.Parse(notaString);

            /*
             * Usando tratamento de erros durante a conversão
             */
            if (int.TryParse(notaString, out int notaTryParse))
            {
                Console.WriteLine($"Usando try parse: {notaTryParse}");

            } else {
                Console.WriteLine("Número em formato inválido.");
            }

            /*
             * OPERADORES: 
             * ARITMÉTICOS - COMPARAÇÃO - LÓGICOS - IGUALDADE
             */

            int nroIncrementar = 30;
            Console.WriteLine($"Vai incrementar o \"30\" e depois imprimir: {++nroIncrementar}");

            /*
             * Alterar o sinal
             */
            int nroMudarSinalparaNegativo = -nroIncrementar;
            Console.WriteLine($"Mudar sinal para negativo: {nroMudarSinalparaNegativo}");

            int nroMudarSinalparaPositivo = (-(nroMudarSinalparaNegativo));
            Console.WriteLine($"Mudar sinal para positivo: {nroMudarSinalparaPositivo}");


            /*
             * Operações aritméticas
             * Converte para double de modo a exibir a parte decimal
             */
            var divisao = (double)20 / 3;

            Console.WriteLine($"Divisão: {Math.Round(divisao,2)}");

            Console.Read();
        }
    }
}
