using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElevatorSaga.Core.Classes;

namespace ElevatorSaga.GUI
{
    public partial class ElevatorShaftControl : UserControl
    {
        private readonly Elevator Elevator;



        public ElevatorShaftControl(Elevator e, int floorHeight, int width)
        {
            InitializeComponent(e);

            elevatorMain = new ElevatorControl(e);


            elevatorMain.Width = width;
            elevatorMain.Height = floorHeight;
            elevatorMain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            elevatorMain.Location = new Point(0, Height - floorHeight);

            Controls.Add(elevatorMain);

            BackColor = Color.Transparent;

            Elevator = e;
        }
    }
}
