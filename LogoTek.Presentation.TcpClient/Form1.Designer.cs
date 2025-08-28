namespace LogoTek.Presentation.TcpClient
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
            label1 = new Label();
            label2 = new Label();
            txtBoxServerHostIp = new TextBox();
            txtBoxServerPort = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 24);
            label1.Name = "label1";
            label1.Size = new Size(102, 20);
            label1.TabIndex = 0;
            label1.Text = "Server Host Ip";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 65);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 1;
            label2.Text = "Servrt Port";
            // 
            // txtBoxServerHostIp
            // 
            txtBoxServerHostIp.Location = new Point(249, 21);
            txtBoxServerHostIp.Name = "txtBoxServerHostIp";
            txtBoxServerHostIp.Size = new Size(125, 27);
            txtBoxServerHostIp.TabIndex = 2;
            txtBoxServerHostIp.Text = "127.0.0.1";
            // 
            // txtBoxServerPort
            // 
            txtBoxServerPort.Location = new Point(249, 62);
            txtBoxServerPort.Name = "txtBoxServerPort";
            txtBoxServerPort.Size = new Size(125, 27);
            txtBoxServerPort.TabIndex = 3;
            txtBoxServerPort.Text = "8083";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(86, 138);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(143, 29);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += async (sender,e)=> await btnConnect_Click(sender,e);
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(86, 193);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(143, 29);
            btnDisconnect.TabIndex = 5;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Controls.Add(txtBoxServerPort);
            Controls.Add(txtBoxServerHostIp);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtBoxServerHostIp;
        private TextBox txtBoxServerPort;
        private Button btnConnect;
        private Button btnDisconnect;
    }
}
