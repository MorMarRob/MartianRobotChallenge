using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRobotMovementService
    {

        void MoveRobot(Grid grid, Robot robot);

    }
}
