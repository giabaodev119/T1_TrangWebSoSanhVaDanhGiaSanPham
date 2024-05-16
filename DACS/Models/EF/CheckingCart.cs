using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;


namespace DACS.Models.EF
{
    public class CheckingCart
    {
        public List<CheckItem> Items { get; set; } = new List<CheckItem>();
        public void AddItem(CheckItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId ==item.ProductId);
            if (existingItem == null)
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }
    }
}
