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
 * Sharp Auto center	
 * Created By David McNiven
 * Student Number 200330143
 * Created On October 5th 2016
 * 
 * App description
 */
namespace Sharp_Auto_Center
{
    public partial class SharpAutoForm : Form
    {
        public SharpAutoForm()
        {
            InitializeComponent();
        }

        private void SharpAutoForm_Load(object sender, EventArgs e)
        {
            BasePriceTextBox.Focus();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            SubTotalTextBox.Text = (Double.Parse(BasePriceTextBox.Text) + Double.Parse(AdditionalOptionsTextBox.Text)).ToString("C2");
            SalesTaxTextBox.Text = (Double.Parse(SubTotalTextBox.Text) * 0.13).ToString("C2");
            TotalTextBox.Text = (Double.Parse(SubTotalTextBox.Text) + Double.Parse(SalesTaxTextBox.Text)).ToString("C2");
            AmountDueTextBox.Text = (Double.Parse(TotalTextBox.Text) - Double.Parse(TradeinAllowanceTextBox.Text)).ToString("C2");
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            BasePriceTextBox.Clear();
            TradeinAllowanceTextBox.Text = "0.00";
            AdditionalOptionsTextBox.Clear();
            SubTotalTextBox.Clear();
            SalesTaxTextBox.Clear();
            TotalTextBox.Clear();
            AmountDueTextBox.Clear();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
