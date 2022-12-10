using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UchebnayaPraktika.Components
{
    public partial class Product
    {
        public string BGColor
        {
            get
            {
                if (Count == 0 || Count == null)
                    return "#a6a6a6";
                else
                    return "#FFB5D5CA";
            }
        }
        public string NoneCount
        {
            get
            {
                if (Count == 0 || Count == null)
                {
                    return "Нет в наличии";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
