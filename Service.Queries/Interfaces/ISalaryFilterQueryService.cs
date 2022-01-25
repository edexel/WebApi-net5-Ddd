using Common.Collection;
using Service.Queries.DTOs;
using System.Threading.Tasks;
using static Service.Queries.Enums.Enums;

namespace Service.Queries.Interfaces
{
    public interface ISalaryFilterQueryService
    {
        Task<DataCollection<SalaryDto>> GetSalaryFilterAction(int id, FilterAction action, int page, int take);
    }
}
