using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAR.Entities;

namespace VAR
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            dataGridView2.DataSource = Portfolio;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            PortfolioItem p = new PortfolioItem();
            p.Index = "OTP";
            p.Value = 10;
            Portfolio.Add(p);
        }
    }
}
