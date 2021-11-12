using Mikulas.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikulas.Entities
{
    public SolidBrush Boxcolor { get; private set; }
    public SolidBrush Ribboncolor { get; private set; }

    public present(Color Boxcolor, Color Ribboncolor)
    {
        Boxcolor = new SolidBrush(Boxcolor);
        Ribboncolor = new SolidBrush(Ribboncolor);
    }
        protected override void DrawImage(Graphics g)
        {
            throw new NotImplementedException();
        }
    
}
