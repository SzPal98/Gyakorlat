using Mikulas.Abstractions;
using Mikulas.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas
{
    public partial class Form1 : Form
    {
        List<toy> _toys = new List<toy>();

        private IToyFactory _factory;
        private Toy _nextToy;



        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
            toy toyy = (toy)Factory.CreateNew();
            _toys.Add(toyy);
            toyy.Left = -toyy.Width;
            panel1.Controls.Add(toyy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _toys)
            {
                ball.Movetoy();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                panel1.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }

        private void btncar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void btnball_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }
        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label2.Top + label2.Height + 20;
            _nextToy.Left = label2.Left;
            Controls.Add(_nextToy);
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }
    }
}
