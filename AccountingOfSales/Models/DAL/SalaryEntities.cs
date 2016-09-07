using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.DAL
{
    public class SalaryEntities
    {
        public static List<Salary> GetSalaries(DateTime? filterCreateDateFrom, DateTime? filterCreateDateTo, string filterUserLogin = "")
        {
            List<Salary> salaries = new List<Salary>();

            //находим дату последних 3 месяцев, от текущей, чтобы ограничить начисления 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterCreateDateTo != null)
                filterCreateDateTo = filterCreateDateTo.Value.AddDays(1); //прибавляем к "дате по" 1 день, чтобы дата была включительно
            
            if (filterCreateDateFrom != null && filterCreateDateTo != null)
                salaries = Config.db.Salaries.Where(d => d.CreateDate >= filterCreateDateFrom).Where(d => d.CreateDate < filterCreateDateTo).ToList();
            else if (filterCreateDateFrom != null && filterCreateDateTo == null)
                salaries = Config.db.Salaries.Where(d => d.CreateDate >= filterCreateDateFrom).ToList();
            else if (filterCreateDateFrom == null && filterCreateDateTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterCreateDateTo.Value.Year, filterCreateDateTo.Value.Month, filterCreateDateTo.Value.Day).AddMonths(-3);
                salaries = Config.db.Salaries.Where(d => d.CreateDate >= last3MonthsDateTo).Where(d => d.CreateDate < filterCreateDateTo).ToList();
            }
            else
                salaries = Config.db.Salaries.Where(d => d.CreateDate >= last3Months).ToList();

            if (filterUserLogin != "")
                salaries = salaries.Where(u => u.User.Login == filterUserLogin).ToList();

            return salaries;
        }
    }
}
