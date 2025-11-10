using Api.Entities;

namespace Api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> RegisterEmployeeAsync(Employee employee);
        Task<Employee?> GetByEmailAsync(string email);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
