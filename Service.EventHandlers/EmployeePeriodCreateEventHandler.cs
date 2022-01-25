using Domain;
using MediatR;
using Persistance.Database;
using Service.EventHandlers.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class EmployeePeriodCreateEventHandler : INotificationHandler<SalaryCreatePeriodCommand>
    {
        private readonly ApplicationDbContext _context;

        public EmployeePeriodCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(SalaryCreatePeriodCommand command, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var period in command.periods)
                {
                    var validSalaries = _context.Salary.Where(x => x.EmployeeCode == command.EmployeeCode && x.Year == period.Year && x.Month == period.Month);


                    if (!validSalaries.Any())
                    {
                        var singleSalary = _context.Salary.Where(x => x.EmployeeCode == command.EmployeeCode).First();

                        await _context.AddAsync(new Salary
                        {
                            Year = period.Year,
                            Month = period.Month,
                            Office = singleSalary.Office,
                            EmployeeCode = singleSalary.EmployeeCode,
                            EmployeeName = singleSalary.EmployeeName,
                            EmployeeSuname = singleSalary.EmployeeSuname,
                            Divicion = singleSalary.Divicion,
                            Position = singleSalary.Position,
                            Grade = singleSalary.Grade,
                            BeginDate = singleSalary.BeginDate,
                            Birthday = singleSalary.Birthday,
                            IdentificationNumber = singleSalary.IdentificationNumber,
                            BaseSalary = period.BaseSalary,
                            Commission = period.Commission,
                            CommissionBonus = period.CommissionBonus,
                            CompensationBonus = period.CompensationBonus,
                            Contributions = period.Contributions,
                            ProductionBonus = period.ProductionBonus
                        });
                    }
                    else
                    {
                        var saleryValue = validSalaries.FirstOrDefault();
                        saleryValue.BaseSalary = period.BaseSalary;
                        saleryValue.Commission = period.Commission;
                        saleryValue.CommissionBonus = period.CommissionBonus;
                        saleryValue.CompensationBonus = period.CompensationBonus;
                        saleryValue.Contributions = period.Contributions;
                        saleryValue.ProductionBonus = period.ProductionBonus;
                    }

                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
    }
}
