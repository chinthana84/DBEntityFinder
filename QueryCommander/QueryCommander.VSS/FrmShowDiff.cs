// *******************************************************************************
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// *******************************************************************************
// ***************************************************************************************************************
// This code is copied from CodeProject.com
// A Generic - Reusable Diff Algorithm in C#.
// By aprenot 
// http://www.codeproject.com/csharp/C__Diff_Algorithm.asp
// ****************************************************************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DifferenceEngine;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class FrmShowDiff : Form
	{	
		// Methods
		public FrmShowDiff(DiffList_TextFile source, DiffList_TextFile destination, ArrayList DiffLines, double seconds)
		{
			this.components = null;
			this.InitializeComponent();
			//this.Text = string.Format("Results: {0} secs.", seconds.ToString("#0.00"));
			int num1 = 1;
			foreach (DiffResultSpan span1 in DiffLines)
			{
				ListViewItem item1;
				int num2;
				switch (span1.Status)
				{
					case DiffResultSpanStatus.NoChange:
					{
						num2 = 0;
						goto Label_01DD;
					}
					case DiffResultSpanStatus.Replace:
					{
						num2 = 0;
						goto Label_035B;
					}
					case DiffResultSpanStatus.DeleteSource:
					{
						num2 = 0;
						goto Label_0114;
					}
					case DiffResultSpanStatus.AddDestination:
					{
						num2 = 0;
						goto Label_0292;
					}
					default:
					{
						continue;
					}
				}
			Label_0078:
				item1 = new ListViewItem(num1.ToString("00000"));
				ListViewItem item2 = new ListViewItem(num1.ToString("00000"));
				item1.BackColor = Color.Red;
				item1.SubItems.Add(((TextLine) source.GetByIndex(span1.SourceIndex + num2)).Line);
				item2.BackColor = Color.LightGray;
				item2.SubItems.Add("");
				this.lvSource.Items.Add(item1);
				this.lvDestination.Items.Add(item2);
				num1++;
				num2++;
			Label_0114:
				if (num2 < span1.Length)
				{
					goto Label_0078;
				}
				continue;
			Label_012D:
				item1 = new ListViewItem(num1.ToString("00000"));
				item2 = new ListViewItem(num1.ToString("00000"));
				item1.BackColor = Color.White;
				item1.SubItems.Add(((TextLine) source.GetByIndex(span1.SourceIndex + num2)).Line);
				item2.BackColor = Color.White;
				item2.SubItems.Add(((TextLine) destination.GetByIndex(span1.DestIndex + num2)).Line);
				this.lvSource.Items.Add(item1);
				this.lvDestination.Items.Add(item2);
				num1++;
				num2++;
			Label_01DD:
				if (num2 < span1.Length)
				{
					goto Label_012D;
				}
				continue;
			Label_01F6:
				item1 = new ListViewItem(num1.ToString("00000"));
				item2 = new ListViewItem(num1.ToString("00000"));
				item1.BackColor = Color.LightGray;
				item1.SubItems.Add("");
				item2.BackColor = Color.LightGreen;
				item2.SubItems.Add(((TextLine) destination.GetByIndex(span1.DestIndex + num2)).Line);
				this.lvSource.Items.Add(item1);
				this.lvDestination.Items.Add(item2);
				num1++;
				num2++;
			Label_0292:
				if (num2 < span1.Length)
				{
					goto Label_01F6;
				}
				continue;
			Label_02AB:
				item1 = new ListViewItem(num1.ToString("00000"));
				item2 = new ListViewItem(num1.ToString("00000"));
				item1.BackColor = Color.Red;
				item1.SubItems.Add(((TextLine) source.GetByIndex(span1.SourceIndex + num2)).Line);
				item2.BackColor = Color.LightGreen;
				item2.SubItems.Add(((TextLine) destination.GetByIndex(span1.DestIndex + num2)).Line);
				this.lvSource.Items.Add(item1);
				this.lvDestination.Items.Add(item2);
				num1++;
				num2++;
			Label_035B:
				if (num2 < span1.Length)
				{
					goto Label_02AB;
				}
			}
			int temp=0;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.lvSource = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.lvDestination = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvSource
			// 
			this.lvSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader5,
																					   this.columnHeader6});
			this.lvSource.FullRowSelect = true;
			this.lvSource.HideSelection = false;
			this.lvSource.Location = new System.Drawing.Point(28, 17);
			this.lvSource.MultiSelect = false;
			this.lvSource.Name = "lvSource";
			this.lvSource.Size = new System.Drawing.Size(114, 102);
			this.lvSource.TabIndex = 0;
			this.lvSource.View = System.Windows.Forms.View.Details;
			this.lvSource.Resize += new System.EventHandler(this.lvSource_Resize);
			this.lvSource.SelectedIndexChanged += new System.EventHandler(this.lvSource_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Line";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Text (Source)";
			this.columnHeader2.Width = 147;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Line";
			this.columnHeader3.Width = 50;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Text (Destination)";
			this.columnHeader4.Width = 198;
			// 
			// lvDestination
			// 
			this.lvDestination.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2});
			this.lvDestination.FullRowSelect = true;
			this.lvDestination.HideSelection = false;
			this.lvDestination.Location = new System.Drawing.Point(272, 15);
			this.lvDestination.MultiSelect = false;
			this.lvDestination.Name = "lvDestination";
			this.lvDestination.Size = new System.Drawing.Size(123, 110);
			this.lvDestination.TabIndex = 2;
			this.lvDestination.View = System.Windows.Forms.View.Details;
			this.lvDestination.Resize += new System.EventHandler(this.lvDestination_Resize);
			this.lvDestination.SelectedIndexChanged += new System.EventHandler(this.lvDestination_SelectedIndexChanged);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Line";
			this.columnHeader5.Width = 50;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Text (Destination)";
			this.columnHeader6.Width = 147;
			// 
			// FrmShowDiff
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(533, 440);
			this.Controls.Add(this.lvDestination);
			this.Controls.Add(this.lvSource);
			this.Name = "FrmShowDiff";
			this.Text = "Results";
			this.Resize += new System.EventHandler(this.Results_Resize);
			this.Load += new System.EventHandler(this.Results_Load);
			this.ResumeLayout(false);

		}

		private void lvDestination_Resize(object sender, EventArgs e)
		{
			if (this.lvDestination.Width > 100)
			{
				this.lvDestination.Columns[1].Width = -2;
			}
		}

		private void lvDestination_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lvDestination.SelectedItems.Count > 0)
			{
				ListViewItem item1 = this.lvSource.Items[this.lvDestination.SelectedItems[0].Index];
				item1.Selected = true;
				item1.EnsureVisible();
			}
		}

		private void lvSource_Resize(object sender, EventArgs e)
		{
			if (this.lvSource.Width > 100)
			{
				this.lvSource.Columns[1].Width = -2;
			}
		}

		private void lvSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lvSource.SelectedItems.Count > 0)
			{
				ListViewItem item1 = this.lvDestination.Items[this.lvSource.SelectedItems[0].Index];
				item1.Selected = true;
				item1.EnsureVisible();
			}
		}

		private void Results_Load(object sender, EventArgs e)
		{
			this.Results_Resize(sender, e);
		}

		private void Results_Resize(object sender, EventArgs e)
		{
			int num1 = base.ClientRectangle.Width / 2;
			this.lvSource.Location = new Point(0, 0);
			this.lvSource.Width = num1;
			this.lvSource.Height = base.ClientRectangle.Height;
			this.lvDestination.Location = new Point(num1 + 1, 0);
			this.lvDestination.Width = base.ClientRectangle.Width - (num1 + 1);
			this.lvDestination.Height = base.ClientRectangle.Height;
		}


		// Fields
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private Container components;
		private ListView lvDestination;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private ListView lvSource;
	}
}
