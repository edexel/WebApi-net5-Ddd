using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.DTOs
{
    public class SalaryCalculateDto
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }

        public string Divicion { get; set; }
        public string Position { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }

        public string IdentificationNumber { get; set; }

        [Column(TypeName = "decimal(5, 2)")] 
        public Decimal TotalSalary { get; set; }
    }
}
