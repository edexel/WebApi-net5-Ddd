using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.EventHandlers;
using Service.EventHandlers.Commands;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Api.Test.Persistence;
using static Service.Queries.Enums.Enums;

namespace Web.Api.Test
{
    [TestClass]
    public class SalariesControllerTest
    {
        [TestMethod]
        public  void TryAddNewSalariesTest()
        {
            // Preparación
            var context = ApplicationDbContextInMemory.Get();

            var newSalary = new SalaryCreateCommand
            {
                Year = 2020,
                Month = 1,
                Office = "D",
                EmployeeCode = "0590598488",
                EmployeeName = "Ede",
                EmployeeSuname = "nunez Flores",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 15,
                BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                IdentificationNumber = "1155643209",
                BaseSalary = Convert.ToDecimal(1500.20),
                Commission = Convert.ToDecimal(200.50),
                CommissionBonus = Convert.ToDecimal(100.80),
                CompensationBonus = Convert.ToDecimal(50.50),
                Contributions = Convert.ToDecimal(700.60),
                ProductionBonus = Convert.ToDecimal(150.80)

            };
            // Prueba
            var handler = new SalaryCreateEventHandler(context);
            handler.Handle(newSalary, new CancellationToken()).Wait();

            var query = new SalaryQueryService(context);
            var result =  query.GetAsync(newSalary.EmployeeCode);

            // Verificación
            Assert.AreEqual(1, result.Result.Count());
            context.Database.EnsureDeleted();


        }
        [TestMethod]
        public async Task TryCreateUpdateEmployeePeriodsTest()
        {
            // Preparación
            var context = ApplicationDbContextInMemory.Get();

            context.Salary.Add(new Salary
            {

                Year = 2020,
                Month = 1,
                Office = "D",
                EmployeeCode = "0590598494",
                EmployeeName = "Ede",
                EmployeeSuname = "nunez Flores",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 15,
                BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                IdentificationNumber = "1155643209",
                BaseSalary = Convert.ToDecimal(1500.20),
                Commission = Convert.ToDecimal(200.50),
                CommissionBonus = Convert.ToDecimal(100.80),
                CompensationBonus = Convert.ToDecimal(50.50),
                Contributions = Convert.ToDecimal(700.60),
                ProductionBonus = Convert.ToDecimal(150.80)

            });
            context.SaveChanges();

            List<SalaryEmployeePeriod> listPeriod = new List<SalaryEmployeePeriod> {

              new SalaryEmployeePeriod
              {

                Year = 2020,
                Month = 2,
                BaseSalary = Convert.ToDecimal(1800.20),
                Commission = Convert.ToDecimal(250.50),
                CommissionBonus = Convert.ToDecimal(120.80),
                CompensationBonus = Convert.ToDecimal(520.50),
                Contributions = Convert.ToDecimal(50.60),
                ProductionBonus = Convert.ToDecimal(50.80)
            },
              new SalaryEmployeePeriod
              {

                  Year = 2020,
                  Month = 3,
                  BaseSalary = Convert.ToDecimal(1200.20),
                  Commission = Convert.ToDecimal(100.50),
                  CommissionBonus = Convert.ToDecimal(150.80),
                  CompensationBonus = Convert.ToDecimal(50.50),
                  Contributions = Convert.ToDecimal(780.60),
                  ProductionBonus = Convert.ToDecimal(10.80)
              },
              new SalaryEmployeePeriod
              {

                  Year = 2020,
                  Month = 3,
                  BaseSalary = Convert.ToDecimal(1550.20),
                  Commission = Convert.ToDecimal(250.50),
                  CommissionBonus = Convert.ToDecimal(150.80),
                  CompensationBonus = Convert.ToDecimal(50.50),
                  Contributions = Convert.ToDecimal(750.60),
                  ProductionBonus = Convert.ToDecimal(150.50)
              }
              };

            var newSalaryPeriod = new SalaryCreatePeriodCommand
            {
                EmployeeCode = "0590598494",
                periods = listPeriod,
            };

            // Prueba
            var handler = new EmployeePeriodCreateEventHandler(context);
            handler.Handle(newSalaryPeriod, new CancellationToken()).Wait();

            var query = new SalaryQueryService(context);

            var result = await query.GetAsync("0590598494");

            // Verificación
            Assert.AreEqual(3, result.Count());
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task TryGetAllSalariesTest()
        {

            // Preparación
            var context = ApplicationDbContextInMemory.Get();
            context.Salary.Add(new Salary
            {
                Year = 2020,
                Month = 1,
                Office = "D",
                EmployeeCode = "0590598494",
                EmployeeName = "Ede",
                EmployeeSuname = "nunez Flores",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 15,
                BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                IdentificationNumber = "1155643209",
                BaseSalary = Convert.ToDecimal(1500.20),
                Commission = Convert.ToDecimal(200.50),
                CommissionBonus = Convert.ToDecimal(100.80),
                CompensationBonus = Convert.ToDecimal(50.50),
                Contributions = Convert.ToDecimal(700.60),
                ProductionBonus = Convert.ToDecimal(150.80)

            });
            context.SaveChanges();

            // Prueba

            var query = new SalaryQueryService(context);
            // Default parameters
            int page = 1;
            int take = 10;
            IEnumerable<int> salary = null;

            var result = await query.GetAllAsync(page, take, salary);

            // Verificación
            Assert.AreEqual(1, result.Total);
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task TryGetAllSalariesByIdTest()
        {
            // Preparación
            var context = ApplicationDbContextInMemory.Get();

            context.Salary.Add(new Salary
            {
                Year = 2020,
                Month = 1,
                Office = "D",
                EmployeeCode = "0590598494",
                EmployeeName = "Ede",
                EmployeeSuname = "nunez Flores",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 15,
                BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                IdentificationNumber = "1155643209",
                BaseSalary = Convert.ToDecimal(1500.20),
                Commission = Convert.ToDecimal(200.50),
                CommissionBonus = Convert.ToDecimal(100.80),
                CompensationBonus = Convert.ToDecimal(50.50),
                Contributions = Convert.ToDecimal(700.60),
                ProductionBonus = Convert.ToDecimal(150.80)

            });

            context.Salary.Add(new Salary
            {
                Year = 2020,
                Month = 2,
                Office = "ZZ",
                EmployeeCode = "0590598495",
                EmployeeName = "Noe",
                EmployeeSuname = "Iturbide Flores",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 16,
                BeginDate = Convert.ToDateTime("2000-05-03T00:00:00"),
                Birthday = Convert.ToDateTime("1998-01-03T00:00:00"),
                IdentificationNumber = "1155643208",
                BaseSalary = Convert.ToDecimal(1400.20),
                Commission = Convert.ToDecimal(210.50),
                CommissionBonus = Convert.ToDecimal(110.80),
                CompensationBonus = Convert.ToDecimal(40.50),
                Contributions = Convert.ToDecimal(710.60),
                ProductionBonus = Convert.ToDecimal(140.80)
            });

            context.Salary.Add(new Salary
            {
                Year = 2021,
                Month = 2,
                Office = "Z",
                EmployeeCode = "0590598496",
                EmployeeName = "Jual Pablo",
                EmployeeSuname = "Inaritu Cortez",
                Divicion = "CUSTOMEN CARE",
                Position = "MARKETIONG ASSITANT",
                Grade = 10,
                BeginDate = Convert.ToDateTime("2000-05-03T00:00:00"),
                Birthday = Convert.ToDateTime("1998-01-03T00:00:00"),
                IdentificationNumber = "1155643207",
                BaseSalary = Convert.ToDecimal(1000.20),
                Commission = Convert.ToDecimal(250.50),
                CommissionBonus = Convert.ToDecimal(150.80),
                CompensationBonus = Convert.ToDecimal(60.50),
                Contributions = Convert.ToDecimal(750.60),
                ProductionBonus = Convert.ToDecimal(180.80)
            });


            context.SaveChanges();

            // Prueba

            var query = new SalaryQueryService(context);
            // Default parameters
            int page = 1;
            int take = 10;
            IEnumerable<int> salary = new[] { 1, 2 };

            var result = await query.GetAllAsync(page, take, salary);

            // Verificación
            Assert.AreEqual(2, result.Total);
            Assert.AreEqual(1, result.Page);
            Assert.AreEqual(1, result.Pages);

            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task TryGetFilterByActionTest()
        {
            // Preparación
            var context = ApplicationDbContextInMemory.Get();

            //Create Salary record
           
            List<Salary> salariesList = CreateRecordSalaryTest();

            salariesList.ForEach(n => context.Salary.Add(n));
            context.SaveChanges();

            // Prueba
            var query = new SalaryFilterQueryService(context);

            // Default parameters
            int page = 1;
            int take = 10;
            int id = 1;

            // Empleados con la misma Oficina y Grado
            var result1 = await query.GetSalaryFilterAction(id, FilterAction.SameOfficeGrade,page,take);
            //Empleados de todas las Oficinas y con el mismo Grado.
            var result2 = await query.GetSalaryFilterAction(id, FilterAction.AllOfficeSameGrade, page, take);
            //Empleados con la misma Posición y Grado. 
            var result3 = await query.GetSalaryFilterAction(id, FilterAction.SamePositionGrade, page, take);
            //Empleados de todas las Posiciones y con el mismo Grado 
            var result4 = await query.GetSalaryFilterAction(id, FilterAction.AllpositionSameGrade, page, take);

            // Verificación

            // Empleados con la misma Oficina y Grado
            Assert.AreEqual(2, result1.Total);
            //Empleados de todas las Oficinas y con el mismo Grado.
            Assert.AreEqual(3, result2.Total);
            //Empleados con la misma Posición y Grado. 
            Assert.AreEqual(1, result3.Total);
            //Empleados de todas las Posiciones y con el mismo Grado 
            Assert.AreEqual(3, result4.Total);

            context.Database.EnsureDeleted();


        }


        [TestMethod]
        public void TryGetTotalSalaryAllEmployeeTest()
        {
            // Preparación
            var context = ApplicationDbContextInMemory.Get();

            //Create Salary record
            List<Salary> salariesList = CreateRecordSalaryTest();

            salariesList.ForEach(n => context.Salary.Add(n));
            context.SaveChanges();

            // Prueba
            var query = new SalaryCalculateQueryService(context);

            // Default parameters
            int page = 1;
            int take = 10;

            var result = query.GetSalaryCalculate(page, take);

            // Verificación

            Assert.AreEqual(2, result.Total);

            var totalSalary =   salariesList.ToLookup(c => c.EmployeeCode)
                                            .Select(g => g.First())
                                            .Select(x => Convert.ToDecimal(String.Format("{0:0.00}", x.BaseSalary + x.ProductionBonus +
                                                            ((x.CommissionBonus * 75) / 100) + //   Other Income 
                                                            ((((x.BaseSalary + x.Commission) * 8) / 100) + x.Commission)
                                                            + x.Commission))).FirstOrDefault();

            var calculatetotal = result.Items.FirstOrDefault().TotalSalary;

            Assert.AreEqual(totalSalary, calculatetotal);
            context.Database.EnsureDeleted();


        }

        #region HelpClass
        public static List<Salary> CreateRecordSalaryTest()
        {
           

            List<Salary> salariesList = new List<Salary>()
            {
                new Salary(){
                        Year = 2020,
                        Month = 1,
                        Office = "D",
                        EmployeeCode = "0590598488",
                        EmployeeName = "juan ",
                        EmployeeSuname = "nunez perez",
                        Divicion = "CUSTOMEN CARE",
                        Position = "MARKETIONG ASSITANT",
                        Grade = 15,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(1000.20),
                        Commission = Convert.ToDecimal(200.50),
                        CommissionBonus = Convert.ToDecimal(100.80),
                        CompensationBonus = Convert.ToDecimal(50.50),
                        Contributions = Convert.ToDecimal(700.60),
                        ProductionBonus = Convert.ToDecimal(150.80)
                },
                new Salary(){
                        Year = 2020,
                        Month = 2,
                        Office = "D",
                        EmployeeCode = "0590598488",
                        EmployeeName = "Ede",
                        EmployeeSuname = "nunez Flores",
                        Divicion = "CUSTOMEN CARE",
                        Position = "CUSTOMER DIRECTOR",
                        Grade = 15,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(1500.20),
                        Commission = Convert.ToDecimal(200.50),
                        CommissionBonus = Convert.ToDecimal(110.80),
                        CompensationBonus = Convert.ToDecimal(50.50),
                        Contributions = Convert.ToDecimal(720.60),
                        ProductionBonus = Convert.ToDecimal(150.80)
                },
                new Salary(){
                        Year = 2021,
                        Month = 1,
                        Office = "A",
                        EmployeeCode = "0590598489",
                        EmployeeName = "Martha",
                        EmployeeSuname = "Gonzales Solorio",
                        Divicion = "CUSTOMEN CARE",
                        Position = "CUSTOMER DIRECTOR",
                        Grade = 18,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(1200.20),
                        Commission = Convert.ToDecimal(250.50),
                        CommissionBonus = Convert.ToDecimal(100.80),
                        CompensationBonus = Convert.ToDecimal(50.50),
                        Contributions = Convert.ToDecimal(700.60),
                        ProductionBonus = Convert.ToDecimal(150.80)
                },
                new Salary(){
                        Year = 2021,
                        Month = 2,
                        Office = "ZZ",
                        EmployeeCode = "0590598489",
                        EmployeeName = "Pedro",
                        EmployeeSuname = "Paramo Lujan",
                        Divicion = "CUSTOMEN CARE",
                        Position = "CUSTOMER DIRECTOR",
                        Grade = 18,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(1400.20),
                        Commission = Convert.ToDecimal(200.50),
                        CommissionBonus = Convert.ToDecimal(100.80),
                        CompensationBonus = Convert.ToDecimal(58.50),
                        Contributions = Convert.ToDecimal(500.60),
                        ProductionBonus = Convert.ToDecimal(150.80)
                },
                new Salary(){
                        Year = 2020,
                        Month = 5,
                        Office = "A",
                        EmployeeCode = "0590598488",
                        EmployeeName = "Gabriela",
                        EmployeeSuname = "Almanza Jimenez",
                        Divicion = "CUSTOMEN CARE",
                        Position = "MARKETIONG ASSITANT",
                        Grade = 18,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(900.20),
                        Commission = Convert.ToDecimal(300.50),
                        CommissionBonus = Convert.ToDecimal(140.80),
                        CompensationBonus = Convert.ToDecimal(30.50),
                        Contributions = Convert.ToDecimal(200.60),
                        ProductionBonus = Convert.ToDecimal(160.80)
                },
                new Salary(){
                        Year = 2020,
                        Month = 6,
                        Office = "C",
                        EmployeeCode = "0590598488",
                        EmployeeName = "Rosalia",
                        EmployeeSuname = "Carrillo Diaz",
                        Divicion = "CUSTOMEN CARE",
                        Position = "SALES MANAGER",
                        Grade = 15,
                        BeginDate = Convert.ToDateTime("2000-09-03T00:00:00"),
                        Birthday = Convert.ToDateTime("1994-07-03T00:00:00"),
                        IdentificationNumber = "1155643209",
                        BaseSalary = Convert.ToDecimal(1100.20),
                        Commission = Convert.ToDecimal(700.50),
                        CommissionBonus = Convert.ToDecimal(110.80),
                        CompensationBonus = Convert.ToDecimal(590.50),
                        Contributions = Convert.ToDecimal(757.60),
                        ProductionBonus = Convert.ToDecimal(100.80)
                },
            };

            
            return salariesList;
        }
        #endregion



    }
}
