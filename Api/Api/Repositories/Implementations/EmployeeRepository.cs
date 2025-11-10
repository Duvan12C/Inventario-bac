using Api.Data;
using Api.Entities;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations
{
    public class EmployeeRepository
        (
          BacDbContext bacDbContext
        ) : IEmployeeRepository
    {

        public async Task<Employee> RegisterEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "El empleado no puede ser nulo");

            bacDbContext.Employees.Add(employee);
            await bacDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email), "El email no puede ser nulo");

            Employee? employee = await bacDbContext.Employees.FirstOrDefaultAsync(u => u.Email == email);
           
            return employee;
        }



        public async Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await bacDbContext.Employees.AnyAsync(u => u.Name == name);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await bacDbContext.Employees.AnyAsync(u => u.Email == email);
        }

    }
}
