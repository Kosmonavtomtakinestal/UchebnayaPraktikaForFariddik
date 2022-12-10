using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchebnayaPraktika.Components
{
    public partial class ProductOrder
    {
        double sum = 0;
            decimal? generalSum = 0;
        public double CostProd
        {
            get
            {
                double ddd = (double)(Count * Product.Cost);
                sum += ddd;
                return ddd;
            }
        }
        public decimal? GeneralCost
        {
            get
            {

                foreach (var item in DbConnection.db.ProductCountry.ToList())
                {
                    generalSum += item.Product.Cost;
                }
                return generalSum;
            }
        }
    }
}
