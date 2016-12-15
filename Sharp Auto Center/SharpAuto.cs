using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Sharp Auto Center	
 * Created By David McNiven
 * Student Number 200330143
 * Created On October 5th 2016
 * 
 * This program does a mock calculation of cost for a new car based on a given trade-in value and selected additional options.
 */
namespace Sharp_Auto_Center
{
    public partial class SharpAutoForm : Form
    {
        private double _finishCost, _addonCost, _basePrice, _tradeIn;

        public SharpAutoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// sets all private variables to 0 and sends focus to the first textbox for user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharpAutoForm_Load(object sender, EventArgs e)
        {
            BasePriceTextBox.Focus();
            _finishCost = 0;
            _addonCost = 0;
            _basePrice = 0;
            _tradeIn = 0;
        }

        /// <summary>
        /// event handler for exiting the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// event handler for calculating the subtotal, sales tax, total, and amount due
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            double subTotal = _basePrice + _finishCost + _addonCost;
            double total = subTotal + calcSalesTax(subTotal);
            SubTotalTextBox.Text = subTotal.ToString("C2");
            TotalTextBox.Text = total.ToString("C2");
            AmountDueTextBox.Text = (total - _tradeIn).ToString("C2");
        }

        /// <summary>
        /// function that calculates the sales tax on the current subtotal, displays it in the sales tax textbox and returns the value
        /// </summary>
        /// <param name="subTotal"></param>
        /// <returns></returns>
        private double calcSalesTax(double subTotal)
        {
            double tax = subTotal * 0.13;
            SalesTaxTextBox.Text = tax.ToString("C2");
            return tax;
        }

        /// <summary>
        /// event handler for clearing the form back to defaults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            BasePriceTextBox.Text = "0.00";
            TradeinAllowanceTextBox.Text = "0.00";
            AdditionalOptionsTextBox.Text = "0.00";
            SubTotalTextBox.Text = "0.00";
            SalesTaxTextBox.Text = "0.00";
            TotalTextBox.Text = "0.00";
            AmountDueTextBox.Text = "0.00";
            StandardFinishRadioButton.Checked = true;
            _finishCost = 0;
            StereoSystemCheckBox.Checked = false;
            LeatherInteriorCheckBox.Checked = false;
            ComputerNavigationCheckBox.Checked = false;
        }

        /// <summary>
        /// event handler for bringing up a fontdialog whenever the font or colour menu items are clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PriceFontDialog.Font = BasePriceTextBox.Font;
            PriceFontDialog.Color = BasePriceTextBox.ForeColor;

            if (PriceFontDialog.ShowDialog().Equals(DialogResult.OK))
            {
                BasePriceTextBox.Font = PriceFontDialog.Font;
                BasePriceTextBox.ForeColor = PriceFontDialog.Color;
                AmountDueTextBox.Font = PriceFontDialog.Font;
                AmountDueTextBox.ForeColor = PriceFontDialog.Color;
            }
        }

        /// <summary>
        /// event handler for correctly adjusting the addon cost any time a checkbox is checked/unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            String senderName = ((CheckBox)sender).Text;
            Boolean senderState = ((CheckBox)sender).Checked;
            if (senderName.Equals("Stereo System") && senderState)
            {
                _addonCost += 80;
            } else if (senderName.Equals("Stereo System"))
            {
                _addonCost -= 80;
            } else if (senderName.Equals("Leather Interior") && senderState)
            {
                _addonCost += 200;
            } else if (senderName.Equals("Leather Interior"))
            {
                _addonCost -= 200;
            } else if (senderState)
            {
                _addonCost += 320;
            } else
            {
                _addonCost -= 320;
            }
            AdditionalOptionsTextBox.Text = (_finishCost + _addonCost).ToString("C2");
        }

        /// <summary>
        /// event handler for showing a short message about the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sharp Auto Center" + Environment.NewLine +
                "Created by David McNiven" + Environment.NewLine +
                "This program does a mock calculation of cost for a new car based on a given trade-in value and selected additional options.");
        }

        /// <summary>
        /// event handler to validate the two user inputs whenever focus leaves each textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateTextBox_TextChanged(object sender, EventArgs e)
        {
            double result = checkInput(((TextBox)sender).Text);
            if (result >= 0 && ((TextBox)sender).Name.Equals("BasePriceTextBox"))
            {
                _basePrice = result;
            } else if (result >= 0)
            {
                _tradeIn = result;
            } else
            {
                MessageBox.Show("Must be a valid Dollar amount");
                ((TextBox)sender).Focus();
            }
        }

        /// <summary>
        /// function for checking if a given string can be converted to a double value, and returns it if so
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double checkInput (String input)
        {
            double result;
            if (Double.TryParse(input, out result))
            {
                return result;
            }
            return -1;
        }

        /// <summary>
        /// event handler for changing between exterior finishes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, EventArgs e)
        {
            String senderName = ((RadioButton)sender).Text;
            if (senderName.Equals("Standard"))
            {
                _finishCost = 0;
            } else if (senderName.Equals("Pearlized"))
            {
                _finishCost = 200;
            } else
            {
                _finishCost = 350;
            }
            AdditionalOptionsTextBox.Text = (_finishCost + _addonCost).ToString("C2");
        }
    }
}
