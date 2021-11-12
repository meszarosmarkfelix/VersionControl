using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futoszalag_8.het.Abstractions;

namespace Futoszalag_8.het.Entities
{
    public class BallFactory: IToyFactory
    {
        public Toy CreateNew() 
        {
            return new Ball();

        }

        public Color BallColor { get; set; }

        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }


    }
}
