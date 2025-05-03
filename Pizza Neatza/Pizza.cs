using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Neatza
{
    public class Pizza
    {
        public PizzaSize Size { get; set; }
        public CrustType CrustType { get; set; }
        public bool IsStuffedCrust { get; set; }

        public SauceType Sauce { get; set; }
        public List<SelectedTopping> Toppings { get; set; } = new();
        public bool AddBreadsticks { get; set; }
        public bool AddCheesyBreadsticks { get; set; }


        public decimal GetPrice()
        {
            decimal price = 0;

            switch (Size)
            {
                case PizzaSize.Small:
                    price += 8;
                    break;

                case PizzaSize.Medium:
                    price += 10;
                    break;

                case PizzaSize.Large:
                    price += 12;
                    break;
            }

            foreach (var topping in Toppings)
            {

                if (topping.Topping.IsPremiumItem)
                {
                    if (topping.Amount == ToppingAmount.Extra)
                        price += 1.50m;
                    else
                        price += 1.00m;
                }
                else
                {
                    if (topping.Amount == ToppingAmount.Extra)
                        price += 1.00m;
                    else
                        price += 0.50m;
                }
            }
            if (AddBreadsticks)
                price += 5;
            if (AddCheesyBreadsticks)
                price += 7;
            return price;
        }

    }
}