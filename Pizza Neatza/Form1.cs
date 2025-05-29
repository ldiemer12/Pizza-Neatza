using System.Text;

namespace Pizza_Neatza
{
    public partial class PatsPizzaPalooza : Form
    {
        public PatsPizzaPalooza()
        {
            InitializeComponent();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {

            if (!rdoHandTossed.Checked && !rdoPan.Checked)
            {
                MessageBox.Show("Please select a crust type.", "Missing Crust", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rdoMarinara.Checked && !rdoBBQ.Checked && !rdoNoSauce.Checked)
            {
                MessageBox.Show("Please select a sauce option.", "Missing Sauce Option", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Pizza pizza = new Pizza();

            if (rdoSmall.Checked) pizza.Size = PizzaSize.Small;
            else if (rdoMedium.Checked) pizza.Size = PizzaSize.Medium;
            else if (rdoLarge.Checked) pizza.Size = PizzaSize.Large;

            if (rdoHandTossed.Checked) pizza.CrustType = CrustType.HandTossed;
            else if (rdoPan.Checked) pizza.CrustType = CrustType.Pan;

            pizza.IsStuffedCrust = chkStuffedCrust.Checked;


            if (rdoMarinara.Checked) pizza.Sauce = SauceType.Marinara;
            else if (rdoBBQ.Checked) pizza.Sauce = SauceType.BBQ;
            else if (rdoNoSauce.Checked) pizza.Sauce = SauceType.NoSauce;

            pizza.Toppings = GetSelectedToppingsFromForm();

            pizza.AddBreadsticks = chkBreadsticks.Checked;
            pizza.AddCheesyBreadsticks = chkCheesyBreadsticks.Checked;

            decimal price = pizza.GetPrice();
            lblPrice.Text = $"Total: {price:C}";

            StringBuilder summary = new();
            summary.AppendLine("Order Summary:");

            summary.AppendLine($"Size: {pizza.Size}");


            summary.AppendLine($"Crust: {pizza.CrustType}" + (pizza.IsStuffedCrust ? " (Stuffed)" : ""));


            summary.AppendLine($"Sauce: {pizza.Sauce}");

            summary.AppendLine("Toppings:");
            foreach (var t in pizza.Toppings)
            {

                summary.AppendLine($"- {t.Topping.Name}({t.Amount})");
            }

            if (pizza.AddBreadsticks)
                summary.AppendLine("Includes Breadsticks");

            if (pizza.AddCheesyBreadsticks)
                summary.AppendLine("Includes Cheesy Breadsticks");

            summary.AppendLine(lblPrice.Text);
            MessageBox.Show(summary.ToString(), "Order Summary");
        }

        private List<SelectedTopping> GetSelectedToppingsFromForm()
        {
            List<SelectedTopping> selectedToppings = new();

            for (int i = 0; i < tblToppings.RowCount; i++)
            {
                var label = tblToppings.GetControlFromPosition(0, i) as Label;
                var combo = tblToppings.GetControlFromPosition(1, i) as ComboBox;

                if (label != null && combo != null && combo.SelectedItem != null)
                {
                    string toppingName = label.Text;
                    string amountText = combo.SelectedItem.ToString();

                    if (Enum.TryParse<ToppingAmount>(amountText, out var amount))
                    {
                        var topping = new SelectedTopping
                        {
                            Topping = new Toppings
                            {
                                Name = toppingName,
                                IsPremiumItem = IsPremiumItem(toppingName)
                            },
                            Amount = amount
                        };
                        selectedToppings.Add(topping);
                    }
                }
            }
            return selectedToppings;
        }

        private bool IsPremiumItem(string toppingName)
        {
            var premiumToppings = new List<string>
            {"Pepperoni", "Sausage", "Ground Beef", "Chicken"};
            return premiumToppings.Contains(toppingName);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rdoSmall.Checked = false;
            rdoMedium.Checked = false;

            rdoHandTossed.Checked = false;
            rdoPan.Checked = false;

            rdoMarinara.Checked = false;
            rdoBBQ.Checked = false;
            rdoNoSauce.Checked = false;

            chkBreadsticks.Checked = false;
            chkCheesyBreadsticks.Checked = false;
            chkStuffedCrust.Checked = false;

            for (int i = 0; i < tblToppings.RowCount; i++)
            {
                var combo = tblToppings.GetControlFromPosition(1, i) as ComboBox;
                if (combo != null)
                {
                    combo.SelectedIndex = -1;
                }
                lblPrice.Text = "";
            }
        }
    }
}
