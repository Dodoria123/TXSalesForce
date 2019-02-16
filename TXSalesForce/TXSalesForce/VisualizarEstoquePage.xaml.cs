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
	public partial class VisualizarEstoquePage : ContentPage
	{
		public VisualizarEstoquePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            this.ListarTodosOsProdutosEstoque();
        }

        public void ListarTodosOsProdutosEstoque()
        {
            ProdutoDataAccess lobjProdutoDataAccess = new ProdutoDataAccess();

            var dados = lobjProdutoDataAccess.GetFilteredProdutos();

            lstEstoque.ItemsSource = dados.Where(x => x.qtdDisponivel >= 10).ToList();
            lstEstoqueBaixo.ItemsSource = dados.Where(x => x.qtdDisponivel < 10).ToList();
        }

        private void lstEstoque_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var estoque = (Produto)myListView.SelectedItem;
        }

        private void lstEstoqueBaixo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var estoque = (Produto)myListView.SelectedItem;
        }
    }
}