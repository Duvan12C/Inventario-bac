using Api.Data;
using Api.Entities;
using Api.Helpers;
using Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositories.Implementations
{
    public class SaleRepository(BacDbContext bacDbContext) : ISaleRepository
    {

        public async Task<int> RegisterSaleCartAsync(int employeeId, List<ProductSaleItem> products)
        {
            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Price", typeof(decimal));

            foreach (var p in products)
                table.Rows.Add(p.ProductId, p.Quantity, p.Price);

            var paramEmployee = new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId };
            var paramProducts = new SqlParameter("@Products", SqlDbType.Structured)
            {
                TypeName = "dbo.ProductSaleType",
                Value = table
            };

            // Este SP devuelve un SELECT con el IdSale, así que lo leemos manualmente
            int idSale = 0;

            var connection = bacDbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "EXEC dbo.sp_RegisterSaleCart @EmployeeId, @Products";
                command.Parameters.Add(paramEmployee);
                command.Parameters.Add(paramProducts);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        idSale = reader.GetInt32(reader.GetOrdinal("IdSale"));
                    }
                }
            }

            await connection.CloseAsync();
            return idSale;
        }




        public async Task<Sale?> GetSaleWithDetailsAsync(int id)
        {
            Sale? sale = await bacDbContext.Sales.
                Include(s => s.SaleDetails).
                FirstOrDefaultAsync(p => p.IdSale == id);
            return sale;
        }
    }
}
