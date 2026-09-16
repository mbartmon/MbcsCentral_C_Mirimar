namespace MbcsCentral
{
    partial class frmProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstProgress = new System.Windows.Forms.ListBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstProgress
            // 
            this.lstProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProgress.FormattingEnabled = true;
            this.lstProgress.Location = new System.Drawing.Point(0, -16);
            this.lstProgress.Name = "lstProgress";
            this.lstProgress.Size = new System.Drawing.Size(492, 394);
            this.lstProgress.TabIndex = 0;
            // 
            // cmdExit
            // 
            this.cmdExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdExit.Location = new System.Drawing.Point(0, 401);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(492, 21);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "EXIT";
            this.cmdExit.UseVisualStyleBackColor = true;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdPrint.Location = new System.Drawing.Point(0, 380);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(492, 21);
            this.cmdPrint.TabIndex = 2;
            this.cmdPrint.Text = "PRINT";
            this.cmdPrint.UseVisualStyleBackColor = true;
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 422);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.lstProgress);
            this.Name = "frmProgress";
            this.Text = "Data Conversion Progress";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstProgress;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdPrint;

    }
}