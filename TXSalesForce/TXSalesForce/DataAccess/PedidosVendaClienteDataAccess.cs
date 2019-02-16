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
    public class PedidosVendaClienteDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<PedidosVendaCliente> PedidosVendaClientes { get; set; }

        public PedidosVendaClienteDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Produto>();

            this.PedidosVendaClientes = new ObservableCollection<PedidosVendaCliente>(database.Table<PedidosVendaCliente>());

            if (!database.Table<PedidosVendaCliente>().Any())
            {
                AdicionarProdutoPedidoVenda();
            }
        }

        public void AdicionarProdutoPedidoVenda()
        {
            this.PedidosVendaClientes.Add(new PedidosVendaCliente { idPedidovenda = 1, idCliente = 1 });
        }

        public IEnumerable<PedidosVendaCliente> GetFilteredPedidosVendaCliente(int idCliente)
        {
            lock (collisionLock)
            {
                var query = from ped in database.Table<PedidosVendaCliente>()
                            where ped.idCliente == idCliente
                            select ped;
                return query.AsEnumerable();
            }
        }

        public int SavePedidosVendaCliente(PedidosVendaCliente instanciaPedidosVendaCliente)
        {
            lock (collisionLock)
            {
                if (instanciaPedidosVendaCliente.id != 0)
                {
                    database.Update(instanciaPedidosVendaCliente);
                }
                else
                {
                    database.Insert(instanciaPedidosVendaCliente);
                }

                return instanciaPedidosVendaCliente.id;
            }
        }

        public void SaveAllPedidosVendaCliente()
        {
            lock (collisionLock)
            {
                foreach (var instanciaPedidosVendaCliente in this.PedidosVendaClientes)
                {
                    if (instanciaPedidosVendaCliente.id != 0)
                    {
                        database.Update(instanciaPedidosVendaCliente);
                    }
                    else
                    {
                        database.Insert(instanciaPedidosVendaCliente);
                    }
                }
            }
        }

        public int DeletePedidosVendaCliente(PedidosVendaCliente instanciaPedidosVendaCliente)
        {
            var id = instanciaPedidosVendaCliente.id;

            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<PedidosVendaCliente>(id);
                }
            }

            this.PedidosVendaClientes.Remove(instanciaPedidosVendaCliente);

            return id;
        }

        public void DeleteAllPedidosVendaCliente()
        {
            lock (collisionLock)
            {
                database.DropTable<PedidosVendaCliente>();
                database.CreateTable<PedidosVendaCliente>();
            }

            this.PedidosVendaClientes = null;
            this.PedidosVendaClientes = new ObservableCollection<PedidosVendaCliente>(database.Table<PedidosVendaCliente>());
        }
    }
}
