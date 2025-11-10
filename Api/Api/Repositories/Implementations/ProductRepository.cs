using Api.Data;
using Api.Entities;
using Api.Helpers;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations
{
    public class ProductRepository(BacDbContext bacDbContext) : IProductRepository
    {
        public async Task<Product> CreateProducAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "El producto no puede ser nulo");

            bacDbContext.Products.Add(product);
            await bacDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProducAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "El producto no puede ser nulo");

            Product? productUpdate = await bacDbContext.Products.FirstOrDefaultAsync(p => p.IdProduct == product.IdProduct);

            if (productUpdate == null)
                throw new ArgumentNullException(nameof(productUpdate), "El producto no existe en la base de datos");

            productUpdate.Name = product.Name;
            productUpdate.Price = product.Price;
            productUpdate.Quantity = product.Quantity;
            productUpdate.UpdatedAt = DateTime.Now;
            productUpdate.UpdatedBy = product.UpdatedBy;

            await bacDbContext.SaveChangesAsync();

            return productUpdate;
        }


        public async Task<Product?> DeleteProductAsync(int id)
        {

            Product? product = await bacDbContext.Products.FirstOrDefaultAsync(p => p.IdProduct == id);

            if (product == null)
                throw new ArgumentNullException(nameof(product), "El producto no se encontró");

            bacDbContext.Products.Remove(product);
            await bacDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<PagedResult<Product>> GetListProducAsync(string? search = null, int page = 1, int pageSize = 10)
        {
            IQueryable<Product> query = bacDbContext.Products;

 
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Code.Contains(search) || p.Name.Contains(search));
            }


            int totalCount = await query.CountAsync();

            List<Product> products = await query
                .OrderBy(p => p.IdProduct)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = products
            };
        }


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            Product? product = await bacDbContext.Products.FirstOrDefaultAsync(p => p.IdProduct == id);
            return product;
        }


    }
}
