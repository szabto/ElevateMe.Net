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
namespace ElevatorSaga.GUI
{
    public partial class MainForm : Form
    {
        private readonly World _world;
        private readonly List<Floor> _floors = new List<Floor>();
        private readonly List<Elevator> _elevators = new List<Elevator>();

        private const int floorHeight = 80;
        private const int shaftWidth = 80;

        public MainForm()
        {
            InitializeComponent();
            _world = new World();

            _world.FloorAdded += (object s, FloorEventArgs a) =>
            {
                _floors.Add(a.Floor);

                FloorControl fc = new FloorControl(a.Floor);

                fc.Location = new Point(0, panel1.Height - ((a.Floor.Level +1) * floorHeight));
                fc.Size = new Size(this.Width, floorHeight);
                fc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

                panel1.Controls.Add(fc);
            };


            _world.ElevatorAdded += (object s, ElevatorEventArgs a) =>
            {
                _elevators.Add(a.Elevator);

                ElevatorShaftControl esc = new ElevatorShaftControl(a.Elevator, floorHeight, shaftWidth);
                esc.Location = new Point(250 + ((_elevators.Count-1) * (shaftWidth + 10)), 0);
                esc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                esc.Height = panel1.Height;
                esc.Width = shaftWidth;
                esc.BackColor = Color.Transparent;

                panel1.Controls.Add(esc);

                esc.BringToFront();
            };

            _world.Generate();
            this.Height = (_floors.Count+1) * floorHeight;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
