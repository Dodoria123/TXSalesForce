using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TXSalesForce.Entities
{
    [Table("Cliente")]
    public class Cliente : INotifyPropertyChanged
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

        private string _nome;
        [NotNull]
        public string nome
        {
            get
            {
                return _nome;
            }

            set
            {
                this._nome = value;
                OnPropertyChanged(nameof(nome));
            }
        }

        private string _endereco;
        [NotNull]
        public string endereco
        {
            get
            {
                return _endereco;
            }

            set
            {
                this._endereco = value;
                OnPropertyChanged(nameof(endereco));
            }
        }

        private string _cidade;
        [NotNull]
        public string cidade
        {
            get
            {
                return _cidade;
            }

            set
            {
                this._cidade = value;
                OnPropertyChanged(nameof(cidade));
            }
        }

        private string _UF;
        [NotNull]
        public string UF
        {
            get
            {
                return _UF;
            }

            set
            {
                this._UF = value;
                OnPropertyChanged(nameof(UF));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
