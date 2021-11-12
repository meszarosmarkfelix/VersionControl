using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futoszalag_8.het.Abstractions;

namespace Futoszalag_8.het.Entities
{
    class Present : Toy
    {

        public SolidBrush PresentColor1 { get; set; }
        public SolidBrush PresentColor2 { get; set; }

        public Present(Color Color1, Color Color2) 
        {
            PresentColor1 = new SolidBrush(Color1);
            PresentColor2 = new SolidBrush(Color2);


        }
        protected override void DrawImage(Graphics g)
        {
            gIn.Fillrectangle(PresentColor1, 0, 0, this.Width, this.Height);
            gIn.Fillrectangle(PresentColor2, 0, (this.Height/5)*2, this.Width, this.Height / 5;
            gIn.Fillrectangle(PresentColor2, 0, (this.Width / 5) * 2, this.Width/5, this.Height;
        }
    }
}
