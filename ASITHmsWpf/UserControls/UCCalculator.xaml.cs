using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASITHmsWpf.UserControls
{
    /// <summary>
    /// Interaction logic for UCCalculator.xaml
    /// </summary>
    public partial class UcCalculator : UserControl
    {
        public UcCalculator()
        {
            InitializeComponent();
        }
        private void btnOne_Click(object sender, RoutedEventArgs e)
        {

            this.txtResult.Text = this.txtResult.Text + "1";
        }

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "3";
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {

            this.txtResult.Text = this.txtResult.Text + "2";
        }

        private void btnBSpace_Click(object sender, RoutedEventArgs e)
        {
            if (txtResult.Text != "")
                this.txtResult.Text = this.txtResult.Text.Remove(this.txtResult.Text.Length - 1);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = "";
            this.txtbFResult.Text = "";
            this.txtResult.Focus();
        }

        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "4";
        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "5";
        }

        private void btnSix_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "6";
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "+";
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "-";
        }

        private void btnSeven_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "7";
        }

        private void btnEight_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "8";
        }

        private void btnNine_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "9";
        }

        private void btnMultiple_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "*";
        }

        private void btnDivid_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "/";
        }

        private void btnZero_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + "0";
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = this.txtResult.Text + ".";
        }

        private void btnEquel_Click(object sender, RoutedEventArgs e)
        {
            #region javascript calculat

            //Type scriptType = Type.GetTypeFromCLSID(Guid.Parse("0E59F1D5-1FBE-11D0-8FF2-00A0D10038BC"));
            //dynamic obj = Activator.CreateInstance(scriptType, false);
            //obj.Language = "Javascript";
            //string str = null;
            //try
            //{
            //    var res = obj.Eval(this.txtResult.Text);
            //    str = Convert.ToString(res);
            //    //this.txtbFResult.Text = this.txtResult.Text + "=" + str;
            //    this.txtbFResult.Text ="= "+str;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            #endregion

            #region MyRegion
            string[] num = Regex.Split(txtResult.Text, @"\-|\+|\*|\/").Where(s => !String.IsNullOrEmpty(s)).ToArray(); // get Array for numbers
            string[] op = Regex.Split(txtResult.Text, @"\d{1,3}").Where(s => !String.IsNullOrEmpty(s)).ToArray(); // get Array for mathematical operators +,-,/,*
            int numCtr = 0, lastVal = 0; // number counter and last Value accumulator
            string lastOp = ""; // last Operator
            foreach (string n in num)
            {
                numCtr++;
                if (numCtr == 1)
                {
                    lastVal = int.Parse(n); // if first loop lastVal will have the first numeric value
                }
                else
                {
                    if (!String.IsNullOrEmpty(lastOp)) // if last Operator not empty
                    {
                        // Do the mathematical computation and accumulation
                        switch (lastOp)
                        {
                            case "+":
                                lastVal = lastVal + int.Parse(n);
                                break;
                            case "-":
                                lastVal = lastVal - int.Parse(n);
                                break;
                            case "*":
                                lastVal = lastVal * int.Parse(n);
                                break;
                            case "/":
                                lastVal = lastVal / int.Parse(n);
                                break;
                        }
                    }
                }
                int opCtr = 0;
                foreach (string o in op)
                {
                    opCtr++;
                    if (opCtr == numCtr) //will make sure it will get the next operator
                    {
                        lastOp = o;  // get the last operator
                        break;
                    }
                }
                this.txtbFResult.Text = "= " + lastVal.ToString();
            }
            #endregion
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,*/]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtResult.Focus();
        }

        private void txtResult_LostFocus(object sender, RoutedEventArgs e)
        {
            this.txtbFResult.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.txtbFResult.Focus();
        }
    }
}
