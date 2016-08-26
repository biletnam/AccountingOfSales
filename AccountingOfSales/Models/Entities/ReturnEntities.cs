using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public class ReturnEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static List<Return> GetReturns(DateTime? filterDateReturnFrom, DateTime? filterDateReturnTo, int? filterUser, int? filterProduct, int? filterTypesReturn)
        {
            List<Return> returns = new List<Return>();

            //находим дату последних 3 месяцев, от текущей, чтобы ограничить возвраты 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterDateReturnFrom != null && filterDateReturnTo != null)
                returns = db.Returns.Where(d => d.ReturnDate >= filterDateReturnFrom).Where(d => d.ReturnDate <= filterDateReturnTo).ToList();
            else if (filterDateReturnFrom != null && filterDateReturnTo == null)
                returns = db.Returns.Where(d => d.ReturnDate >= filterDateReturnFrom).ToList();
            else if (filterDateReturnFrom == null && filterDateReturnTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterDateReturnTo.Value.Year, filterDateReturnTo.Value.Month, filterDateReturnTo.Value.Day).AddMonths(-3);
                returns = db.Returns.Where(d => d.ReturnDate >= last3MonthsDateTo).Where(d => d.ReturnDate <= filterDateReturnTo).ToList();
            }
            else
                returns = db.Returns.Where(d => d.ReturnDate >= last3Months).ToList();

            if (filterProduct != null && filterProduct != 0)
                returns = returns.Where(u => u.ProductId == filterProduct).ToList();

            if (filterUser != null && filterUser != 0)
                returns = returns.Where(u => u.User.Id == filterUser).ToList();

            if (filterTypesReturn != null && filterTypesReturn != 0)
                returns = returns.Where(t => t.TypeReturnId == filterTypesReturn).ToList();

            return returns;
        }
    }
}
