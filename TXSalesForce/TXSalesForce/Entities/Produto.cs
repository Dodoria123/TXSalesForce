using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TXSalesForce.Entities
{
    [Table("Produto")]
    public class Produto : INotifyPropertyChanged
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

        private string _nomeProduto;
        [NotNull]
        public string nomeProduto
        {
            get
            {
                return _nomeProduto;
            }

            set
            {
                this._nomeProduto = value;
                OnPropertyChanged(nameof(nomeProduto));
            }
        }

        private double _preçoProduto;
        [NotNull]
        public double preçoProduto
        {
            get
            {
                return _preçoProduto;
            }

            set
            {
                this._preçoProduto = value;
                OnPropertyChanged(nameof(preçoProduto));
            }
        }

        private int _qtdDisponivel;
        [NotNull]
        public int qtdDisponivel
        {
            get
            {
                return _qtdDisponivel;
            }

            set
            {
                this._qtdDisponivel = value;
                OnPropertyChanged(nameof(qtdDisponivel));
            }
        }

        public string qtdDisponivelString
        {
            get
            {
                return "Quantidade: " + _qtdDisponivel.ToString();
            }
        }

        public int qtdSelecionada { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
