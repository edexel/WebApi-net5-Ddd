using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Salary
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Office { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSuname { get; set; }

        public string Divicion { get; set; }
        public string Position { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }

        public string IdentificationNumber { get; set; }

        public Decimal BaseSalary { get; set; }
        public Decimal ProductionBonus { get; set; }
        public Decimal CompensationBonus { get; set; }

        public Decimal CommissionBonus { get; set; }
        public Decimal Commission { get; set; }

        public Decimal Contributions { get; set; }

    }
}
