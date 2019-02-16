using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TXSalesForce.Entities;
using Xamarin.Forms;

namespace TXSalesForce.DataAccess
{
    public class ProdutoPedidoVendaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<ProdutoPedidoVenda> ProdutoPedidoVendas { get; set; }

        public ProdutoPedidoVendaDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<ProdutoPedidoVenda>();

            this.ProdutoPedidoVendas = new ObservableCollection<ProdutoPedidoVenda>(database.Table<ProdutoPedidoVenda>());

            //this.DeleteAllProdutoPedidosVendas();

            if (!database.Table<ProdutoPedidoVenda>().Any())
            {
                AdicionarProdutoPedidoVenda();
            }
        }

        public void AdicionarProdutoPedidoVenda()
        {
            this.ProdutoPedidoVendas.Add(new ProdutoPedidoVenda { idPedidovenda = 1, idProduto = 1 });
            this.ProdutoPedidoVendas.Add(new ProdutoPedidoVenda { idPedidovenda = 1, idProduto = 2 });
            this.ProdutoPedidoVendas.Add(new ProdutoPedidoVenda { idPedidovenda = 1, idProduto = 3 });
            this.SaveAllProdutoPedidoVenda();
        }
     
        public IEnumerable<ProdutoPedidoVenda> GetFilteredProdutosPorPedido(int idPedido)
        {
            lock (collisionLock)
            {
                var query = from pro in database.Table<ProdutoPedidoVenda>()
                            where pro.idPedidovenda == idPedido
                            select pro;
                return query.AsEnumerable();
            }
        }

        //public IEnumerable<Produto> GetFilteredProdutos()
        //{
        //    lock (collisionLock)
        //    {
        //        return database.
        //            Query<Produto>
        //            ("SELECT * FROM Produto").AsEnumerable();
        //    }
        //}

        //public Produto GetProduto(int id)
        //{
        //    lock (collisionLock)
        //    {
        //        return database.Table<Produto>().FirstOrDefault(produto => produto.id == id);
        //    }
        //}

        public int SaveProdutoPedidoVenda(ProdutoPedidoVenda instanciaProdutoPedidoVenda)
        {
            lock (collisionLock)
            {
                if (instanciaProdutoPedidoVenda.id != 0)
                {
                    database.Update(instanciaProdutoPedidoVenda);
                }
                else
                {
                    database.Insert(instanciaProdutoPedidoVenda);
                }

                return instanciaProdutoPedidoVenda.id;
            }
        }

        public void SaveAllProdutoPedidoVenda()
        {
            lock (collisionLock)
            {
                foreach (var instanciaProdutoPedidoVendas in this.ProdutoPedidoVendas)
                {
                    if (instanciaProdutoPedidoVendas.id != 0)
                    {
                        database.Update(instanciaProdutoPedidoVendas);
                    }
                    else
                    {
                        database.Insert(instanciaProdutoPedidoVendas);
                    }
                }
            }
        }

        public int DeleteProdutoPedidoVenda(ProdutoPedidoVenda instanciaProdutoPedidoVenda)
        {
            var id = instanciaProdutoPedidoVenda.id;

            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<ProdutoPedidoVenda>(id);
                }
            }

            this.ProdutoPedidoVendas.Remove(instanciaProdutoPedidoVenda);

            return id;
        }

        public void DeleteAllProdutoPedidosVendas()
        {
            lock (collisionLock)
            {
                database.DropTable<ProdutoPedidoVenda>();
                database.CreateTable<ProdutoPedidoVenda>();
            }

            this.ProdutoPedidoVendas = null;
            this.ProdutoPedidoVendas = new ObservableCollection<ProdutoPedidoVenda>(database.Table<ProdutoPedidoVenda>());
        }
    }
}
