using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public partial class AdmissionEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static List<Admission> GetAdmissions(DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo, int? filterUser, int? filterProvider)
        {
            List<Admission> admissions = new List<Admission>();

            //находим дату последних 3 месяцев, от текущей, чтобы ограничить поступления 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterDateAdmissionTo != null)
                filterDateAdmissionTo = filterDateAdmissionTo.Value.AddDays(1); //прибавляем к "дате по" 1 день, чтобы дата была включительно

            if (filterDateAdmissionFrom != null && filterDateAdmissionTo != null)
                admissions = db.Admissions.Where(d => d.AdmissionDate > filterDateAdmissionFrom).Where(d => d.AdmissionDate < filterDateAdmissionTo).ToList();
            else if (filterDateAdmissionFrom != null && filterDateAdmissionTo == null)
                admissions = db.Admissions.Where(d => d.AdmissionDate > filterDateAdmissionFrom).ToList();
            else if (filterDateAdmissionFrom == null && filterDateAdmissionTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterDateAdmissionTo.Value.Year, filterDateAdmissionTo.Value.Month, filterDateAdmissionTo.Value.Day).AddMonths(-3);
                admissions = db.Admissions.Where(d => d.AdmissionDate > last3MonthsDateTo).Where(d => d.AdmissionDate < filterDateAdmissionTo).ToList();
            }
            else
                admissions = db.Admissions.Where(d => d.AdmissionDate > last3Months).ToList();

            if (filterProvider != null && filterProvider != 0)
                admissions = admissions.Where(u => u.ProviderId == filterProvider).ToList();

            if (filterUser != null && filterUser != 0)
                admissions = admissions.Where(u => u.User.Id == filterUser).ToList();

            return admissions;
        }
    }
}
