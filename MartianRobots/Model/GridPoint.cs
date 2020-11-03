using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GridPoint
    {

        #region Properties

        public int X_Coordinate { get; set; }

        public int Y_Coordinate { get; set; }

        #endregion

        #region Constructor

        public GridPoint(int xpoint, int ypoint)
        {
            this.X_Coordinate = xpoint;
            this.Y_Coordinate = ypoint;
        }

        public GridPoint Clone()
        {
            return (GridPoint)this.MemberwiseClone();
        }

        #endregion

    }
}
