using Common.Collection;
using Service.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.Interfaces
{
   public interface ISalaryCalculateQueryService
    {
        DataCollection<SalaryCalculateDto> GetSalaryCalculate(int page, int take);

    }
}
