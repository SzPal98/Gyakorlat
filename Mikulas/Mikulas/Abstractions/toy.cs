using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas.Abstractions
{
    public abstract class toy: Label
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

        protected abstract void DrawImage(Graphics g);
        

        public void Movetoy()
        {
            Left += 1;
        }
    }
}
