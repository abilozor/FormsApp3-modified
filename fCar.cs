using System;
using System.Windows.Forms;

namespace FormsApp2
{
    public partial class fCar : Form
    {
        public Car TheCar;
        public fCar(Car car)
        {
            InitializeComponent();
            TheCar = car;
        }

        public fCar(ref Car car)
        {
            InitializeComponent();
            TheCar = car;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            TheCar.ModelName = tbModelName.Text.Trim();
            TheCar.Manufacturer = tbManufacturer.Text.Trim();
            TheCar.YearOfProduction = int.Parse(tbYearOfProduction.Text.Trim());
            TheCar.EngineCapacity = double.Parse(tbEngineCapacity.Text.Trim());
            TheCar.SeatCount = int.Parse(tbSeatCount.Text.Trim());
            TheCar.IsElectric = bool.Parse(tbIsElectric.Text.Trim());
            TheCar.AverageFuelConsumption = double.Parse(tbAverageFuelConsumption.Text.Trim());

            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e){DialogResult = DialogResult.Cancel;}

        private void fCar_Load(object sender, EventArgs e)
        {
            if(TheCar != null)
            {
                tbModelName.Text = TheCar.ModelName;
                tbManufacturer.Text = TheCar.Manufacturer;
                tbYearOfProduction.Text = TheCar.YearOfProduction.ToString();
                tbEngineCapacity.Text = TheCar.EngineCapacity.ToString();
                tbSeatCount.Text = TheCar.SeatCount.ToString();
                tbIsElectric.Text = TheCar.IsElectric.ToString();
                tbAverageFuelConsumption.Text = TheCar.AverageFuelConsumption.ToString();
            }
        }
    }
}
