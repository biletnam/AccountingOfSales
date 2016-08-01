using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;

namespace AccountingOfSales.Models.ViewModel
{
    public class IndexViewModels
    {
        public DateTime FilterDateAdmissionFrom { get; set; }
        public DateTime FilterDateAdmissionTo { get; set; }
        public string UserName { get; set; }
        PagedList.IPagedList<Admission> Admissions { get; set; }
    }
}
