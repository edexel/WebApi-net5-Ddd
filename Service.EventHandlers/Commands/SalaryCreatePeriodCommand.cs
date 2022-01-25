using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventHandlers.Commands
{
    public class SalaryCreatePeriodCommand : INotification
    {
        [Required]
        [MaxLength(10)]
        public string EmployeeCode { get; set; }
   

        public List<SalaryEmployeePeriod> periods { get; set; }
    }

    public class SalaryEmployeePeriod
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal BaseSalary { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal ProductionBonus { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal CompensationBonus { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal CommissionBonus { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal Commission { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public Decimal Contributions { get; set; }
    }
}
