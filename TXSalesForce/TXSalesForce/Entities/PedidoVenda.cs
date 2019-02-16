using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TXSalesForce.Entities
{
    [Table("PedidoVenda")]
    public class PedidoVenda : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get
            {
                return _id;
            }

            set
            {
                this._id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        private int _idCliente;
        [NotNull]
        public int idCliente
        {
            get
            {
                return _idCliente;
            }

            set
            {
                this._idCliente = value;
                OnPropertyChanged(nameof(idCliente));
            }
        }

        //private IList<Produto> _lstProduto;
        //public IList<Produto> lstProduto
        //{
        //    get
        //    {
        //        return _lstProduto;
        //    }

        //    set
        //    {
        //        this._lstProduto = value;
        //        OnPropertyChanged(nameof(lstProduto));
        //    }
        //}

        private DateTime _datapedido;
        [NotNull]
        public DateTime dataPedido
        {
            get
            {
                return _datapedido;
            }

            set
            {
                this._datapedido = value;
                OnPropertyChanged(nameof(dataPedido));
            }
        }

        private double _totalPagar;
        public double totalPagar
        {
            get
            {
                return _totalPagar;
            }

            set
            {
                this._totalPagar = value;
                OnPropertyChanged(nameof(totalPagar));
            }
        }

        public string totalPagarString
        {
            get
            {
                return "Total Pedido: R$ " + this.totalPagar.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
