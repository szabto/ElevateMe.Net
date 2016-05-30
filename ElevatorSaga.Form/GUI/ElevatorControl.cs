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
    public partial class ElevatorControl : UserControl
    {
        private readonly Elevator Elevator;

        public ElevatorControl(Elevator e)
        {
            InitializeComponent();

            Elevator = e;

            e.IndicatorChanged += OnIndicatorChanged;
            e.DoorStateChanged += OnDoorStateChanged;
            BackColor = Color.DarkOliveGreen;
        }

        private void OnDoorStateChanged(object sender, DoorStateEventArgs eargs)
        {
            this.Invoke(new Action(() =>
            {
                this.BackColor = eargs.NewState == DoorState.Closed ? Color.IndianRed : (eargs.NewState == DoorState.Opened ? Color.DarkOliveGreen : Color.Yellow);
            }));
        }

        private void OnIndicatorChanged(object sender, IndicatorEventArgs eargs)
        {
            CheckBox cb = eargs.Direction == Direction.Up ? chkUp : chkDown;

            cb.Checked = eargs.Value;
        }
    }
}
