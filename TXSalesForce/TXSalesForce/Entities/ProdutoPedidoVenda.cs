using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TXSalesForce.Entities
{
    [Table("ProdutoPedidoVenda")]
    public class ProdutoPedidoVenda : INotifyPropertyChanged
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

        private int _idProduto;
        [NotNull]
        public int idProduto
        {
            get
            {
                return _idProduto;
            }

            set
            {
                this._idProduto = value;
                OnPropertyChanged(nameof(idProduto));
            }
        }

        private int _idPedidovenda;
        [NotNull]
        public int idPedidovenda
        {
            get
            {
                return _idPedidovenda;
            }

            set
            {
                this._idPedidovenda = value;
                OnPropertyChanged(nameof(idPedidovenda));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
