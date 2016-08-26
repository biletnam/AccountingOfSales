using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public class OtherCostsEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static List<OtherCosts> GetOtherCosts(DateTime? filterDateCostFrom, DateTime? filterDateCostTo)
        {
            List<OtherCosts> otherCosts = new List<OtherCosts>();

            //находим дату последних 3 месяцев, от текущей, чтобы ограничить расходы 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterDateCostFrom != null && filterDateCostTo != null)
                otherCosts = db.OtherCosts.Where(d => d.CostsDate >= filterDateCostFrom).Where(d => d.CostsDate <= filterDateCostTo).ToList();
            else if (filterDateCostFrom != null && filterDateCostTo == null)
                otherCosts = db.OtherCosts.Where(d => d.CostsDate >= filterDateCostFrom).ToList();
            else if (filterDateCostFrom == null && filterDateCostTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterDateCostTo.Value.Year, filterDateCostTo.Value.Month, filterDateCostTo.Value.Day).AddMonths(-3);
                otherCosts = db.OtherCosts.Where(d => d.CostsDate >= last3MonthsDateTo).Where(d => d.CostsDate <= filterDateCostTo).ToList();
            }
            else
                otherCosts = db.OtherCosts.Where(d => d.CostsDate >= last3Months).ToList();

            return otherCosts;
        }
    }
}
