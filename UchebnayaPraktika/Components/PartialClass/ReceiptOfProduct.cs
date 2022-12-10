using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchebnayaPraktika.Components
{
    partial class ReceiptOfProduct
    {
        public decimal? GeneralCost
        {
            get
            {
                decimal? generalSum = 0;
                foreach (var item in ProductSupplier)
                    generalSum += item.PriceUnit * item.Count;
                TotalCost = generalSum;
                return generalSum;
            }
        }
    }
}
