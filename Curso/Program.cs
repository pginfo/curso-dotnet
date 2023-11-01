using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new ApplicationContext();

            //var cliente = db.Clientes.Find(2);

            /*
             * Opções que podem ser usadas para excluir
             */
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            //db.Entry(cliente).State = EntityState.Deleted;

            /*
             * Método desconectado
             */
            var cliente = new Cliente { 
                Id = 3 
            };
            
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new ApplicationContext();
            /*
             * O método Find() procura na chave primária
             */
            //var cliente = db.Clientes.Find(3);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "ClienteDesconectadoPasso3",
                Telefone = "78521548965"
            };

            // Atachar objeto para que ele possa ser rastreado
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            
            /*
             * Atualizar os dados de rastreamento
             */
            //db.Entry(cliente).State = EntityState.Modified;
            
            /*
             * Usando o método Update() ele grava novamente todas as propriedades,
             * mesmo que não tenham sido alteradas
             */
            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new ApplicationContext();

            var pedidos = db.Pedidos
                .Include(p=>p.Itens)
                    .ThenInclude(p=>p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();

            var consultaPorMetodo = db.Clientes
                .Where(p=>p.Id > 0)
                .OrderBy(p=>p.Id)
                .ToList();

            foreach(var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                /*
                 * Apenas o métod find procura o que está em memória.
                 */
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste Novo",
                CodigoBarras = "1234567893254",
                Valor = 12m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Rafael Almeida",
                Cep = "11720013",
                Cidade = "Praia Grande",
                Estado = "SP",
                Telefone = "988974215"
            };

            var listaClientes = new[] 
            {
                new Cliente
                { 
                    Nome = "Roberto Augusto Pacheco",
                    Cep = "12345678",
                    Cidade = "Guarujá",
                    Estado = "SP",
                    Telefone = "01234569788"
                },
                new Cliente
                {
                    Nome = "Maria de Lourdes",
                    Cep = "22302678",
                    Cidade = "Peruíbe",
                    Estado = "SP",
                    Telefone = "13334569788"
                }
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);

            db.AddRange(listaClientes);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total registros: {registros}");

        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();

            /*
             * Existe 4 formas de adicionar o registro na base de dados
             * as duas primeiras são as recomendadas.
             */
            db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //db.Add(produto);

            /*
             * db.SaveChanges() 
             * executa efetivamente a inserção no banco de dados e
             * retorna a quantidade de registros alterados
             */
            var registros = db.SaveChanges();

            Console.WriteLine($"Total registros: {registros}");
        }
    }
}
