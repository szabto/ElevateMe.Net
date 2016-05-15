namespace ElevatorSaga.GUI
{
    partial class FloorControl
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
        private void InitializeComponent()
        {
            this.lblLevel = new System.Windows.Forms.Label();
            this.chkDown = new System.Windows.Forms.CheckBox();
            this.chkUp = new System.Windows.Forms.CheckBox();
            this.buttonContainer = new System.Windows.Forms.TableLayoutPanel();
            this.buttonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLevel
            // 
            this.lblLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLevel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(0, 1);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(49, 78);
            this.lblLevel.TabIndex = 0;
            this.lblLevel.Text = "0";
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkDown
            // 
            this.chkDown.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDown.AutoSize = true;
            this.chkDown.Location = new System.Drawing.Point(42, 26);
            this.chkDown.Name = "chkDown";
            this.chkDown.Size = new System.Drawing.Size(45, 23);
            this.chkDown.TabIndex = 1;
            this.chkDown.Text = "Down";
            this.chkDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDown.UseVisualStyleBackColor = true;
            this.chkDown.CheckedChanged += new System.EventHandler(this.OnButtonStateChanged);
            // 
            // chkUp
            // 
            this.chkUp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUp.AutoSize = true;
            this.chkUp.Location = new System.Drawing.Point(3, 26);
            this.chkUp.Name = "chkUp";
            this.chkUp.Size = new System.Drawing.Size(32, 23);
            this.chkUp.TabIndex = 1;
            this.chkUp.Text = "UP";
            this.chkUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUp.UseVisualStyleBackColor = true;
            this.chkUp.CheckedChanged += new System.EventHandler(this.OnButtonStateChanged);
            // 
            // buttonContainer
            // 
            this.buttonContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonContainer.ColumnCount = 2;
            this.buttonContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.buttonContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.buttonContainer.Controls.Add(this.chkDown, 1, 1);
            this.buttonContainer.Controls.Add(this.chkUp, 0, 1);
            this.buttonContainer.Location = new System.Drawing.Point(50, 1);
            this.buttonContainer.Name = "buttonContainer";
            this.buttonContainer.RowCount = 3;
            this.buttonContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.buttonContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonContainer.Size = new System.Drawing.Size(90, 75);
            this.buttonContainer.TabIndex = 2;
            // 
            // FloorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonContainer);
            this.Controls.Add(this.lblLevel);
            this.Name = "FloorControl";
            this.Size = new System.Drawing.Size(1089, 79);
            this.buttonContainer.ResumeLayout(false);
            this.buttonContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.CheckBox chkDown;
        private System.Windows.Forms.CheckBox chkUp;
        private System.Windows.Forms.TableLayoutPanel buttonContainer;
    }
}
