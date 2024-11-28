using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FormsApp2
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            gvCars.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ModelName";
            column.Name = "Модель";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Manufacturer";
            column.Name = "Виробник";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "YearOfProduction";
            column.Name = "Рік випуску";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "EngineCapacity";
            column.Name = "Об'єм двигуна (л)";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "SeatCount";
            column.Name = "Кількість місць";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "IsElectric";
            column.Name = "Електричний";
            gvCars.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "AverageFuelConsumption";
            column.Name = "Середня витрата палива (л/100км)";
            gvCars.Columns.Add(column);

            bindSrcCars.Add(new Car("Tesla Model S", "Tesla", 2022, 0, 5, true, 0));
            EventArgs args = new EventArgs(); OnResize(args);
        }
        private void fMain_Resize(object sender, EventArgs e) 
        {
            int buttonSize = 5 * btnAdd.Width + 2 * tsSeparator1.Width + 30;
            btnEdit.Margin = new Padding(Width - buttonSize, 0, 0, 0);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Car car = new Car();

            fCar fc = new fCar(car);
            if(fc.ShowDialog() == DialogResult.OK) 
            {
                bindSrcCars.Add(car);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Car car = (Car)bindSrcCars.List[bindSrcCars.Position];

            fCar fc = new fCar(ref car);
            if(fc.ShowDialog() == DialogResult.OK)
            {
                bindSrcCars.List[bindSrcCars.Position] = car;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Видалити поточний запис?", "Видалення запису", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcCars.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                bindSrcCars.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Закрити застосунок?", "Вихід з програми", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK ) 
            {
                Application.Exit();
            }
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у текстовому форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            StreamWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (Car car in bindSrcCars.List)
                    {
                        sw.Write(car.ModelName + "\t" + car.Manufacturer + "\t" +
                                 car.YearOfProduction + "\t" + car.EngineCapacity + "\t" +
                                 car.SeatCount + "\t" + car.IsElectric + "\t" + car.AverageFuelConsumption + "\t\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка: \n{ex.Message}",
                                    "Помилка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.cars)|*.cars|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            BinaryWriter bw;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (Car car in bindSrcCars.List)
                    {
                        bw.Write(car.ModelName);
                        bw.Write(car.Manufacturer);
                        bw.Write(car.YearOfProduction);
                        bw.Write(car.EngineCapacity);
                        bw.Write(car.SeatCount);
                        bw.Write(car.IsElectric);
                        bw.Write(car.AverageFuelConsumption);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            StreamReader sr;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcCars.Clear();
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;

                try
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] split = s.Split('\t');
                        CarSample car = new Car(split[0], split[1], int.Parse(split[2]), double.Parse(split[3]), 
                            int.Parse(split[4]), bool.Parse(split[5]), double.Parse(split[6])); 
                        bindSrcCars.Add(car);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файли даних (*.cars)|*.cars|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcCars.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    CarSample car;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        car = new Car();
                        for (int i = 1; i <= 7; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    car.ModelName = br.ReadString();
                                    break;
                                case 2:
                                    car.Manufacturer = br.ReadString();
                                    break;
                                case 3:
                                    car.YearOfProduction = br.ReadInt32();
                                    break;
                                case 4:
                                    car.EngineCapacity = br.ReadDouble();
                                    break;
                                case 5:
                                    car.SeatCount = br.ReadInt32();
                                    break;
                                case 6:
                                    car.IsElectric = br.ReadBoolean();
                                    break;
                                case 7:
                                    car.AverageFuelConsumption = br.ReadDouble();
                                    break;
                            }
                        }
                        bindSrcCars.Add(car);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }
        }
    }
}
