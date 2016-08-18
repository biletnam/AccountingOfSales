using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public class SalesEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static List<Sale> GetSales(DateTime? filterDateSaleFrom, DateTime? filterDateSaleTo, int? filterUser, int? filterProduct)
        {
            List<Sale> sales = new List<Sale>();

            //находим дату последних 3 месяцев, от текущей, чтобы ограничить поступления 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterDateSaleTo != null)
                filterDateSaleTo = filterDateSaleTo.Value.AddDays(1); //прибавляем к "дате по" 1 день, чтобы дата была включительно

            if (filterDateSaleFrom != null && filterDateSaleTo != null)
                sales = db.Sales.Where(d => d.SaleDate >= filterDateSaleFrom).Where(d => d.SaleDate < filterDateSaleTo).ToList();
            else if (filterDateSaleFrom != null && filterDateSaleTo == null)
                sales = db.Sales.Where(d => d.SaleDate >= filterDateSaleFrom).ToList();
            else if (filterDateSaleFrom == null && filterDateSaleTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterDateSaleTo.Value.Year, filterDateSaleTo.Value.Month, filterDateSaleTo.Value.Day).AddMonths(-3);
                sales = db.Sales.Where(d => d.SaleDate >= last3MonthsDateTo).Where(d => d.SaleDate < filterDateSaleTo).ToList();
            }
            else
                sales = db.Sales.Where(d => d.SaleDate >= last3Months).ToList();

            if (filterProduct != null && filterProduct != 0)
                sales = sales.Where(u => u.ProductId == filterProduct).ToList();

            if (filterUser != null && filterUser != 0)
                sales = sales.Where(u => u.User.Id == filterUser).ToList();

            return sales;
        }
    }
}
