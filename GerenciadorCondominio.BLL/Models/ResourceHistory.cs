using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class ResourceHistory
    {
        public int ResourceHistoryId { get; set; }
        public decimal Value { get; set; }

        public Types Type { get; set; }

        public int Day { get; set; }
        public int MonthId { get; set; }
        public virtual Month Month { get; set; }
        public int Year { get; set; }

    }

    public enum Types
    {
        In, Out
    }
}
