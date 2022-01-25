using Common.Collection;
using Common.Mapping;
using Common.Paging;
using Persistance.Database;
using Service.Queries.DTOs;
using Service.Queries.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static Service.Queries.Enums.Enums;

namespace Service.Queries
{
    public class SalaryFilterQueryService : ISalaryFilterQueryService
    {
        private readonly ApplicationDbContext _context;
        public SalaryFilterQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<SalaryDto>> GetSalaryFilterAction(int id, FilterAction action, int page, int take)
        {
            var employee = _context.Salary.Where(x => x.Id == id).FirstOrDefault();

            var collection = await _context.Salary
                            .Where( x =>    action == FilterAction.SameOfficeGrade ? x.Office == employee.Office && x.Grade == employee.Grade  
                                            :action == FilterAction.AllOfficeSameGrade  ?  x.Grade == employee.Grade
                                                                                        : action == FilterAction.SamePositionGrade ? x.Position == employee.Position && x.Grade == employee.Grade 
                                                                                        : x.Grade == employee.Grade

                                            )
                            .OrderByDescending(x => x.Id)
                            .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<SalaryDto>>();
        }


    }
}