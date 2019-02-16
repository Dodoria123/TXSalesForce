using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXSalesForce.DataAccess;
using TXSalesForce.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TXSalesForce
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarPedidosVendaPage : ContentPage
	{
        IList<PedidoVenda> llstPedidosVenda = new List<PedidoVenda>();

		public VisualizarPedidosVendaPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            this.ListarTodosOsPedidos();
        }

        public void ListarTodosOsPedidos()
        {
            PedidoVendaDataAccess lobjPedidoVendaDataAccess = new PedidoVendaDataAccess();

            llstPedidosVenda = lobjPedidoVendaDataAccess.GetAllPedidosVenda().ToList();

            ddlListaPedidos.ItemsSource = llstPedidosVenda.ToList();
            lstListaPedidosVenda.ItemsSource = llstPedidosVenda;
        }

        private void Button_ConsultarPedidoVendaPorID_Clicked(object sender, EventArgs e)
        {
            //TODO
        }

        private void Button_ConsultarPedidoVenda_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
    }
}