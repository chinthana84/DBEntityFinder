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
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using System.Data;
using System.Data.SqlClient;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class FrmOutput : FrmBaseContent
	{
		#region Members
		IDataAdapter _dataAdapter = null;
		DataSet _dataSet=null;
		private System.Windows.Forms.RichTextBox rtbResult;
		public System.Windows.Forms.TabControl tabControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		#region Default
		public FrmOutput(Form mdiParentForm)
		{
			this.MdiParentForm=mdiParentForm;
			InitializeComponent();
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmOutput));
			this.rtbResult = new System.Windows.Forms.RichTextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// rtbResult
			// 
			this.rtbResult.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtbResult.Location = new System.Drawing.Point(64, 264);
			this.rtbResult.Name = "rtbResult";
			this.rtbResult.TabIndex = 6;
			this.rtbResult.Text = "";
			this.rtbResult.Visible = false;
			this.rtbResult.WordWrap = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(688, 506);
			this.tabControl1.TabIndex = 7;
			this.tabControl1.Click += new System.EventHandler(this.FrmOutput_Click);
			// 
			// FrmOutput
			// 
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float 
				| WeifenLuo.WinFormsUI.DockAreas.DockLeft) 
				| WeifenLuo.WinFormsUI.DockAreas.DockRight) 
				| WeifenLuo.WinFormsUI.DockAreas.DockTop) 
				| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockBottomAutoHide;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 510);
			this.CloseButton = false;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.rtbResult);
			this.DockPadding.Bottom = 2;
			this.DockPadding.Top = 2;
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmOutput";
			this.Text = "Output";
			this.Click += new System.EventHandler(this.FrmOutput_Click);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion
		#region Events
		private void dataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
			try
			{
				string QueryName = tabControl1.SelectedTab.Text;

				DataGrid dg = (DataGrid)sender;
				DataSet ds = (DataSet)dg.DataSource;
				
				if(_dataAdapter is SqlDataAdapter)
					((SqlDataAdapter)_dataAdapter).Update(ds.Tables[QueryName]); 
				else if(_dataAdapter is MySql.Data.MySqlClient.MySqlDataAdapter)
					((MySql.Data.MySqlClient.MySqlDataAdapter)_dataAdapter).Update(ds.Tables[QueryName]);
				else if(_dataAdapter is Sybase.Data.AseClient.AseDataAdapter)
					((Sybase.Data.AseClient.AseDataAdapter)_dataAdapter).Update(ds.Tables[QueryName]);
				else if(_dataAdapter is Oracle.DataAccess.Client.OracleDataAdapter)
					((Oracle.DataAccess.Client.OracleDataAdapter)_dataAdapter).Update(ds.Tables[QueryName]);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "QueryCommander",MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			
			}
		}
		
		private void DataGridCell_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string s = e.KeyChar.ToString();

			if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyChar.ToString().ToUpper() == "C")
				return;
		}
		private void DataGridCell_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		private void DataGridCell_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		private void FrmOutput_Click(object sender, System.EventArgs e)
		{
			foreach(Control control in tabControl1.SelectedTab.Controls)
			{
				if(control is DataGrid)
				{
					DataGrid dg=(DataGrid)control;
					MainForm frm = (MainForm)MdiParentForm;
					DataTable dt = null;
					if(dg.DataSource is DataSet)
					{
						foreach(DataTable datatable in ((DataSet)dg.DataSource).Tables)
						{
							if(datatable.TableName==dg.DataMember)
							{
								dt=datatable;
								break;
							}
						}
						string rowCountString="Rowcount: " + dt.Rows.Count.ToString();
						frm.statusBar.Panels[3].Text = rowCountString;
					}
				}
			}
		}
		#endregion
		#region Methods
//		public string GetTextContentFromDataSet(DataSet dataSet)
//		{
//			string outPut="";
//			foreach(DataTable table in dataSet.Tables)
//			{
//				// Header
//				foreach(DataColumn col in table.Columns)
//				{
//					dataSet.s
//					outPut+=col.ColumnName;
//				}
//
//				foreach(DataRow row in table.Rows)
//				{
//					foreach(DataColumn col in table.Columns)
//					{
//						outPut+=row[col.ColumnName].ToString();
//					}
//				}
//			}
//			return outPut;
//		}
		public void BrowseTable(DataTable datatable, IDataAdapter adapter)
		{
			_dataAdapter = adapter;
			_dataSet = datatable.DataSet;
			tabControl1.TabPages.Clear();
			AddDataGrid(datatable);
			this.Text = "OutPut [rowcount:" + datatable.Rows.Count.ToString() + "]";
			
		}
		public void BrowseTable(DataSet dataset, IDataAdapter adapter)
		{
			_dataAdapter = adapter;
			_dataSet = dataset;
			tabControl1.TabPages.Clear();
			
			foreach(DataTable dataTable in dataset.Tables)
			{
				AddDataGrid(dataTable);
			}

			this.Text = "OutPut [" + dataset.Tables.Count.ToString() + " tables]";
			if(dataset.Tables.Count>1)
				this.Text += " - Read only";
		}

		private void AutoSizeColumns(DataGrid grid, DataTable dataTable)
		{
			int maxWidth = 500;
			int maxRows  = 100;
			int padding  = 20;

			//DataTable dataTable = ((DataTable)grid.DataSource);
			Graphics graphics = Graphics.FromHwnd(grid.Handle);
			//StringFormat format = new StringFormat("MM/dd/yyyy hh:mm:ss");
			StringFormat format = new StringFormat(StringFormat.GenericDefault);// .GenericTypographic);
			SizeF size; // Size struct of X,Y floating

			float width;

			string caption;
			int totalRows = dataTable.Rows.Count;
			int rowCount  = Math.Min(totalRows,maxRows); // Avg of first 100 rows only
			int colCount  = dataTable.Columns.Count;

			for(int col=0;col<colCount;col++)
			{
				// Check the caption's width first
				caption = dataTable.Columns[col].Caption.ToString();
				size = graphics.MeasureString(caption, grid.HeaderFont, maxWidth, format);
				width = size.Width + padding;
				// Loop all rows to get the widest
				for(int row=0;row<rowCount;row++)
				{
					size = graphics.MeasureString(dataTable.Rows[row][col].ToString(), grid.Font, maxWidth, format);
					if(size.Width + padding > width) {width = size.Width + padding;}
				}
				// Apply width
				grid.TableStyles[0].GridColumnStyles[col].Width = (int)width;
			}

			graphics.Dispose();
		}
		public void AddText(string text)
		{
			tabControl1.TabPages.Clear();
			TabPage tp = new TabPage();
			tp.Location = new System.Drawing.Point(4, 22);
			tp.Name = "tabPage1";
			tp.Size = new System.Drawing.Size(488, 406);
			tp.TabIndex = 0;
			tp.Text = "Message";
			tp.Controls.Add(rtbResult);
			rtbResult.Dock = DockStyle.Fill;
			rtbResult.Visible = true;
			rtbResult.Text = text;
			tabControl1.TabPages.Add(tp);		
		}

		private void AddDataGrid(DataTable dataTable)
		{
			tabControl1.SuspendLayout();
	
			
			// Add new tab page
			TabPage tp = new TabPage();
			tp.Location = new System.Drawing.Point(4, 22);
			tp.Name = "tabPage1";
			tp.Size = new System.Drawing.Size(1488, 1406);
			tp.TabIndex = 0;
			tp.Text = dataTable.TableName;
			//tp.Click+=new EventHandler(tp_Click);
		
			// Add new DataGrid
			System.Windows.Forms.DataGrid dg = new System.Windows.Forms.DataGrid();
			tp.Controls.Add(dg);
			dg.Dock = DockStyle.Fill;
			dg.BackgroundColor = System.Drawing.SystemColors.Window;
			dg.CaptionBackColor = System.Drawing.SystemColors.InactiveBorder;
			dg.DataMember = "";
			dg.FlatMode = true;
			dg.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			dg.Location = new System.Drawing.Point(0, 24);
			dg.Name = "dataGrid";
			dg.TabIndex = 2;
			dg.Visible = true;
			dg.Visible = true;
			dg.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName = dataTable.TableName;

			dg.TableStyles.Clear();

			foreach(DataColumn col in dataTable.Columns)
			{
				if(col.DataType.ToString() == "System.Boolean")
				{
					DataGridBoolColumn colStyleBool = new DataGridBoolColumn();
					colStyleBool.TrueValue = true;
					colStyleBool.FalseValue = false;
					colStyleBool.HeaderText = col.ColumnName;
					colStyleBool.MappingName = col.ColumnName;
					tableStyle.GridColumnStyles.Add(colStyleBool);
				}
				else
				{
					DataGridTextBoxColumn colStyleText = new DataGridTextBoxColumn();
					PropertyDescriptor pd = colStyleText.PropertyDescriptor;
					colStyleText.HeaderText = col.ColumnName;
					colStyleText.MappingName = col.ColumnName;
					tableStyle.GridColumnStyles.Add(colStyleText);
					colStyleText.TextBox.KeyPress +=new KeyPressEventHandler(DataGridCell_KeyPress);
				}
			}
			dg.TableStyles.Add(tableStyle);
			dg.SetDataBinding(_dataSet,dataTable.TableName);
			AutoSizeColumns(dg,dataTable);

			tabControl1.TabPages.Add(tp);
			tabControl1.ResumeLayout(true);
			
		}
		public void AddMessage(string msg)
		{
			System.Windows.Forms.RichTextBox txtMessage  = new RichTextBox();
			

			TabPage tp = new TabPage();
			tp.Location = new System.Drawing.Point(4, 22);
			tp.Name = "tabPage1";
			tp.Size = new System.Drawing.Size(488, 406);
			tp.TabIndex = 0;
			tp.Text = "Message";
			tp.Controls.Add(txtMessage);
			txtMessage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			txtMessage.Location = new System.Drawing.Point(64, 264);
			txtMessage.Name = "rtbResult";
			txtMessage.TabIndex = 6;
			txtMessage.Text = "";
			txtMessage.Visible = true;
			txtMessage.WordWrap = false;
			txtMessage.Dock = DockStyle.Fill;
			txtMessage.Text = msg;

			tabControl1.TabPages.Add(tp);
			
		}
		#endregion
	}
}





