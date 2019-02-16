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
	public partial class CriarPedidoVendaPage : ContentPage
	{
		public CriarPedidoVendaPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            this.ListarCliente();
            this.ListarProdutos();
        }

        private void ListarCliente()
        {
            ClienteDataAccess lobjClienteDataAccess = new ClienteDataAccess();

            lstListaClientes.ItemsSource = lobjClienteDataAccess.GetAllClientes().ToList();
        }

        private void ListarProdutos()
        {
            ProdutoDataAccess lobjProdutoDataAccess = new ProdutoDataAccess();

            lstProdutos.ItemsSource = lobjProdutoDataAccess.GetFilteredProdutos().ToList();
        }

        private void Button_SubtrairQtd_Clicked(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Button lobjButton = (Button)sender;

                Produto lobjProduto = (Produto)lobjButton.BindingContext;

                if (lstProdutos.ItemsSource != null)
                {
                    IList<Produto> llstProduto = new List<Produto>();
                    llstProduto = (IList<Produto>)lstProdutos.ItemsSource;

                    if (llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada > 0)
                    {
                        llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada = llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada - 1;

                        string totalPagarString = lblTotalPagar.Text;
                        double totalPagar = Convert.ToDouble(totalPagarString);
                        lblTotalPagar.Text = Convert.ToString(totalPagar - llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().preçoProduto);

                        lstProdutos.ItemsSource = null;
                        lstProdutos.ItemsSource = llstProduto;
                    } 
                }
            }
        }

        private void Button_AdicionarQtd_Clicked(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Button lobjButton = (Button)sender;
                double totalPagar = 0;
                Produto lobjProduto = (Produto)lobjButton.BindingContext;

                if (lstProdutos.ItemsSource != null)
                {
                    IList<Produto> llstProduto = new List<Produto>();
                    llstProduto = (IList<Produto>)lstProdutos.ItemsSource;

                    if (llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada == llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdDisponivel)
                    {
                        DisplayAlert("Alerta", "Quantidade máxima em estoque atingida!", "OK");
                        return;
                    }

                    llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada = llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().qtdSelecionada + 1;

                    string totalPagarString = lblTotalPagar.Text;
                    if (!string.IsNullOrEmpty(totalPagarString))
                    {
                        totalPagar = Convert.ToDouble(totalPagarString);
                    }
                    lblTotalPagar.Text = Convert.ToString(totalPagar + llstProduto.Where(x => x.id == lobjProduto.id).FirstOrDefault().preçoProduto);

                    lstProdutos.ItemsSource = null;
                    lstProdutos.ItemsSource = llstProduto;
                }
            }
        }

        private void Button_SalvarPedido_Clicked(object sender, EventArgs e)
        {
            if (lstListaClientes.SelectedItem == null)
            {
                DisplayAlert("Alerta", "Cliente não selecionado!", "OK");
                return;
            }

            IList<Produto> llstProduto = (IList<Produto>)lstProdutos.ItemsSource;
            llstProduto = llstProduto.Where(x => x.qtdSelecionada > 0).ToList();

            if (llstProduto == null || llstProduto.Count() <= 0)
            {
                DisplayAlert("Alerta", "Nenhum produto selecionado!", "OK");
                return;
            }

            PedidoVendaDataAccess lobjPedidoVendaDataAcess = new PedidoVendaDataAccess();
            ProdutoPedidoVendaDataAccess lobjProdutoPedidoVendaDataAccess = new ProdutoPedidoVendaDataAccess();
            ProdutoDataAccess lobjProdutoDataAccess = new ProdutoDataAccess();
            PedidoVenda lobjPedidoVenda = new PedidoVenda();
            Cliente lobjCliente = new Cliente();

            lobjPedidoVenda.dataPedido = DateTime.Now;
            lobjCliente = (Cliente)lstListaClientes.SelectedItem;
            lobjPedidoVenda.idCliente = lobjCliente.id;

            lobjPedidoVenda.totalPagar = Convert.ToDouble(lblTotalPagar.Text);

            //Insere novo pedido
            int idNovoPedidoVenda = lobjPedidoVendaDataAcess.SavePedidoVenda(lobjPedidoVenda);

            //Insere os produtos relacionados ao pedido
            foreach (var lobjProduto in llstProduto)
            {
                ProdutoPedidoVenda lobjProdutoPedidoVenda = new ProdutoPedidoVenda();
                lobjProdutoPedidoVenda.idPedidovenda = idNovoPedidoVenda;
                lobjProdutoPedidoVenda.idProduto = lobjProduto.id;
                lobjProdutoPedidoVendaDataAccess.SaveProdutoPedidoVenda(lobjProdutoPedidoVenda);

                //Da baixa na quantidade do estoque
                lobjProduto.qtdDisponivel = lobjProduto.qtdDisponivel - lobjProduto.qtdSelecionada;
                lobjProduto.qtdSelecionada = 0;
                lobjProdutoDataAccess.SaveProduto(lobjProduto);
            }

            lstProdutos.ItemsSource = null;
            lstProdutos.ItemsSource = lobjProdutoDataAccess.GetFilteredProdutos().ToList();
            lblTotalPagar.Text = "";

            DisplayAlert("Alerta", "Pedido cadastrado com sucesso!", "OK");

        }
    }
}