
using Common.Collection;
using Common.Mapping;
using Common.Paging;
using Microsoft.EntityFrameworkCore;
using Persistance.Database;
using Service.Queries.DTOs;
using Service.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class SalaryQueryService : ISalaryQueryService
    {
        private readonly ApplicationDbContext _context;
        public SalaryQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<SalaryDto>> GetAllAsync(int page, int take, IEnumerable<int> salary = null)
        {
            var collection = await _context.Salary
                            .Where(x => salary == null || salary.Contains(x.Id))
                            .OrderByDescending(x => x.Id)
                            .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<SalaryDto>>();
        }

        public async Task<SalaryDto> GetAsync(int id)
        {
            var salary = await _context.Salary.SingleOrDefaultAsync(x => x.Id == id);

            return salary != null ? salary.MapTo<SalaryDto>() : new SalaryDto();
        }

        public async Task<IEnumerable<SalaryDto>> GetAsync(string employeeCode)
        {
            return (await _context.Salary.Where(x => x.EmployeeCode == employeeCode).ToListAsync()).MapTo<List<SalaryDto>>();
        }
    }
}
