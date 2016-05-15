using ElevatorSaga.Core.Classes;

namespace ElevatorSaga.GUI
{
    partial class ElevatorShaftControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(Elevator e)
        {
            this.SuspendLayout();
            // 
            // ElevatorShaftControl
            // 
            this.Name = "ElevatorShaftControl";
            this.Size = new System.Drawing.Size(414, 729);
            this.ResumeLayout(false);

        }

        #endregion

        private ElevatorControl elevatorMain;
    }
}
