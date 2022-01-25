using Common.Collection;
using Service.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.Interfaces
{
    public interface ISalaryQueryService
    {
        Task<DataCollection<SalaryDto>> GetAllAsync(int page, int take, IEnumerable<int> salary = null);
        Task<SalaryDto> GetAsync(int id);
        Task<IEnumerable<SalaryDto>> GetAsync(string employeeCode);
    }
}
