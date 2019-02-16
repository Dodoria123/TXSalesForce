using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TXSalesForce.Entities;
using SQLite;

namespace TXSalesForce.DataAccess
{
    public class ProdutoDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Produto> Produtos { get; set; }

        public ProdutoDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Produto>();

            this.Produtos = new ObservableCollection<Produto>(database.Table<Produto>());

            //this.DeleteAllProdutos();

            if (!database.Table<Produto>().Any())
            {
                AdicionarProdutos();
            }
        }

        public void AdicionarProdutos()
        {
            this.Produtos.Add(new Produto { nomeProduto = "Café em pó", preçoProduto = 5.5, qtdDisponivel = 9 });
            this.Produtos.Add(new Produto { nomeProduto = "Bolacha Maria", preçoProduto = 3.2, qtdDisponivel = 100 });
            this.Produtos.Add(new Produto { nomeProduto = "Leite em pó", preçoProduto = 4.6, qtdDisponivel = 300 });
            this.Produtos.Add(new Produto { nomeProduto = "Todinho", preçoProduto = 2, qtdDisponivel = 6 });
            this.Produtos.Add(new Produto { nomeProduto = "Nutella", preçoProduto = 9.5, qtdDisponivel = 3 });
            this.Produtos.Add(new Produto { nomeProduto = "Pão Carioquinha", preçoProduto = 8, qtdDisponivel = 500 });
            this.Produtos.Add(new Produto { nomeProduto = "Arroz Pai João", preçoProduto = 12.5, qtdDisponivel = 250 });
            this.Produtos.Add(new Produto { nomeProduto = "Sabão Minerva", preçoProduto = 11.5, qtdDisponivel = 150 });
            this.Produtos.Add(new Produto { nomeProduto = "Peito de Frango Sadia", preçoProduto = 22, qtdDisponivel = 350 });
            this.Produtos.Add(new Produto { nomeProduto = "Fanta Uva", preçoProduto = 10.5, qtdDisponivel = 450 });
            this.Produtos.Add(new Produto { nomeProduto = "Ovo Maltine", preçoProduto = 14, qtdDisponivel = 550 });
            this.SaveAllProdutos();
        }

        public IEnumerable<Produto> GetFilteredProdutosPorNome(string nome)
        {
            lock (collisionLock)
            {
                var query = from pro in database.Table<Produto>()
                            where pro.nomeProduto == nome
                            select pro;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Produto> GetFilteredProdutosPorPreco(double preco)
        {
            lock (collisionLock)
            {
                var query = from pro in database.Table<Produto>()
                            where pro.preçoProduto == preco
                            select pro;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Produto> GetFilteredProdutos()
        {
            lock (collisionLock)
            {
                return database.
                    Query<Produto>
                    ("SELECT * FROM Produto").AsEnumerable();
            }
        }

        public Produto GetProduto(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Produto>().FirstOrDefault(produto => produto.id == id);
            }
        }

        public int SaveProduto(Produto instanciaProduto)
        {
            lock (collisionLock)
            {
                if (instanciaProduto.id != 0)
                {
                    database.Update(instanciaProduto);
                }
                else
                {
                    database.Insert(instanciaProduto);
                }

                return instanciaProduto.id;
            }
        }

        public void SaveAllProdutos()
        {
            lock (collisionLock)
            {
                foreach (var instanciaProduto in this.Produtos)
                {
                    if (instanciaProduto.id != 0)
                    {
                        database.Update(instanciaProduto);
                    }
                    else
                    {
                        database.Insert(instanciaProduto);
                    }
                }
            }
        }

        public int DeleteProduto(Produto instanciaProduto)
        {
            var id = instanciaProduto.id;

            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Produto>(id);
                }
            }

            this.Produtos.Remove(instanciaProduto);

            return id;
        }

        public void DeleteAllProdutos()
        {
            lock (collisionLock)
            {
                database.DropTable<Produto>();
                database.CreateTable<Produto>();
            }

            this.Produtos = null;
            this.Produtos = new ObservableCollection<Produto>(database.Table<Produto>());
        }
    }
}
