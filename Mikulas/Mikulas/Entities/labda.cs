using Mikulas.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas.Entities
{
    class labda: toy
    {
        public SolidBrush BallColor { get; private set; }

        public labda (Color color)
        {
            BallColor = new SolidBrush(color);
        }

        protected override void DrawImage(Graphics g)
        {
            var ecset = new SolidBrush(Color.Blue);
            g.FillEllipse(ecset, 0, 0, Width, Height);
        }

        
    }
}
