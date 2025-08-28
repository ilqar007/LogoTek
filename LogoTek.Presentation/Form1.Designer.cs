namespace LogoTek.Presentation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBoxServerIpAddress = new TextBox();
            txtBoxServerPort = new TextBox();
            btnStartServer = new Button();
            SuspendLayout();
            // 
            // txtBoxServerIpAddress
            // 
            txtBoxServerIpAddress.Location = new Point(181, 14);
            txtBoxServerIpAddress.Name = "txtBoxServerIpAddress";
            txtBoxServerIpAddress.Size = new Size(125, 27);
            txtBoxServerIpAddress.TabIndex = 0;
            txtBoxServerIpAddress.Text = "127.0.0.1";
            // 
            // txtBoxServerPort
            // 
            txtBoxServerPort.Location = new Point(376, 14);
            txtBoxServerPort.Name = "txtBoxServerPort";
            txtBoxServerPort.Size = new Size(131, 27);
            txtBoxServerPort.TabIndex = 1;
            txtBoxServerPort.Text = "8083";
            // 
            // btnStartServer
            // 
            btnStartServer.Location = new Point(72, 12);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(94, 29);
            btnStartServer.TabIndex = 2;
            btnStartServer.Text = "Start Server";
            btnStartServer.UseVisualStyleBackColor = true;
            btnStartServer.Click += async(sender,e) => await btnStartServer_Click(sender,e);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStartServer);
            Controls.Add(txtBoxServerPort);
            Controls.Add(txtBoxServerIpAddress);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxServerIpAddress;
        private TextBox txtBoxServerPort;
        private Button btnStartServer;
    }
}
