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
	public partial class VisualizarClientePage : ContentPage
	{
        int idCliente;

        public VisualizarClientePage (int id)
		{
			InitializeComponent ();
            idCliente = id;
        }

        protected override void OnAppearing()
        {
            this.DetalhamentoCliente();
        }

        public void DetalhamentoCliente()
        {
            //client = new HttpClient();
            //var json = await client.GetStringAsync($"http://meucongressonacional.com/api/001/deputado/{idDeputado}");
            //var dados = JsonConvert.DeserializeObject<Deputado>(json);

            //DeputadoImage = new Uri(dados.fotoURL);
            //imgDeputado.Source = DeputadoImage;

            ClienteDataAccess lobjClienteDataAccess = new ClienteDataAccess();
            PedidoVendaDataAccess lobjPedidoVendaDataAccess = new PedidoVendaDataAccess();
            ProdutoPedidoVendaDataAccess lobjProdutoPedidoVendaDataAccess = new ProdutoPedidoVendaDataAccess();
            ProdutoDataAccess lobjProdutoDataAccess = new ProdutoDataAccess();

            Cliente lobjCliente = lobjClienteDataAccess.GetCliente(idCliente);            

            //Lista de pedidos do cliente em questão
            IList<PedidoVenda> llstPedidosVenda = lobjPedidoVendaDataAccess.GetFilteredPedidosPorIdCliente(idCliente).ToList();

            double ldouTotalGasto = 0;

            foreach (var pedido in llstPedidosVenda)
            {
                double totalPedido = 0;

                IList<ProdutoPedidoVenda> llstProdutoPedidoVenda = lobjProdutoPedidoVendaDataAccess.GetFilteredProdutosPorPedido(pedido.id).ToList();

                foreach(var produtoPedido in llstProdutoPedidoVenda)
                {
                    Produto lobjProdtuo = new Produto();

                    lobjProdtuo = lobjProdutoDataAccess.GetProduto(produtoPedido.idProduto);
                    ldouTotalGasto = ldouTotalGasto + lobjProdtuo.preçoProduto;
                    totalPedido = totalPedido + lobjProdtuo.preçoProduto;
                    pedido.totalPagar = totalPedido;
                }
            }

            lblId.Text = lobjCliente.id.ToString();
            lblNome.Text = "Nome do Cliente: " + lobjCliente.nome;
            lblEndereco.Text = "Endereço: " + lobjCliente.endereco;
            lblCidade.Text = "Cidade: " + lobjCliente.cidade;
            lblUF.Text = "UF: " + lobjCliente.UF;
            lblTotalGasto.Text = "TOTAL GASTO EM COMPRAS: R$ " + ldouTotalGasto.ToString();
            lstPedidosCliente.ItemsSource = llstPedidosVenda;
        }
    }
}