using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Neatza
{
    public class PizzaOrder
    {
        public Pizza Pizza { get; set; } = new();

        public decimal GetTotal()
        {
            return Pizza.GetPrice();
        }

    }
}
 