using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.EventHandlers.Commands
{
   public class SalaryCreateCommand : INotification
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        [MaxLength(30)]
        public string Office { get; set; }
        [Required]
        [MaxLength(10)]
        public string EmployeeCode { get; set; }
        [Required]
        [MaxLength(30)]
        public string EmployeeName { get; set; }
        [Required]
        [MaxLength(30)]
        public string EmployeeSuname { get; set; }
        [Required]
        [MaxLength(30)]
        public string Divicion { get; set; }
        [Required]
        [MaxLength(30)]
        public string Position { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1950-01-01", "2000-01-01",
    ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }
        [Required]
        [MaxLength(10)]
        public string IdentificationNumber { get; set; }
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
