using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebFrameworks_CA1.Question1;
using WebFrameworks_CA1.Question2;
using WebFrameworks_CA1.Question3;

namespace WebFrameworks_CA1
{
    public partial class Form1 : Form
    {
        private readonly Lotto lotto = new Lotto();
        private RainfallData rainfall = new RainfallData(4, 4);
        private Runners runners = new Runners();
        private string season;
        BindingList<hseEmployee> emps = new BindingList<hseEmployee>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Question 2
            SeasonsDataGridView.Columns.Add("Season", "Season");
            SeasonsDataGridView.Columns.Add("2016", "2016");
            SeasonsDataGridView.Columns.Add("2017", "2017");
            SeasonsDataGridView.Columns.Add("2018", "2018");
            SeasonsDataGridView.Columns.Add("2019", "2019");

            int[,] data = rainfall.data[0];
            string[] seasons = new string[] { "Spring", "Summer", "Autumn", "Winter" };
            int x = 0;

            foreach (string s in seasons)
            {
                SeasonsDataGridView.Rows.Add(s, data[x, 0], data[x, 1], data[x, 2], data[x, 3]);
                x++;
            }
            //END//

            //Last part of Question 2
            Seasons.Items.Add("All Seasons");
            Seasons.Items.Add("Spring");
            Seasons.Items.Add("Summer");
            Seasons.Items.Add("Autumn");
            Seasons.Items.Add("Winter");

            Years.Items.Add("All Years");
            Years.Items.Add("2016");
            Years.Items.Add("2017");
            Years.Items.Add("2018");
            Years.Items.Add("2019");
            //END//

            //Question 3
            EmployeesDataGridView.DataSource = emps;
            //END//

        }

        //Question 1
        private void GenerateLottoLine_Click(object sender, EventArgs e)
        {
            var lottoNums = lotto.LottoLine();

            lottoNum1.Text = lottoNums.ElementAt(0).ToString();
            lottoNum2.Text = lottoNums.ElementAt(1).ToString();
            lottoNum3.Text = lottoNums.ElementAt(2).ToString();
            lottoNum4.Text = lottoNums.ElementAt(3).ToString();
            lottoNum5.Text = lottoNums.ElementAt(4).ToString();
            lottoNum6.Text = lottoNums.ElementAt(5).ToString();
        }
        //END//

        //Question 2
        private void ButtonToAddRainfallData_Click(object sender, EventArgs e)
        {
            string year = YearTextBox.Text;
            string rainfall = RainfallTextBox.Text;

            if (year != null && rainfall != null && year != "" && rainfall != "" && season != null)
            {
                if (year.All(char.IsDigit) && rainfall.All(char.IsDigit))
                {
                    if (SeasonsDataGridView.Columns.Contains(year)) ;
                    else SeasonsDataGridView.Columns.Add(year, year);


                    foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                    {
                        if ((string)row.Cells["Season"].Value == season)
                        {
                            row.Cells[year].Value = rainfall;
                            break;
                        }
                    }
                    Years.Items.Add(year);
                }
            }
            //TODO:: Show error messages when something was done wrong....
            //TODO:: Make sure nothing happens if a Radio Button was not selected....
        }

        private void SpringRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            season = SpringRadioButton.Text;
        }

        private void SummerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            season = SummerRadioButton.Text;
        }

        private void AutumnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            season = AutumnRadioButton.Text;
        }

        private void WinterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            season = WinterRadioButton.Text;
        }

        private void DriestYearButton_Click(object sender, EventArgs e)
        {
            int lastRow = SeasonsDataGridView.Rows.Count - 1;
            int driest = Int32.MaxValue;
            string driestYear = "";

            foreach (DataGridViewColumn col in SeasonsDataGridView.Columns)
            {
                int total = 0;
                if (col.Name == "Season") continue;
                foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                {

                    if (row.Index == lastRow) break;
                    try
                    {
                        total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                    }
                    catch (System.NullReferenceException ex)
                    {
                        total += 0;
                    }


                }
                driest = Math.Min(driest, total);
            }
            driestYear = FindColumnNameByColumnTotal(driest, lastRow);
            DriestYearTextBox.Text = driestYear;
        }

        private void WettestYearButton_Click(object sender, EventArgs e)
        {
            int lastRow = SeasonsDataGridView.Rows.Count - 1;
            int wettest = Int32.MinValue;
            string wettestYear = "";

            foreach (DataGridViewColumn col in SeasonsDataGridView.Columns)
            {
                int total = 0;
                if (col.Name == "Season") continue;
                foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                {

                    if (row.Index == lastRow) break;
                    try
                    {
                        total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                    }
                    catch (System.NullReferenceException ex)
                    {
                        total += 0;
                    }
                }
                wettest = Math.Max(wettest, total);
            }
            wettestYear = FindColumnNameByColumnTotal(wettest, lastRow);
            WettestYearTextBox.Text = wettestYear;
        }

        private void DriestSeasonButton_Click(object sender, EventArgs e)
        {

            int lastRow = SeasonsDataGridView.Rows.Count - 1;
            int driest = Int32.MaxValue;
            string driestSeason = "";
            int total = 0;

            total = rowTotal(0, lastRow);
            driest = Math.Min(driest, total);

            total = rowTotal(1, lastRow);
            driest = Math.Min(driest, total);

            total = rowTotal(2, lastRow);
            driest = Math.Min(driest, total);

            total = rowTotal(3, lastRow);
            driest = Math.Min(driest, total);

            for(int i = 0; i <= 3; i ++)
            {
                driestSeason = FindRowNameByRowTotal(driest, lastRow, i);
                if (driestSeason != null) break;
            }
            DriestSeasonTextBox.Text = driestSeason;
        }

        private void WettestSeasonButton_Click(object sender, EventArgs e)
        {
            int lastRow = SeasonsDataGridView.Rows.Count - 1;
            int wettest = Int32.MinValue;
            string wettestSeason = "";
            int total = 0;

            total = rowTotal(0, lastRow);
            wettest = Math.Max(wettest, total);

            total = rowTotal(1, lastRow);
            wettest = Math.Max(wettest, total);

            total = rowTotal(2, lastRow);
            wettest = Math.Max(wettest, total);

            total = rowTotal(3, lastRow);
            wettest = Math.Max(wettest, total);

            for (int i = 0; i <= 3; i++)
            {
                wettestSeason = FindRowNameByRowTotal(wettest, lastRow, i);
                if (wettestSeason != null) break;
            }
            WettestSeasonTextBox.Text = wettestSeason;
        }

        private string FindColumnNameByColumnTotal(int value, int lastRow)
        {
            string found = null;

            foreach (DataGridViewColumn col in SeasonsDataGridView.Columns)
            {
                int total1 = 0;

                if (col.Name == "Season") continue;
                foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                {

                    if (row.Index == lastRow) break;
                    try
                    {
                        total1 += Int32.Parse(row.Cells[col.Name].Value.ToString());
                    }
                    catch (System.NullReferenceException ex)
                    {
                        total1 += 0;
                    }
                }
                if (value == total1) found = col.Name;
            }

            return found;
        }

        private string FindRowNameByRowTotal(int value, int lastRow, int index)
        {
            string found = null;
            int total = 0;
            foreach (DataGridViewColumn col in SeasonsDataGridView.Columns)
            {
                int count = 0;
                //int total = 0;
                if (col.Name == "Season") continue;
                foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                {

                    if (row.Index == lastRow) break;
                    try
                    {
                        if (count == index) total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                    }
                    catch (System.NullReferenceException ex)
                    {
                        total += 0;
                    }
                    if (value == total) found = row.Cells["Season"].Value.ToString();
                    if (count == index) break;
                    count++;
                }
            }
            return found;
        }
        private int rowTotal(int end, int lastRow)
        {
            int total2 = 0;
            foreach (DataGridViewColumn col in SeasonsDataGridView.Columns)
            {
                int total = 0;
                int count = 0;
                if (col.Name == "Season") continue;
                foreach (DataGridViewRow row in SeasonsDataGridView.Rows)
                {

                    if (row.Index == lastRow) break;
                    try
                    {
                        if (count == end) total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                    }
                    catch (System.NullReferenceException ex)
                    {
                        total += 0;
                    }
                    if (count == end) break;
                    count++;

                }
                total2 += total;

            }
            return total2;
        }

        private void YearAndSeasonCalcButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                int lastRow = SeasonsDataGridView.Rows.Count - 1;
                int total = 0;
                foreach (DataGridViewColumn col in SeasonsDataGridView.Columns) {
                    foreach (DataGridViewRow row in SeasonsDataGridView.Rows) {

                        if (row.Index == lastRow) break;
                        if (Years.SelectedItem.ToString() != "All Years" && Seasons.SelectedItem.ToString() != "All Seasons")
                        {
                            if (col.Name == Years.SelectedItem.ToString())
                            {
                                if (row.Cells["Season"].Value.ToString() == Seasons.SelectedItem.ToString())
                                {
                                    YearsAndSeasonsCalcTextBox.Text = row.Cells[Years.SelectedItem.ToString()].Value.ToString();
                                    break;
                                }
                            }
                        }
                        else if(Years.SelectedItem.ToString() == "All Years" && Seasons.SelectedItem.ToString() != "All Seasons")
                        {
                            if(Seasons.SelectedItem.ToString() == "Spring")
                            {
                                YearsAndSeasonsCalcTextBox.Text = rowTotal(0, lastRow).ToString();
                                break;
                            }
                            if (Seasons.SelectedItem.ToString() == "Summer")
                            {
                                YearsAndSeasonsCalcTextBox.Text = rowTotal(1, lastRow).ToString();
                                break;
                            }
                            if (Seasons.SelectedItem.ToString() == "Autumn")
                            {
                                YearsAndSeasonsCalcTextBox.Text = rowTotal(2, lastRow).ToString();
                                break;
                            }
                            if (Seasons.SelectedItem.ToString() == "Winter")
                            {
                                YearsAndSeasonsCalcTextBox.Text = rowTotal(3, lastRow).ToString();
                                break;
                            }
                            
                        }
                        else if(Years.SelectedItem.ToString() != "All Years" && Seasons.SelectedItem.ToString() == "All Seasons")
                        {
                            try
                            { 
                                if(col.Name == Years.SelectedItem.ToString()) total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                            }
                            catch (System.NullReferenceException ex)
                            {
                                total += 0;
                            }
                            YearsAndSeasonsCalcTextBox.Text = total.ToString();
                        }
                        else if(Years.SelectedItem.ToString() == "All Years" && Seasons.SelectedItem.ToString() == "All Seasons")
                        {
                            if(col.Name == "Season") continue;
                            try
                            {
                                total += Int32.Parse(row.Cells[col.Name].Value.ToString());
                            }
                            catch (System.NullReferenceException ex)
                            {
                                total += 0;
                            }
                            YearsAndSeasonsCalcTextBox.Text = total.ToString();
                        }
                    }
                }
            }
            catch (System.NullReferenceException ex) { };
        }
        //END//

        //Question 3
        private void Race1Btn_Click(object sender, EventArgs e)
        {
            RunnersTextBox.Text = "";
            foreach (String s in runners.ListRunnersRace1())
            {
                RunnersTextBox.Text += s + ", ";
            } 
        }

        private void Race2Btn_Click(object sender, EventArgs e)
        {
            RunnersTextBox.Text = "";
            foreach (String s in runners.ListRunnersRace2())
            {
                RunnersTextBox.Text += s + ", ";
            }
        }

        private void Race3Btn_Click(object sender, EventArgs e)
        {
            RunnersTextBox.Text = "";
            foreach (String s in runners.ListRunnersRace3())
            {
                RunnersTextBox.Text += s + ", ";
            }
        }

        //Employees
        private void AddEmployeeBtn_Click(object sender, EventArgs e)
        {
            string name = "Empty";
            string type = "Empty";

            if (EmpNameTextBox.Text != null && EmpNameTextBox.Text != "" && EmpTypeTextBox.Text != null && EmpTypeTextBox.Text != "")
            {
                name = EmpNameTextBox.Text;
                type = EmpTypeTextBox.Text;

                int yrsService = 0;
                Int32.TryParse(EmpYrsServiceTextBox.Text, out yrsService);

                double salary = 0;
                Double.TryParse(EmpSalaryTextBox.Text, out salary);

                hseEmployee emp = new hseEmployee(name, type, yrsService, salary);

                MessageBox.Show(hseEmployee.PrintDetails(emp));
            }

        }

        private void CreateDoctorBtn_Click(object sender, EventArgs e)
        {
            hseEmployee doc = new Doctor();

            MessageBox.Show(hseEmployee.PrintDetails(doc));
        }

        private void GenerateRandomEmployeesBtn_Click(object sender, EventArgs e)
        {
            var rand = new Random();

            string[] names = new string[] {
                "James","Bob","John","Paul","Sam","Tony","Arnas",
                "Dylan","James","Denis","Malo","Peter","Tomas",
                "Terry","Eugine","Hank","Jerry","Rick","Morty"
            };
            string[] types = new string[] {
                "Doctor","Employee","Porter"
            };
            int[] yrsServices = new int[] { 
                0,1,2,3,4,5,6,7,8,9,10,12,13,14,15,16,17,18,19,20 
            };
            double[] salaries = new double[] { 
                20000.00,30000.00,40000.00,
                50000.00,60000.00,70000.00,
                80000.00,90000.00,100000.00,
                110000.00,120000.00,130000.00,
                140000.00,150000.00,160000.00,
                170000.00,180000.00,190000.00,
                200000.00
            };

            emps.Clear();
            for(int i = 0; i < 10; i++)
            {
                
                emps.Add(hseEmployee.createEmployee(
                    types[rand.Next(0,2)], 
                    names[rand.Next(0, 19)], types[rand.Next(0, 2)], 
                    yrsServices[rand.Next(0, 19)], 
                    salaries[rand.Next(0, 19)]));
            }
                        
        }
        //END//
    }
}
