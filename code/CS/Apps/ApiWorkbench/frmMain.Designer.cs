﻿namespace Org.ApiWorkbench
{
  partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.mnuMain.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1274, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 697);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1274, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Location = new System.Drawing.Point(4, 28);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1270, 100);
      this.pnlTop.TabIndex = 2;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1274, 720);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "API Workbench - v1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
  }
}

