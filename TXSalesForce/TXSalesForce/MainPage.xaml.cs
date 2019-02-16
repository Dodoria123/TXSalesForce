using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TXSalesForce
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_CriarPedidoVenda_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CriarPedidoVendaPage());
        }

        private void Button_VisualizarPedidosVenda_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisualizarPedidosVendaPage());
        }

        private void Button_VisualizarEstoque_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisualizarEstoquePage());
        }

        private void Button_VisualizarCliente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaClientesPage());
        }
    }
}
