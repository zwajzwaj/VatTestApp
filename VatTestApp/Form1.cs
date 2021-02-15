using Exchanges;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VatTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            cbbTaxRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTaxRate.DataSource = GenerateTaxRates();
            cbbTaxRate.DisplayMember = "Name";
            cbbTaxRate.ValueMember = "Value";
        }

        private List<GuiObjects.ListElement<decimal>> GenerateTaxRates()
        {
            List<GuiObjects.ListElement<decimal>> result = new List<GuiObjects.ListElement<decimal>>();
            result.Add(new GuiObjects.ListElement<decimal>() { Name = "Wybierz...", Value = 0M });
            result.Add(new GuiObjects.ListElement<decimal>() { Name = "8%", Value = 0.08M });
            result.Add(new GuiObjects.ListElement<decimal>() { Name = "23%", Value = 0.23M });
            return result;
        }

        private CalculateVatRequest CreateCalculateVatRequest()
        {
            CalculateVatRequest result = new CalculateVatRequest();
            decimal netto = 0;
            bool isValidNetto = Decimal.TryParse(tbNetto.Text, out netto);
            result.Netto = netto;
            result.TaxRate = ((GuiObjects.ListElement<decimal>)cbbTaxRate.SelectedItem).Value;
            return result;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Logic.Logic logic = new Logic.Logic();
            CalculateVatRequest request = CreateCalculateVatRequest();
            CalculateVatResponse response = logic.HandleCalculateRequest(request) as CalculateVatResponse;
            if (response != null && response.IsValid)
            {
                //zakładając że chcemy zaokrąglenie
                response.Result = Math.Round(response.Result, 2, MidpointRounding.AwayFromZero);
                tbBrutto.Text = String.Format("{0:0.##}", response.Result); 
            }
            else
            {
                // można też gdzieś wrzucić komunikaty walidacyjne
                tbBrutto.Text = String.Empty;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbbTaxRate.SelectedIndex = 0;
            tbNetto.Text = String.Empty;
            tbBrutto.Text = String.Empty;
        }

    }
}
