using MNB.MNBServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MNB.Entities;
using System.Windows.Forms.DataVisualization.Charting;

namespace MNB
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            
            string xmlstring = consume();
            loadXml(xmlstring);
            dataGridView1.DataSource = Rates;
            Charting();
        }

        string consume() {
            MNBArfolyamServiceSoap mnbservice = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = comboBox1.SelectedItem.ToString();//"EUR";
            request.startDate = dateTimePicker1.Value.ToString("yyyy-mm-dd");// "2020-01-01";
            request.endDate = dateTimePicker2.Value.ToString("yyyy-mm-dd");//"2020-06-30";
            var response = mnbservice.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;



        }

        private void loadXml(string input)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(input);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData r = new RateData();
                r.Date = DateTime.Parse(item.GetAttribute("date"));
                XmlElement child = (XmlElement)item.FirstChild;
                r.Currency = child.GetAttribute("curr");
                r.value = decimal.Parse(child.InnerText);
                int unit = int.Parse(child.GetAttribute("unit"));
                if (unit !=0)
                
                    r.value = r.value / unit;

                Rates.Add(r);
            }
        }

        private void Charting()
        {
            chart1.DataSource = Rates;
            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
}
