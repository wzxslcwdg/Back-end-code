using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculate
{

    public partial class Form1 : Form
    {
        double number1 = 0, number2 = 0, number3 = 0;
        int inputnumber;

        enum Operator { none, plus, minus, multiply, division, remainder }
        Operator mode = Operator.none;
        bool ismode = false;
        bool isplus = false;
        bool isminus = false;
        bool ismult = false;
        bool isdivi = false;
        bool isremainder = false;


        public Form1()
        {
            InitializeComponent();
        }



        private void button16_Click(object sender, EventArgs e)
        {
            number1 = 0;
            number2 = 0;
            number3 = 0;
            isminus = false;
            ismult = false;
            isdivi = false;
            isplus = false;
            ismode = false;
            isremainder = false;
            labelout.Text = Convert.ToString(number1);
            labelout.Text = Convert.ToString(number2);
            labelout.Text = Convert.ToString(number3);

        }
        private void plus_Click(object sender, EventArgs e)
        {

            mode = Operator.plus;

            labelmode.Text = "+";
            labelout.Text = Convert.ToString(number2);
            labelbefore.Text = Convert.ToString(number1);
            ismode = true;
            isplus = true;

        }
        private void minus_Click(object sender, EventArgs e)
        {
            mode = Operator.minus;

            labelmode.Text = "-";
            labelout.Text = Convert.ToString(number2);
            labelbefore.Text = Convert.ToString(number1);
            ismode = true;
            isminus = true;
        }
        private void multiply_Click(object sender, EventArgs e)
        {
            mode = Operator.multiply;

            labelmode.Text = "*";
            labelout.Text = Convert.ToString(number2);
            labelbefore.Text = Convert.ToString(number1);
            ismode = true;
            ismult = true;
        }
        private void division_Click(object sender, EventArgs e)
        {
            mode = Operator.division;

            labelmode.Text = "/";
            labelout.Text = Convert.ToString(number2);
            labelbefore.Text = Convert.ToString(number1);
            ismode = true;
            isdivi = true;
        }
        private void remainder_Click(object sender, EventArgs e)
        {
            mode = Operator.remainder;

            labelmode.Text = "%";
            labelout.Text = Convert.ToString(number2);
            labelbefore.Text = Convert.ToString(number1);
            ismode = true;
            isremainder = true;
        }



        private void button10_Click(object sender, EventArgs e)
        {
            inputnumber = 0;
            Calculate(inputnumber);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            inputnumber = 9;
            Calculate(inputnumber);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            inputnumber = 8;
            Calculate(inputnumber);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            inputnumber = 7;
            Calculate(inputnumber);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            inputnumber = 6;
            Calculate(inputnumber);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            inputnumber = 5;
            Calculate(inputnumber);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inputnumber = 4;
            Calculate(inputnumber);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputnumber = 3;
            Calculate(inputnumber);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputnumber = 2;
            Calculate(inputnumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputnumber = 1;
            Calculate(inputnumber);
        }
        public void Calculate(int ck)
        {
            if (ismode == false)
            {
                number1 = number1 * 10 + ck;
                labelout.Text = Convert.ToString(number1);
            }
            else
            {
                number2 = number2 * 10 + ck;
                labelout.Text = Convert.ToString(number2);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            labelout.Text = Convert.ToString(number1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string operatorSymbol = "";
            double result = 0.0;

            if (isplus)
            {
                operatorSymbol = "+";
                result = number1 + number2;
            }
            else if (isminus)
            {
                operatorSymbol = "-";
                result = number1 - number2;
            }
            else if (ismult)
            {
                operatorSymbol = "*";
                result = number1 * number2;
            }
            else if (isdivi && number2 != 0)
            {
                operatorSymbol = "/";
                result = number1 / number2;
            }
            else if (isdivi && number2 == 0)
            {
                labelbefore.Text = "";
                labelmode.Text = "";
                labelout.Text = "error";
                operatorSymbol = "/";
            }
            else if (isremainder)
            {
                operatorSymbol = "%";
                result = number1 % number2;
            }

            string calculationString = $"{number1} {operatorSymbol} {number2}";
            labelbefore.Text = "";
            labelmode.Text = "";
            labelout.Text = result.ToString();

            InsertCalculationIntoDatabase(calculationString, result);

            number1 = result;
            number2 = 0;
            isplus = false;
            isminus = false;
            ismult = false;
            isdivi = false;
            ismode = false;
        }


        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void connect_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;user=root;password=Kk20021125k*;database=cal;";
            MySqlConnection con = new MySqlConnection(connStr);
            try
            {
                con.Open();
                string history = "";
                string query = "SELECT String, Result FROM cal ORDER BY ID DESC LIMIT 5"; // ÐÞ¸ÄÁÐÃû
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string calculationString = reader.GetString("String");
                            string result = reader.GetString("Result");
                            history = history + calculationString + " = " + result + "\r\n";
                        }
                    }
                }
                MessageBox.Show(history, "History");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertCalculationIntoDatabase(string String, double result)
        {
            string connStr = "server=localhost;port=3306;user=root;password=Kk20021125k*;database=cal;";
            MySqlConnection con = new MySqlConnection(connStr);

            try
            {
                con.Open();
                string insertQuery = "INSERT INTO cal (String, Result) VALUES (@String, @Result)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, con))
                {
                    insertCommand.Parameters.AddWithValue("@String", String);
                    insertCommand.Parameters.AddWithValue("@Result", result);
                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Calculation inserted successfully.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;user=root;password=Kk20021125k*;database=cal;";
            MySqlConnection con = new MySqlConnection(connStr);

            try
            {
                con.Open();
                string clearQuery = "DELETE FROM cal";
                using (MySqlCommand clearCommand = new MySqlCommand(clearQuery, con))
                {
                    clearCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Database cleared successfully.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                con.Close();
            }
        }
    }
}