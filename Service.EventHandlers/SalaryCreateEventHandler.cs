using Domain;
using MediatR;
using Persistance.Database;
using Service.EventHandlers.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public  class SalaryCreateEventHandler : INotificationHandler<SalaryCreateCommand>
    {
        private readonly ApplicationDbContext _context;

        public SalaryCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

         public async Task Handle(SalaryCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _context.AddAsync(new Salary
                {
                    Year = command.Year,
                    Month = command.Month,
                    Office = command.Office,
                    EmployeeCode = command.EmployeeCode,
                    EmployeeName = command.EmployeeName,
                    EmployeeSuname = command.EmployeeSuname,
                    Divicion = command.Divicion,
                    Position = command.Position,
                    Grade = command.Grade,
                    BeginDate = command.BeginDate,
                    Birthday = command.Birthday,
                    IdentificationNumber = command.IdentificationNumber,
                    BaseSalary = command.BaseSalary,
                    Commission = command.Commission,
                    CommissionBonus = command.CommissionBonus,
                    CompensationBonus = command.CompensationBonus,
                    Contributions = command.Contributions,
                    ProductionBonus = command.ProductionBonus
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

         
        }
    }
}
