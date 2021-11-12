using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas.Abstractions
{
    class toy: Label
    {
        public toy()
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Paint += toy_Paint;
        }

        private void toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected void DrawImage(Graphics g)
        {
            var ecset = new SolidBrush(Color.Blue);
            g.FillEllipse(ecset, 0, 0, Width, Height);
        }

        public void Movetoy()
        {
            Left += 1;
        }
    }
}
