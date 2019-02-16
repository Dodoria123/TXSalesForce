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
    public class PedidoVendaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        private ClienteDataAccess clienteDB = new ClienteDataAccess();
        private ProdutoDataAccess produtoDB = new ProdutoDataAccess();

        public ObservableCollection<PedidoVenda> PedidosVenda { get; set; }

        public PedidoVendaDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<PedidoVenda>();

            this.PedidosVenda = new ObservableCollection<PedidoVenda>(database.Table<PedidoVenda>());

            this.DeleteAllPedidoVendas();

            if (!database.Table<PedidoVenda>().Any())
            {
                AdicionarPedidosVenda();
            }
        }

        public void AdicionarPedidosVenda()
        {
            this.PedidosVenda.Add(new PedidoVenda { idCliente = 1, totalPagar = 12, dataPedido = DateTime.Now });
            this.PedidosVenda.Add(new PedidoVenda { idCliente = 1, totalPagar = 55.6, dataPedido = DateTime.Now });
            this.PedidosVenda.Add(new PedidoVenda { idCliente = 1, totalPagar = 110.8, dataPedido = DateTime.Now });
            this.SaveAllPedidoVendas();
        }

        public IEnumerable<PedidoVenda> GetFilteredPedidosPorIdCliente(int idCliente)
        {
            lock (collisionLock)
            {
                var query = from ped in database.Table<PedidoVenda>()
                            where ped.idCliente == idCliente
                            select ped;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<PedidoVenda> GetAllPedidosVenda()
        {
            lock (collisionLock)
            {
                return database.
                    Query<PedidoVenda>
                    ("SELECT * FROM PedidoVenda").AsEnumerable();
            }
        }

        public PedidoVenda GetPedidoVenda(int id)
        {
            lock (collisionLock)
            {
                return database.Table<PedidoVenda>().FirstOrDefault(pedidoVenda => pedidoVenda.id == id);
            }
        }

        public int SavePedidoVenda(PedidoVenda instanciaPedidoVenda)
        {
            lock (collisionLock)
            {
                if (instanciaPedidoVenda.id != 0)
                {
                    database.Update(instanciaPedidoVenda);
                }
                else
                {
                    database.Insert(instanciaPedidoVenda);
                }

                return instanciaPedidoVenda.id;
            }
        }

        public void SaveAllPedidoVendas()
        {
            lock (collisionLock)
            {
                foreach (var instanciaPedidoVenda in this.PedidosVenda)
                {
                    if (instanciaPedidoVenda.id != 0)
                    {
                        database.Update(instanciaPedidoVenda);
                    }
                    else
                    {
                        database.Insert(instanciaPedidoVenda);
                    }
                }
            }
        }

        public int DeletePedidoVenda(PedidoVenda instanciaPedidoVenda)
        {
            var id = instanciaPedidoVenda.id;

            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<PedidoVenda>(id);
                }
            }

            this.PedidosVenda.Remove(instanciaPedidoVenda);

            return id;
        }

        public void DeleteAllPedidoVendas()
        {
            lock (collisionLock)
            {
                database.DropTable<PedidoVenda>();
                database.CreateTable<PedidoVenda>();
            }

            this.PedidosVenda = null;
            this.PedidosVenda = new ObservableCollection<PedidoVenda>(database.Table<PedidoVenda>());
        }
    }
}
