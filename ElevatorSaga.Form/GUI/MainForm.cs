using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElevatorSaga.Core.Classes;
using ElevatorSaga.Core.Extensions;
using ElevatorSaga.Core.Interfaces;

namespace ElevatorSaga.GUI
{
    public partial class MainForm : Form
    {
        private readonly World _world;
        private readonly List<Floor> _floors = new List<Floor>();
        private readonly List<Elevator> _elevators = new List<Elevator>();

        private const int floorHeight = 80;
        private const int shaftWidth = 80;

        private Timer timer = new Timer();

        private List<ElevatorShaftControl> elevatorShafts = new List<ElevatorShaftControl>();
        private List<FloorControl> floorControls = new List<FloorControl>();

        public MainForm()
        {
            InitializeComponent();
            _world = new World();

            timer.Tick += Timer_Tick;
            timer.Interval = 1000 / 20;
            timer.Start();

            _world.FloorAdded += (object s, FloorAddedEventArgs a) =>
            {
                _floors.Add(a.Floor);

                FloorControl fc = new FloorControl(a.Floor);

                fc.Location = new Point(0, panel1.Height - ((a.Floor.Level +1) * floorHeight));
                fc.Size = new Size(this.Width, floorHeight);
                fc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

                floorControls.Add(fc);

                panel1.Controls.Add(fc);
            };


            _world.ElevatorAdded += (object s, ElevatorAddedEventArgs a) =>
            {
                _elevators.Add(a.Elevator);

                ElevatorShaftControl esc = new ElevatorShaftControl(a.Elevator, floorHeight, shaftWidth);
                esc.Location = new Point(250 + ((_elevators.Count-1) * (shaftWidth + 10)), 0);
                esc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                esc.Height = panel1.Height;
                esc.Width = shaftWidth;
                esc.BackColor = Color.Transparent;

                panel1.Controls.Add(esc);

                elevatorShafts.Add(esc);

                esc.BringToFront();
            };

            _world.Generate();
            this.Height = (_floors.Count+1) * floorHeight;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elevatorShafts.ForEach(x => x.Update());
            floorControls.ForEach(x => x.Update());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string assemblyName = "ElevatorSaga.CustomDll.CustomWorld, ElevatorSaga.CustomDll";
            IWorld w = (IWorld)Activator.CreateInstance(assemblyName.LoadType());

            w.WorldGenerated(_floors, _elevators);
        }
    }
}
