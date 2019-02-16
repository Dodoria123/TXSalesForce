using System;
using System.Collections.Generic;
using System.Text;
using TXSalesForce.Entities;
using Xamarin.Forms;

namespace TXSalesForce
{
    public class ProdutoDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Produto)item).qtdDisponivel < 10 ? ValidTemplate : InvalidTemplate;
        }
    }
}
