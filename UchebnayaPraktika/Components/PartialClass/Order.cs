using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchebnayaPraktika.Components
{
    partial class Order
    {
        public decimal? GeneralCost
        {
            get
            {
                decimal? generalSum = 0;
                foreach (var item in ProductOrder)
                    generalSum += item.Product.Cost * item.Count;
                TotalCost = generalSum;
                return generalSum;
            }
        }
    }
}
