using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXSalesForce.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TXSalesForce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaClientesPage : ContentPage
    {
        public ListaClientesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.ListarTodosOsClientes();
        }

        public void ListarTodosOsClientes()
        {
            ClienteDataAccess lobjClienteDataAccess = new ClienteDataAccess();

            IList<Cliente> llstClientes = lobjClienteDataAccess.GetAllClientes().ToList();

            lstListaClientes.ItemsSource = llstClientes;
        }

        private void lstListaClientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var cliente = (Cliente)myListView.SelectedItem;
            Navigation.PushAsync(new VisualizarClientePage(cliente.id));
        }
    }
}