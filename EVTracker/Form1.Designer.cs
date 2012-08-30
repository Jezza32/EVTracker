namespace EVTracker
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.cmbRoute = new System.Windows.Forms.ComboBox();
			this.lblRoute = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbGame = new System.Windows.Forms.ComboBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.addPokemonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deletePokemonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(984, 668);
			this.splitContainer1.SplitterDistance = 342;
			this.splitContainer1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(984, 342);
			this.tabControl1.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.cmbRoute);
			this.splitContainer3.Panel1.Controls.Add(this.lblRoute);
			this.splitContainer3.Panel1.Controls.Add(this.label8);
			this.splitContainer3.Panel1.Controls.Add(this.cmbGame);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.AutoScroll = true;
			this.splitContainer3.Size = new System.Drawing.Size(984, 322);
			this.splitContainer3.SplitterDistance = 234;
			this.splitContainer3.TabIndex = 0;
			// 
			// cmbRoute
			// 
			this.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRoute.FormattingEnabled = true;
			this.cmbRoute.Location = new System.Drawing.Point(84, 51);
			this.cmbRoute.Name = "cmbRoute";
			this.cmbRoute.Size = new System.Drawing.Size(138, 21);
			this.cmbRoute.TabIndex = 3;
			// 
			// lblRoute
			// 
			this.lblRoute.AutoSize = true;
			this.lblRoute.Location = new System.Drawing.Point(42, 59);
			this.lblRoute.Name = "lblRoute";
			this.lblRoute.Size = new System.Drawing.Size(36, 13);
			this.lblRoute.TabIndex = 2;
			this.lblRoute.Text = "Route";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(43, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 13);
			this.label8.TabIndex = 1;
			this.label8.Text = "Game";
			// 
			// cmbGame
			// 
			this.cmbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGame.FormattingEnabled = true;
			this.cmbGame.Location = new System.Drawing.Point(84, 19);
			this.cmbGame.Name = "cmbGame";
			this.cmbGame.Size = new System.Drawing.Size(138, 21);
			this.cmbGame.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPokemonToolStripMenuItem,
            this.deletePokemonToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(984, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// addPokemonToolStripMenuItem
			// 
			this.addPokemonToolStripMenuItem.Name = "addPokemonToolStripMenuItem";
			this.addPokemonToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
			this.addPokemonToolStripMenuItem.Text = "Add Pokemon";
			this.addPokemonToolStripMenuItem.Click += new System.EventHandler(this.addPokemonToolStripMenuItem_Click);
			// 
			// deletePokemonToolStripMenuItem
			// 
			this.deletePokemonToolStripMenuItem.Name = "deletePokemonToolStripMenuItem";
			this.deletePokemonToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
			this.deletePokemonToolStripMenuItem.Text = "Delete Pokemon";
			this.deletePokemonToolStripMenuItem.Click += new System.EventHandler(this.deletePokemonToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.loadToolStripMenuItem.Text = "Load";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 692);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "EV Tracker";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ComboBox cmbRoute;
		private System.Windows.Forms.Label lblRoute;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cmbGame;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addPokemonToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deletePokemonToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
	}
}

