using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Database.Configuration
{
    public class SalaryConfiguration
    {
       
        public SalaryConfiguration(EntityTypeBuilder<Salary> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.Id);
            entityBuilder.Property(x => x.IdentificationNumber).IsRequired().HasMaxLength(10);
            entityBuilder.Property(x => x.EmployeeCode).IsRequired().HasMaxLength(10);
            entityBuilder.Property(x => x.EmployeeName).IsRequired().HasMaxLength(150);
            entityBuilder.Property(x => x.EmployeeSuname).IsRequired().HasMaxLength(150);
            entityBuilder.Property(x => x.Commission).HasPrecision(5, 2);
            entityBuilder.Property(x => x.CommissionBonus).HasPrecision(5, 2);
            entityBuilder.Property(x => x.CompensationBonus).HasPrecision(5, 2);
            entityBuilder.Property(x => x.Contributions).HasPrecision(5, 2);
            entityBuilder.Property(x => x.ProductionBonus).HasPrecision(5, 2);
            entityBuilder.Property(x => x.BaseSalary).HasPrecision(5, 2);

            
            // Employee by default
            CreateSalaryDataByPopulate(entityBuilder);

        }

        public static void CreateSalaryDataByPopulate(EntityTypeBuilder<Salary>  entityBuilder)
        {
            var employees = new List<Salary>();
            var random = new Random();

            for (int e = 1; e <= 100; e++)
            {
                var year = 2021;
                var name = GetRandomData("name");
                var lasName = GetRandomData("last");
                var code = GetRandomData("code");
                var divicion = GetRandomData("division");
                var position = GetRandomData("position");
                var grade = random.Next(5, 20);
                var begin = DateTime.Parse(GetRandomData("date"));
                var birth = DateTime.Parse(GetRandomData("birthday"));
                var number = GetRandomData("code");
                var salarybase = Convert.ToDecimal(GetRandomData("decimal"));
                int month = 0;
                var meses = new List<int>();

                for (int i = 0; i < 6; i++)
                {

                    var id = Convert.ToInt32(e.ToString() + i.ToString());

                    (month, meses) = GetNumberRandom(meses);

                    employees.Add(new Salary
                    {
                        Id = id,
                        Year = year,
                        Month = month,
                        Office = GetRandomData("office"),
                        EmployeeCode = code,
                        EmployeeName = name,
                        EmployeeSuname = lasName,
                        Divicion = divicion,
                        Position = position,
                        Grade = random.Next(5, 20),
                        BeginDate = begin,
                        Birthday = birth,
                        IdentificationNumber = number,
                        BaseSalary = salarybase,
                        Commission = Convert.ToDecimal(GetRandomData("decimal")),
                        CommissionBonus = Convert.ToDecimal(GetRandomData("decimal")),
                        CompensationBonus = Convert.ToDecimal(GetRandomData("decimal")),
                        Contributions = Convert.ToDecimal(GetRandomData("decimal")),
                        ProductionBonus = Convert.ToDecimal(GetRandomData("decimal"))
                    });
                }
            }

            entityBuilder.HasData(employees);
        }

        public static (int, List<int>) GetNumberRandom(List<int> meses)
        {
            var random = new Random();
            var month = 0;

            while (!meses.Contains(month) && meses.Count < 6)
            {
                month = random.Next(1, 12);

                if (!meses.Contains(month) && month != 0)
                {
                    meses.Add(month);
                    break;
                }
                month = 0;
            }
            return (month, meses);
        }



        public static string GetRandomData(string type)
        {
            string[] names = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "abby", "abigail", "adele", "adrian" };
            string[] lastNames = { "abbott", "acosta", "adams", "adkins", "aguilar", "Smith", "Jason", "Cassio", "Maxwell", "Perez", "Torres", "Nunez" };
            string[] divicion = { "OPERATION", "SALES", "MARKETING", "CUSTOMEN CARE" };
            string[] position = { "CARGO MANAGER", "HEAD OF CARGO", "SALES MANAGER", "MARKETIONG ASSITANT", "CUSTOMER DIRECTOR" };
            string[] offices = { "C", "D", "A", "ZZ" };
            var characters = "0123456789";
            var Charsarr = new char[10];
            DateTime start = new(2000, 03, 03);
            DateTime startbirth = new(1990, 01, 01);
            Random random = new();
            string result = "";
            int index;
            int range;

            switch (type)
            {
                case "name":
                    index = random.Next(0, names.Length);
                    result = names[index];
                    break;
                case "last":
                    index = random.Next(0, lastNames.Length);
                    result = lastNames[index];
                    break;
                case "office":
                    index = random.Next(0, offices.Length);
                    result = offices[index];
                    break;
                case "code":
                    for (int i = 0; i < Charsarr.Length; i++)
                        Charsarr[i] = characters[random.Next(characters.Length)];
                    result = new String(Charsarr);
                    break;
                case "division":
                    index = random.Next(0, divicion.Length);
                    result = divicion[index];
                    break;
                case "position":
                    index = random.Next(0, position.Length);
                    result = position[index];
                    break;
                case "date":
                    range = (DateTime.Today - start).Days;
                    result = start.AddDays(random.Next(range)).ToString();
                    break;
                case "birthday":
                    range = (new DateTime(2000, 01, 01) - startbirth).Days;
                    result = startbirth.AddDays(random.Next(range)).ToString();
                    break;
                case "decimal":
                    result = Math.Round(new decimal(random.NextDouble()) * 1000, 2).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture).ToString();
                    break;

            }
            return result;
        }
    }
}
