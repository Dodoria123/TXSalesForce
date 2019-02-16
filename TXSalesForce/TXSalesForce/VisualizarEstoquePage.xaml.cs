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

            //client = new HttpClient();
            //var json = await client.GetStringAsync($"http://meucongressonacional.com/api/001/deputado");
            //var dados = JsonConvert.DeserializeObject<IList<Deputado>>(json);
            var dados = lobjProdutoDataAccess.GetFilteredProdutos();

            //IList<TextCell> llstCells = new List<TextCell>();

            //foreach (var lobjProduto in dados)
            //{
            //    TextCell cell = new TextCell();

            //    cell.Text = lobjProduto.nomeProduto;
            //    cell.Detail = lobjProduto.qtdDisponivel.ToString();

            //    if (lobjProduto.qtdDisponivel < 10)
            //    {
            //        cell.TextColor = Color.Red;
            //    }
            //    else
            //    {
            //        cell.TextColor = Color.Black;
            //    }

            //    llstCells.Add(cell);
            //}

            //lstEstoque.ItemsSource = llstCells.AsEnumerable();

            lstEstoque.ItemsSource = dados.Where(x => x.qtdDisponivel >= 10).ToList();
            lstEstoqueBaixo.ItemsSource = dados.Where(x => x.qtdDisponivel < 10).ToList();
        }

        private void lstEstoque_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var estoque = (Produto)myListView.SelectedItem;
            //Navigation.PushAsync(new DetalhamentoDeputadoPage(deputado.id));
        }

        private void lstEstoqueBaixo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var estoque = (Produto)myListView.SelectedItem;
            //Navigation.PushAsync(new DetalhamentoDeputadoPage(deputado.id));
        }
    }
}