using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Grid
    {
        #region Properties

        public GridPoint GridOrigen { get; set; }

        public GridPoint GridLimit { get; set; }
       
        public List<GridPoint> ScentPoints { get; set; }

        #endregion


        #region Constructor

        public Grid()
        {
            GridOrigen = new GridPoint(0, 0);
            ScentPoints = new List<GridPoint>();
        }
        #endregion
    }
}
