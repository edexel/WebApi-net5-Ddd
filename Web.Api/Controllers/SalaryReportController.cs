using Common.Collection;
using Microsoft.AspNetCore.Mvc;
using Service.Queries.DTOs;
using Service.Queries.Interfaces;
using System.Threading.Tasks;
using static Service.Queries.Enums.Enums;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/report")]
    public class SalaryReportController
    {

       private readonly ISalaryCalculateQueryService _salaryCalculateQueryService;
        private readonly ISalaryFilterQueryService _SalaryFilter;
        public SalaryReportController(  ISalaryCalculateQueryService salaryCalculateQueryService, 
                                        ISalaryFilterQueryService SalaryFilter)
        {
           _salaryCalculateQueryService = salaryCalculateQueryService;
           _SalaryFilter = SalaryFilter;
        }


        /// <summary>
        /// This method return List item salaries estimate
        /// </summary>         
        /// <param name="page">This  parameter define  the number of page to show</param>
        /// <param name="take">This parameter define how much item you want to get</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        //salarystimate
        [HttpGet("getsalarystimate")]
        public DataCollection<SalaryCalculateDto> SalaryStimate(int page = 1, int take = 10)
        {
            return  _salaryCalculateQueryService.GetSalaryCalculate(page, take);
        }


        /// <summary>
        /// This method filters salary information based to an Employee
        /// </summary>
        /// <param name="id">This parameter is the id key record</param>  
        /// <param name="action">This parameter represents the action filter that you want to get</param>  
        /// <param name="page">This parameter define  the number of page to show</param>
        /// <param name="take">This parameter define how much item you want to get</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        /// <response code="400">BadRequest. Error de validacióon de datos.</response> 
        [HttpGet("filteraction")]
        public async Task<DataCollection<SalaryDto>> GetFilter(int id, FilterAction action, int page = 1, int take = 10)
        {
            return await _SalaryFilter.GetSalaryFilterAction(id, action, page, take);
        }

    }
}
