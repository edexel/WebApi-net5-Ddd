using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.EventHandlers.Commands;
using Service.Queries.DTOs;
using Service.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/salaries")]
    //salaries
    public class SalaryController: ControllerBase
    {
        private readonly ISalaryQueryService _salaryQueryService;
        private readonly IMediator _mediator;
        public SalaryController( ISalaryQueryService salaryQueryService, IMediator mediator)
        {
            _salaryQueryService = salaryQueryService;
            _mediator = mediator;
        }

        /// <summary>
        /// This method return List item salaries (default paginate page = 1, take = 10, ids = null)
        /// </summary>
        /// <param name="page">This  parameter define  the number of page to show</param>
        /// <param name="take">This parameter define how much item you want to get</param>
        /// <param name="ids">This parameter set the especify ids that you want to get</param>           
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        /// <response code="400">BadRequest. Error de validacióon de datos.</response> 
        [HttpGet]
        public async Task<DataCollection<SalaryDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> salaries = null;
            if (!string.IsNullOrEmpty(ids))
                salaries = ids.Split(',').Select(x => Convert.ToInt32(x));


            return await  _salaryQueryService.GetAllAsync(page, take, salaries);
        }

        /// <summary>
        /// This method return item Salary by employeeCode
        /// </summary>
        /// <param name="employeeCode">This parameter define the employeeCode </param>          
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Bad Request. Error de validacióon de datos.</response> 
        //salaries/0841868491
        [HttpGet("getbyemployeecode/{employeeCode}")]
        public async Task<IEnumerable<SalaryDto>> Get(string employeeCode)
        {
            return await _salaryQueryService.GetAsync(employeeCode);
        }

        /// <summary>
        /// This method return item Salary by id
        /// </summary>
        /// <param name="id">This parameter define the id </param>          
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Bad Request. Error de validacióon de datos.</response> 
        //salaries/1
        [HttpGet("{id}")]
        public async Task<SalaryDto> Get(int id)
        {
            return await _salaryQueryService.GetAsync(id);
        }


        /// <summary>
        /// This method create new salary record
        /// </summary>
        /// <param name="command">This objet represent a new the salary record</param>          
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        /// <response code="400">Bad Request. Error de validacióon de datos.</response> 
        //Salaries
        [HttpPost]
        public async Task<IActionResult> Create(SalaryCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
        /// <summary>
        /// This method create new salary periods records or update exist it
        /// </summary>
        /// <param name="command">This list object represent periods of salary, one employee </param>          
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        /// <response code="400">Bad Request. Error de validacióon de datos.</response> 
        [HttpPut]
        public async Task<IActionResult> CreateUpdatePeriod(SalaryCreatePeriodCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
     

    }
}
