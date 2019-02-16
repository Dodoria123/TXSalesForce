using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TXSalesForce.Entities;

namespace TXSalesForce
{
    public class ClienteDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Cliente> Clientes { get; set; }

        public ClienteDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Cliente>();

            this.Clientes = new ObservableCollection<Cliente>(database.Table<Cliente>());

            //this.DeleteAllClientes();

            if (!database.Table<Cliente>().Any())
            {
                AdicionarClientes();
            }
        }

        public void AdicionarClientes()
        {
            this.Clientes.Add(new Cliente { nome = "Thiago Xavier", endereco = "Rua Teste 999", cidade = "Fortaleza", UF = "CE" });
            this.Clientes.Add(new Cliente { nome = "Sávio Pinho", endereco = "Rua Teste 555", cidade = "Morada Nova", UF = "CE" });
            this.Clientes.Add(new Cliente { nome = "Renan Alves", endereco = "Rua Teste 7777", cidade = "Caucaia", UF = "CE" });
            this.SaveAllClientes();
        }

        public IEnumerable<Cliente> GetFilteredClientesPorNome(string nome)
        {
            lock (collisionLock)
            {
                var query = from cli in database.Table<Cliente>()
                            where cli.nome == nome
                            select cli;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Cliente> GetFilteredClientesPorEndereco(string endereco)
        {
            lock (collisionLock)
            {
                var query = from cli in database.Table<Cliente>()
                            where cli.endereco == endereco
                            select cli;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Cliente> GetAllClientes()
        {
            lock (collisionLock)
            {
                return database.
                    Query<Cliente>
                    ("SELECT * FROM Cliente").AsEnumerable();
            }
        }

        public Cliente GetCliente(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Cliente>().FirstOrDefault(cliente => cliente.id == id);
            }
        }

        public int SaveCliente(Cliente instanciaCliente)
        {
            lock (collisionLock)
            {
                if (instanciaCliente.id != 0)
                {
                    database.Update(instanciaCliente);
                }
                else
                {
                    database.Insert(instanciaCliente);
                }

                return instanciaCliente.id;
            }
        }

        public void SaveAllClientes()
        {
            lock (collisionLock)
            {
                foreach (var instanciaCliente in this.Clientes)
                {
                    if (instanciaCliente.id != 0)
                    {
                        database.Update(instanciaCliente);
                    }
                    else
                    {
                        database.Insert(instanciaCliente);
                    }
                }
            }
        }

        public int DeleteCliente(Cliente instanciaCliente)
        {
            var id = instanciaCliente.id;

            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Cliente>(id);
                }
            }

            this.Clientes.Remove(instanciaCliente);

            return id;
        }

        public void DeleteAllClientes()
        {
            lock (collisionLock)
            {
                database.DropTable<Cliente>();
                database.CreateTable<Cliente>();
            }

            this.Clientes = null;
            this.Clientes = new ObservableCollection<Cliente>(database.Table<Cliente>());
        }
    }
}
