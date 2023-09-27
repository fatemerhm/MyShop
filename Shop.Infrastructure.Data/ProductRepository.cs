using Microsoft.EntityFrameworkCore;
using Shop.Core.Contracts;
using Shop.Core.Domain;
using Shop.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext context;

        public ProductRepository(ShopContext context)
        {
            this.context = context;
        }
        public Product Get(int ProductId)
        {
            return context.Products.Include(a => a.Medias).First(a => a.ProductId == ProductId);
        }

        public (List<Product>, int Count) GetAll(int pageNumber, int PageSize)
        {
            var  query = context.Products.Include(a => a.Medias).ToList();
            var lengthQuery = query.ToList().Count;
            return (query.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList(), lengthQuery);
        }

        public List<Product> GetChippestProduct()
        {
            List<Product> result = new List<Product>();
            foreach (var category in context.Categories.ToList())
            {
                int minPrice = context.Products.Include(a => a.Category).Where(a => a.Category == category)
                    .Min(a => a.Price);
                result.Add(context.Products.Include(a => a.Medias).First(a => a.Price == minPrice));

            }
            return result;
        }

        public (List<Product>, int Count) GetFilterProducts(string search, string category,int pageNumber, int PageSize)
        {
            IQueryable<Product> query = context.Products.Include(a => a.Category).Include(a => a.Medias);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Name.Contains(search) || a.Description.Contains(search));
            }
            if (category != "All")
            {
                query = query.Where(a => a.Category.CategoryName == category);

            }

            var lengthQuery = query.ToList().Count;

            return (query.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList(), lengthQuery);
        }

        public List<Product> GetNewstProduct()
        {
            return context.Products.Include(a => a.Medias)
               .OrderByDescending(a => a.InsertTime).ToList();
        }
        public (List<Product>, int Count) GetCategory(int Categoryid, int pageNumber, int PageSize)
        {
            var query = context.Products.Include(a => a.Medias).Where(a => a.Category.CategoryId == Categoryid).ToList();
            var lengthQuery = query.ToList().Count;
            return (query.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList(), lengthQuery);
        }

    }
}
