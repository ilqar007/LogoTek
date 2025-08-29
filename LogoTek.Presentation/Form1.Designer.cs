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
            listBoxTelegrams = new ListBox();
            txtBoxDbConnectionString = new TextBox();
            label1 = new Label();
            btnListTelegrams = new Button();
            btnCreateSqlTable = new Button();
            txtBoxPosX = new TextBox();
            txtBoxPosY = new TextBox();
            txtBoxPosZ = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtBoxServerIpAddress
            // 
            txtBoxServerIpAddress.Location = new Point(168, 122);
            txtBoxServerIpAddress.Name = "txtBoxServerIpAddress";
            txtBoxServerIpAddress.Size = new Size(125, 27);
            txtBoxServerIpAddress.TabIndex = 0;
            txtBoxServerIpAddress.Text = "127.0.0.1";
            // 
            // txtBoxServerPort
            // 
            txtBoxServerPort.Location = new Point(335, 124);
            txtBoxServerPort.Name = "txtBoxServerPort";
            txtBoxServerPort.Size = new Size(131, 27);
            txtBoxServerPort.TabIndex = 1;
            txtBoxServerPort.Text = "8083";
            // 
            // btnStartServer
            // 
            btnStartServer.Location = new Point(42, 122);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(94, 29);
            btnStartServer.TabIndex = 2;
            btnStartServer.Text = "Start Server";
            btnStartServer.UseVisualStyleBackColor = true;
            // 
            // listBoxTelegrams
            // 
            listBoxTelegrams.FormattingEnabled = true;
            listBoxTelegrams.Location = new Point(240, 221);
            listBoxTelegrams.Name = "listBoxTelegrams";
            listBoxTelegrams.Size = new Size(459, 104);
            listBoxTelegrams.TabIndex = 3;
            listBoxTelegrams.Click += listBoxTelegrams_Click;
            // 
            // txtBoxDbConnectionString
            // 
            txtBoxDbConnectionString.Location = new Point(28, 32);
            txtBoxDbConnectionString.Name = "txtBoxDbConnectionString";
            txtBoxDbConnectionString.Size = new Size(760, 27);
            txtBoxDbConnectionString.TabIndex = 4;
            txtBoxDbConnectionString.Text = "Server=DESKTOP-MP1J8TM\\SQLEXPRESS01;Database=LogoTek;User Id=sa;Password=sa\r\n;TrustServerCertificate=True;";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 9);
            label1.Name = "label1";
            label1.Size = new Size(108, 20);
            label1.TabIndex = 5;
            label1.Text = "DB Connection";
            // 
            // btnListTelegrams
            // 
            btnListTelegrams.Location = new Point(28, 221);
            btnListTelegrams.Name = "btnListTelegrams";
            btnListTelegrams.Size = new Size(162, 37);
            btnListTelegrams.TabIndex = 6;
            btnListTelegrams.Text = "List Telegrams";
            btnListTelegrams.UseVisualStyleBackColor = true;
            // 
            // btnCreateSqlTable
            // 
            btnCreateSqlTable.Location = new Point(28, 65);
            btnCreateSqlTable.Name = "btnCreateSqlTable";
            btnCreateSqlTable.Size = new Size(189, 32);
            btnCreateSqlTable.TabIndex = 7;
            btnCreateSqlTable.Text = "Create Sql Table";
            btnCreateSqlTable.UseVisualStyleBackColor = true;
            // 
            // txtBoxPosX
            // 
            txtBoxPosX.Location = new Point(240, 350);
            txtBoxPosX.Name = "txtBoxPosX";
            txtBoxPosX.Size = new Size(125, 27);
            txtBoxPosX.TabIndex = 8;
            // 
            // txtBoxPosY
            // 
            txtBoxPosY.Location = new Point(400, 350);
            txtBoxPosY.Name = "txtBoxPosY";
            txtBoxPosY.Size = new Size(125, 27);
            txtBoxPosY.TabIndex = 9;
            // 
            // txtBoxPosZ
            // 
            txtBoxPosZ.Location = new Point(547, 350);
            txtBoxPosZ.Name = "txtBoxPosZ";
            txtBoxPosZ.Size = new Size(125, 27);
            txtBoxPosZ.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(240, 328);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 11;
            label2.Text = "PosX";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(400, 328);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 12;
            label3.Text = "PosY";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(547, 327);
            label4.Name = "label4";
            label4.Size = new Size(40, 20);
            label4.TabIndex = 13;
            label4.Text = "PosZ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtBoxPosZ);
            Controls.Add(txtBoxPosY);
            Controls.Add(txtBoxPosX);
            Controls.Add(btnCreateSqlTable);
            Controls.Add(btnListTelegrams);
            Controls.Add(label1);
            Controls.Add(txtBoxDbConnectionString);
            Controls.Add(listBoxTelegrams);
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
        private ListBox listBoxTelegrams;
        private TextBox txtBoxDbConnectionString;
        private Label label1;
        private Button btnListTelegrams;
        private Button btnCreateSqlTable;
        private TextBox txtBoxPosX;
        private TextBox txtBoxPosY;
        private TextBox txtBoxPosZ;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
