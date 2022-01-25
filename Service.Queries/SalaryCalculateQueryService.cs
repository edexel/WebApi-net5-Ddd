using Common.Collection;
using Common.Mapping;
using Common.Paging;
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
   public class SalaryCalculateQueryService : ISalaryCalculateQueryService
    {
        private readonly ApplicationDbContext _context;
        public SalaryCalculateQueryService(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public  DataCollection<SalaryCalculateDto> GetSalaryCalculate(int page, int take)
        {
            var collection = _context.Salary
                             .ToLookup(c => c.EmployeeCode)
                             .Select(g => g.First())
                             .Select(   x => new SalaryCalculateDto
                                        {
                                            EmployeeCode = x.EmployeeCode,
                                            Name = x.EmployeeName + " " + x.EmployeeSuname,
                                            Divicion = x.Divicion,
                                            Position = x.Position,
                                            BeginDate = x.BeginDate,
                                            Birthday = x.Birthday,
                                            IdentificationNumber = x.IdentificationNumber,
                                            TotalSalary =   Convert.ToDecimal(String.Format("{0:0.00}", x.BaseSalary + x.ProductionBonus +
                                                            ((x.CommissionBonus * 75) / 100) + //   Other Income 
                                                            ((((x.BaseSalary + x.Commission) * 8) / 100) + x.Commission)
                                                            + x.Commission))
                                        })
                             .GetPagedAsyncAsIENumerable(page,take);

            return collection.MapTo<DataCollection<SalaryCalculateDto>>();
        }

    }
}
