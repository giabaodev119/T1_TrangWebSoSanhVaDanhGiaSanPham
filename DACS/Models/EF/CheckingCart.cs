using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using DACS.DataAccess;
using DACS.Helper;
using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        public int totalcart()
        {
            return Items.Count();
        }
        public bool condition (CheckItem item)
        {
            foreach (var tmp in Items)
            {
                if (tmp.ProductCategoryId != item.ProductCategoryId)
                {
                    return false;
                }

            }
            return true;
        }

    }
}
