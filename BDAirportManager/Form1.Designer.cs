namespace BDAirportManager
{
	partial class MainScene
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.airplanesTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.airplanesRefreshButton = new System.Windows.Forms.Button();
			this.airplanesUpdateButton = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.hangarsTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.hangarsUpdateButton = new System.Windows.Forms.Button();
			this.hangarsRefreshButton = new System.Windows.Forms.Button();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.employeeTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.employeesUpdateButton = new System.Windows.Forms.Button();
			this.employeesRefreshButton = new System.Windows.Forms.Button();
			this.dataGridView3 = new System.Windows.Forms.DataGridView();
			this.miscTab = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripConnectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1.SuspendLayout();
			this.airplanesTab.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.hangarsTab.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.employeeTab.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
			this.miscTab.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.airplanesTab);
			this.tabControl1.Controls.Add(this.hangarsTab);
			this.tabControl1.Controls.Add(this.employeeTab);
			this.tabControl1.Controls.Add(this.miscTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Enabled = false;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(823, 398);
			this.tabControl1.TabIndex = 0;
			// 
			// airplanesTab
			// 
			this.airplanesTab.Controls.Add(this.tableLayoutPanel1);
			this.airplanesTab.Location = new System.Drawing.Point(4, 22);
			this.airplanesTab.Name = "airplanesTab";
			this.airplanesTab.Padding = new System.Windows.Forms.Padding(3);
			this.airplanesTab.Size = new System.Drawing.Size(815, 372);
			this.airplanesTab.TabIndex = 0;
			this.airplanesTab.Text = "Airplanes";
			this.airplanesTab.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 366);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.groupBox1);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(609, 3);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(197, 360);
			this.flowLayoutPanel2.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.airplanesRefreshButton);
			this.groupBox1.Controls.Add(this.airplanesUpdateButton);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(87, 90);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Controls";
			// 
			// airplanesRefreshButton
			// 
			this.airplanesRefreshButton.Location = new System.Drawing.Point(6, 19);
			this.airplanesRefreshButton.Name = "airplanesRefreshButton";
			this.airplanesRefreshButton.Size = new System.Drawing.Size(75, 23);
			this.airplanesRefreshButton.TabIndex = 0;
			this.airplanesRefreshButton.Text = "Refresh";
			this.airplanesRefreshButton.UseVisualStyleBackColor = true;
			this.airplanesRefreshButton.Click += new System.EventHandler(this.airplanesRefreshButton_Click);
			// 
			// airplanesUpdateButton
			// 
			this.airplanesUpdateButton.Location = new System.Drawing.Point(6, 48);
			this.airplanesUpdateButton.Name = "airplanesUpdateButton";
			this.airplanesUpdateButton.Size = new System.Drawing.Size(75, 23);
			this.airplanesUpdateButton.TabIndex = 3;
			this.airplanesUpdateButton.Text = "Update";
			this.airplanesUpdateButton.UseVisualStyleBackColor = true;
			this.airplanesUpdateButton.Click += new System.EventHandler(this.airplanesUpdateButton_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(3, 3);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(600, 360);
			this.dataGridView1.TabIndex = 0;
			// 
			// hangarsTab
			// 
			this.hangarsTab.Controls.Add(this.tableLayoutPanel2);
			this.hangarsTab.Location = new System.Drawing.Point(4, 22);
			this.hangarsTab.Name = "hangarsTab";
			this.hangarsTab.Padding = new System.Windows.Forms.Padding(3);
			this.hangarsTab.Size = new System.Drawing.Size(815, 372);
			this.hangarsTab.TabIndex = 1;
			this.hangarsTab.Text = "Hangars";
			this.hangarsTab.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.dataGridView2, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(809, 366);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.groupBox2);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(609, 3);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(197, 360);
			this.flowLayoutPanel3.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.hangarsUpdateButton);
			this.groupBox2.Controls.Add(this.hangarsRefreshButton);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(87, 90);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Controls";
			// 
			// hangarsUpdateButton
			// 
			this.hangarsUpdateButton.Location = new System.Drawing.Point(6, 48);
			this.hangarsUpdateButton.Name = "hangarsUpdateButton";
			this.hangarsUpdateButton.Size = new System.Drawing.Size(75, 23);
			this.hangarsUpdateButton.TabIndex = 2;
			this.hangarsUpdateButton.Text = "Update";
			this.hangarsUpdateButton.UseVisualStyleBackColor = true;
			this.hangarsUpdateButton.Click += new System.EventHandler(this.hangarsUpdateButton_Click);
			// 
			// hangarsRefreshButton
			// 
			this.hangarsRefreshButton.Location = new System.Drawing.Point(6, 19);
			this.hangarsRefreshButton.Name = "hangarsRefreshButton";
			this.hangarsRefreshButton.Size = new System.Drawing.Size(75, 23);
			this.hangarsRefreshButton.TabIndex = 1;
			this.hangarsRefreshButton.Text = "Refresh";
			this.hangarsRefreshButton.UseVisualStyleBackColor = true;
			this.hangarsRefreshButton.Click += new System.EventHandler(this.hangarsRefreshButton_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView2.Location = new System.Drawing.Point(3, 3);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(600, 360);
			this.dataGridView2.TabIndex = 0;
			// 
			// employeeTab
			// 
			this.employeeTab.Controls.Add(this.tableLayoutPanel3);
			this.employeeTab.Location = new System.Drawing.Point(4, 22);
			this.employeeTab.Name = "employeeTab";
			this.employeeTab.Padding = new System.Windows.Forms.Padding(3);
			this.employeeTab.Size = new System.Drawing.Size(815, 372);
			this.employeeTab.TabIndex = 2;
			this.employeeTab.Text = "Employees";
			this.employeeTab.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel4, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.dataGridView3, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(809, 366);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.Controls.Add(this.groupBox3);
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel4.Location = new System.Drawing.Point(609, 3);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(197, 360);
			this.flowLayoutPanel4.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.AutoSize = true;
			this.groupBox3.Controls.Add(this.employeesUpdateButton);
			this.groupBox3.Controls.Add(this.employeesRefreshButton);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(87, 90);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Controls";
			// 
			// employeesUpdateButton
			// 
			this.employeesUpdateButton.Location = new System.Drawing.Point(6, 48);
			this.employeesUpdateButton.Name = "employeesUpdateButton";
			this.employeesUpdateButton.Size = new System.Drawing.Size(75, 23);
			this.employeesUpdateButton.TabIndex = 2;
			this.employeesUpdateButton.Text = "Update";
			this.employeesUpdateButton.UseVisualStyleBackColor = true;
			this.employeesUpdateButton.Click += new System.EventHandler(this.employeesUpdateButton_Click);
			// 
			// employeesRefreshButton
			// 
			this.employeesRefreshButton.Location = new System.Drawing.Point(6, 19);
			this.employeesRefreshButton.Name = "employeesRefreshButton";
			this.employeesRefreshButton.Size = new System.Drawing.Size(75, 23);
			this.employeesRefreshButton.TabIndex = 1;
			this.employeesRefreshButton.Text = "Refresh";
			this.employeesRefreshButton.UseVisualStyleBackColor = true;
			this.employeesRefreshButton.Click += new System.EventHandler(this.employeesRefreshButton_Click);
			// 
			// dataGridView3
			// 
			this.dataGridView3.AllowUserToAddRows = false;
			this.dataGridView3.AllowUserToDeleteRows = false;
			this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView3.Location = new System.Drawing.Point(3, 3);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.Size = new System.Drawing.Size(600, 360);
			this.dataGridView3.TabIndex = 0;
			// 
			// miscTab
			// 
			this.miscTab.Controls.Add(this.flowLayoutPanel1);
			this.miscTab.Location = new System.Drawing.Point(4, 22);
			this.miscTab.Name = "miscTab";
			this.miscTab.Padding = new System.Windows.Forms.Padding(3);
			this.miscTab.Size = new System.Drawing.Size(815, 372);
			this.miscTab.TabIndex = 3;
			this.miscTab.Text = "Maintenance";
			this.miscTab.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(809, 366);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripConnectionStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 388);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(823, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripConnectionStatusLabel
			// 
			this.toolStripConnectionStatusLabel.ForeColor = System.Drawing.Color.Red;
			this.toolStripConnectionStatusLabel.Name = "toolStripConnectionStatusLabel";
			this.toolStripConnectionStatusLabel.Size = new System.Drawing.Size(79, 17);
			this.toolStripConnectionStatusLabel.Text = "Disconnected";
			// 
			// MainScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(823, 410);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainScene";
			this.Text = "Airport Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScene_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.airplanesTab.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.hangarsTab.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.employeeTab.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
			this.miscTab.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage airplanesTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TabPage hangarsTab;
		private System.Windows.Forms.TabPage employeeTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.DataGridView dataGridView3;
		private System.Windows.Forms.TabPage miscTab;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button airplanesRefreshButton;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.Button hangarsRefreshButton;
		private System.Windows.Forms.Button employeesRefreshButton;
		private System.Windows.Forms.Button airplanesUpdateButton;
		private System.Windows.Forms.Button hangarsUpdateButton;
		private System.Windows.Forms.Button employeesUpdateButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripConnectionStatusLabel;
	}
}

