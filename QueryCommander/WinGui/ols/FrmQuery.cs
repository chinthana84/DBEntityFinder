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
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using QueryCommander.General;
using QueryCommander.PlugIn.Core;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using QueryCommander.Database;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for FrmQuery.
	/// </summary>
	public class FrmQuery : FrmBaseContent
	{
		#region Private members
		private OutPutContainer _outPutContainer=null;
		private Hashtable Aliases = new Hashtable();
		private ArrayList AliasList = new ArrayList();
		private int lastPos;
		private int firstPos;
		private XmlDocument sqlReservedWords = new XmlDocument();
		private static ArrayList _sqlInfoMessages = new ArrayList();
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ContextMenu cmShortcutMeny;		
		private ArrayList ReservedWords = new ArrayList();
		private bool DoInsert;
		private string _OrginalName;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView lstv_Commands;
		private System.ComponentModel.IContainer components;
		private string m_fileName = string.Empty;
		private bool m_resetText = true;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private Regex _findNextRegex; 
		private int _findNextStartPos;
		#endregion
		#region Public members
		
		public bool IsActive;
		/// <summary>
		/// All queries will be executed with this connection
		/// </summary>
		public IDbConnection dbConnection = null;
		/// <summary>
		/// Current database name
		/// </summary>
		public string DatabaseName = "";
		public QueryCommander.Editor.TextEditorControlWrapper qcTextEditor;
		/// <summary>
		/// The Syntax reader handles all font and color settings.
		/// </summary>
		public SyntaxReader syntaxReader;
		/// <summary>
		/// Used when opening/saving.
		/// </summary>
		public string FileName
		{
			get	{	return m_fileName;	}
			set
			{
				if (value != string.Empty)
				{
					string fileName = value.Substring(value.LastIndexOf(@"\")+1);
					this.Text=fileName;
				}

				m_fileName = value;
			}
		}

		/// <summary>
		/// The content of the qcTextEditor
		/// </summary>
		public string Content
		{
			get { return qcTextEditor.Text;}
			set 
			{ 
				qcTextEditor.Text=value;

				qcTextEditor.Refresh();
				MainForm frm = (MainForm)MdiParentForm;
				frm.SetPandelInfo("dbConnection.DataSource", dbConnection.Database);
			}
		}

		/// <summary>
		/// Font settings 
		/// </summary>
		public Font EditorFont
		{
			get{return qcTextEditor.Font;}
			set{qcTextEditor.Font = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		
		#endregion
		#region Default
		public FrmQuery(Form mdiParentForm)
		{
			this.MdiParentForm=mdiParentForm;
			InitializeComponent();

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			syntaxReader =  new SyntaxReader();
			
			qcTextEditor.Font = syntaxReader.EditorFont;
			qcTextEditor.ActiveTextAreaControl.TextArea.MouseUp +=new MouseEventHandler(qcTextEditor_MouseUp);
			qcTextEditor.ActiveTextAreaControl.TextArea.DragDrop +=new DragEventHandler(TextArea_DragDrop);
			qcTextEditor.ActiveTextAreaControl.TextArea.DragEnter +=new DragEventHandler(TextArea_DragEnter);
			qcTextEditor.ActiveTextAreaControl.TextArea.Click +=new EventHandler(TextArea_Click);
			 
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
			settings.ShowEOLMarkers=qcTextEditor.ShowEOLMarkers;
			settings.ShowSpaces=qcTextEditor.ShowSpaces;
			settings.ShowTabs=qcTextEditor.ShowTabs;
			settings.ShowLineNumbers=qcTextEditor.ShowLineNumbers;
			settings.ShowMatchingBracket=qcTextEditor.ShowMatchingBracket;

			settings.fontFamily = qcTextEditor.Font.FontFamily.Name;
			settings.fontGraphicsUnit = qcTextEditor.Font.Unit;
			settings.fontSize = qcTextEditor.Font.Size;
			settings.fontStyle = qcTextEditor.Font.Style;
			
			settings.Save();

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmQuery));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lstv_Commands = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmShortcutMeny = new System.Windows.Forms.ContextMenu();
			this.qcTextEditor = new QueryCommander.Editor.TextEditorControlWrapper();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth4Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lstv_Commands
			// 
			this.lstv_Commands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1});
			this.lstv_Commands.FullRowSelect = true;
			this.lstv_Commands.HideSelection = false;
			this.lstv_Commands.LabelWrap = false;
			this.lstv_Commands.Location = new System.Drawing.Point(304, 104);
			this.lstv_Commands.MultiSelect = false;
			this.lstv_Commands.Name = "lstv_Commands";
			this.lstv_Commands.Size = new System.Drawing.Size(200, 136);
			this.lstv_Commands.SmallImageList = this.imageList1;
			this.lstv_Commands.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstv_Commands.TabIndex = 1;
			this.lstv_Commands.TabStop = false;
			this.lstv_Commands.View = System.Windows.Forms.View.SmallIcon;
			this.lstv_Commands.Visible = false;
			this.lstv_Commands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstv_Commands_KeyDown);
			this.lstv_Commands.Leave += new System.EventHandler(this.lstv_Commands_Leave);
			this.lstv_Commands.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstv_Commands_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 200;
			// 
			// qcTextEditor
			// 
			this.qcTextEditor.AllowDrop = true;
			this.qcTextEditor.AutoScroll = true;
			this.qcTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.qcTextEditor.Encoding = ((System.Text.Encoding)(resources.GetObject("qcTextEditor.Encoding")));
			this.qcTextEditor.IsIconBarVisible = false;
			this.qcTextEditor.Location = new System.Drawing.Point(2, 2);
			this.qcTextEditor.Name = "qcTextEditor";
			this.qcTextEditor.SelectedText = "";
			this.qcTextEditor.SelectionStart = 0;
			this.qcTextEditor.ShowEOLMarkers = true;
			this.qcTextEditor.ShowInvalidLines = false;
			this.qcTextEditor.ShowSpaces = true;
			this.qcTextEditor.ShowTabs = true;
			this.qcTextEditor.ShowVRuler = true;
			this.qcTextEditor.Size = new System.Drawing.Size(788, 618);
			this.qcTextEditor.TabIndex = 2;
			this.qcTextEditor.KeyPressEvent += new QueryCommander.Editor.TextEditorControlWrapper.KeyPressEventHandler(this.qcTextEditor_KeyPressEvent);
			this.qcTextEditor.RMouseUpEvent += new QueryCommander.Editor.TextEditorControlWrapper.MYMouseRButtonUpEventHandler(this.qcTextEditor_MouseUp);
			// 
			// FrmQuery
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 622);
			this.Controls.Add(this.lstv_Commands);
			this.Controls.Add(this.qcTextEditor);
			this.DockPadding.All = 2;
			this.Name = "FrmQuery";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmQuery_Closing);
			this.Load += new System.EventHandler(this.FrmQuery_Load);
			this.Enter += new System.EventHandler(this.FrmQuery_Enter);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion
		#region Events
		/// <summary>
		/// Recieves all Infomessages from executed statement
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
		{
			Console.WriteLine("***Got InfoMessage***");
			foreach (SqlError err in args.Errors)
			{
				System.Windows.Forms.Application.DoEvents();
				if(!_sqlInfoMessages.Contains(err))
					_sqlInfoMessages.Add(err);
				Console.WriteLine("\t" + err);
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (m_resetText)
			{
				m_resetText = false;
				FileName = FileName;
			}
		}

		protected override string GetPersistString()
		{
			return GetType().ToString() + "," + FileName + "," + Text;
		}

		private void FrmQuery_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MainForm frm = (MainForm)MdiParentForm;
			frm.QueryForms.Remove(this);

			syntaxReader.Save(qcTextEditor.Font);
		}
		private void FrmQuery_Load(object sender, System.EventArgs e)
		{
			MainForm frm = (MainForm)MdiParentForm;
			MainForm.DBConnection dbc = (MainForm.DBConnection)frm.DBConnections[0];
			dbConnection = dbc.Connection;
			if(dbConnection is SqlConnection)
				((SqlConnection)dbConnection).InfoMessage +=new SqlInfoMessageEventHandler(OnInfoMessage); 
			_OrginalName = this.Text;

			string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings.config");
			QueryCommander.Config.Settings settings;
			if(File.Exists(filename))
			{
				try
				{
					settings = QueryCommander.Config.Settings.Load();
					qcTextEditor.ShowEOLMarkers=settings.ShowEOLMarkers;
					qcTextEditor.ShowSpaces=settings.ShowSpaces;
					qcTextEditor.ShowTabs=settings.ShowTabs;
					qcTextEditor.ShowLineNumbers=settings.ShowLineNumbers;
					qcTextEditor.ShowMatchingBracket=settings.ShowMatchingBracket;
					qcTextEditor.Font=settings.GetFont();
					qcTextEditor.ShowVRuler=false;
				}
				catch(Exception ex)
				{
					qcTextEditor.ShowEOLMarkers=false;
					qcTextEditor.ShowSpaces=false;
					qcTextEditor.ShowTabs=false;
					qcTextEditor.ShowVRuler=false;
				}
			}
			else
			{
				qcTextEditor.ShowEOLMarkers=false;
				qcTextEditor.ShowSpaces=false;
				qcTextEditor.ShowTabs=false;
				qcTextEditor.ShowVRuler=false;
			}

			frm.SetPandelInfo("dbConnection.DataSource", dbConnection.Database);
			AddPluginMenuItems();
			
		}
		private void FrmQuery_Enter(object sender, System.EventArgs e)
		{
			MainForm frm = (MainForm)MdiParentForm;
			frm.SetPandelInfo("dbConnection.DataSource", DatabaseName);
			
		}

		private void lstv_Commands_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Tab)
				qcTextEditor.SelectedText = "\t";

			if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Back) // No update
			{
				DoInsert = false;
				lstv_Commands.Hide();
				qcTextEditor.Focus();
			}
		
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
			{
				DoInsert = true;
				lstv_Commands.Hide();
				qcTextEditor.Focus();
			}
		}

		private void lstv_Commands_Leave(object sender, System.EventArgs e)
		{
			//int fPos =TextUtilities.FindWordEnd(qcTextEditor.Document, qcTextEditor.ActiveTextAreaControl.TextArea.Caret.Offset);
			if(lstv_Commands.SelectedItems.Count>0 && DoInsert)
			{
				//qcTextEditor.Select(firstPos,lastPos-firstPos);

				if(lstv_Commands.SelectedItems[0].Tag==null)
				{
					qcTextEditor.Document.Replace(firstPos,lastPos-firstPos,lstv_Commands.SelectedItems[0].Text);
					
					int pos = firstPos+lstv_Commands.SelectedItems[0].Text.Length;
					qcTextEditor.SetPosition(pos);
				}
				else
					qcTextEditor.SelectedText = lstv_Commands.SelectedItems[0].Tag.ToString();
				
				lstv_Commands.Hide();

			}
			else
			{
				lstv_Commands.Hide();
				qcTextEditor.SelectionStart=lastPos;
			}
		}
		
		private void lstv_Commands_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListView lv = (ListView)sender;
			ListViewItem lvi = lv.GetItemAt(e.X, e.Y);

			if (lvi != null)
			{
				toolTip1.Active = true;
				toolTip1.SetToolTip(lv, lvi.Text);
			} 
			else
				toolTip1.Active = false; 
		}
		
		private void qcTextEditor_KeyPressEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			string keyString = e.ToString();
			string line=qcTextEditor.ActiveTextAreaControl.Caret.Line.ToString();
			string col=qcTextEditor.ActiveTextAreaControl.Caret.Column.ToString();
			((MainForm)MdiParentForm).SetPandelPositionInfo(line,col);
			if (e.Alt == true && e.KeyValue == 88)
			{
				e.Handled = false;
				RunQuery();
				return;
			}
			if (e.KeyCode == Keys.Down)
				lstv_Commands.Focus();
			
			if(e.KeyCode == Keys.F5)
				RunQuery();

			if (((Control.ModifierKeys & Keys.Control) == Keys.Control)&&e.KeyValue==32 
				|| e.KeyValue == 190)
			{
				if(e.KeyValue == 190)
				{
					ApplyProperty();
				}
				else
				{
					e.Handled = true;
					ComplementWord();
				}
			}
		}

		private void TextArea_DragDrop(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.FileDrop) )
			{
				// Assign the file names to a string array, in 
				// case the user has selected multiple files.
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				try
				{
					string fullName = files[0];
					if(files.Length>1)
						return;
					MainForm mainform = (MainForm)MdiParentForm;
					string fileName = Path.GetFileName(fullName);
					string line;
					string fileContent = "";

					FrmQuery frmquery = new FrmQuery( MdiParentForm );
					
					StreamReader sr = new StreamReader(fullName);

					while ((line = sr.ReadLine()) != null) 
					{
						fileContent += "\n" + line;
					}
					sr.Close();
					sr=null;

					this.Content=fileContent;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}

		}
		private void TextArea_DragEnter(object sender, DragEventArgs e)
		{

			if (((DragEventArgs)e).Data.GetDataPresent(DataFormats.FileDrop)) 
				((DragEventArgs)e).Effect = DragDropEffects.Copy;
			else
				((DragEventArgs)e).Effect = DragDropEffects.None; 
			
		}
		
		private void TextArea_Click(object sender, EventArgs e)
		{
			string line=qcTextEditor.ActiveTextAreaControl.Caret.Line.ToString();
			string col=qcTextEditor.ActiveTextAreaControl.Caret.Column.ToString();
			((MainForm)MdiParentForm).SetPandelPositionInfo(line,col);
		}
		#endregion
		#region Private Methods
		private static DragDropEffects GetDragDropEffect(DragEventArgs e)
		{
			if ((e.AllowedEffect & DragDropEffects.Move) > 0 &&	(e.AllowedEffect & DragDropEffects.Copy) > 0) 
			{
				return (e.KeyState & 8) > 0 ? DragDropEffects.Copy : DragDropEffects.Move;
			} 
			else if ((e.AllowedEffect & DragDropEffects.Move) > 0) 
			{
				return DragDropEffects.Move;
			} 
			else if ((e.AllowedEffect & DragDropEffects.Copy) > 0) 
			{
				return DragDropEffects.Copy;
			}
			return DragDropEffects.None;
		}

		private int ParseText(WordAndPosition[] words, string s)
		{
			words.Initialize();
			int count = 0;
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ []\f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch()) 
			{
				words[count].Word = m.Value;
				words[count].Position = m.Index;
				words[count].Length = m.Length;
				count++;
			}
			return count;
		}
		private string Create2Alter(string script, string type)
		{
			string returnString=script;
			WordAndPosition[] words  = new WordAndPosition[20000];
			int count = ParseText(words,script);
			
			for(int i=0;i<count;i++)
			{
				if(words[i].Word.ToUpper()=="CREATE" && words[i+1].Word.ToUpper()==type.ToUpper())
				{
					returnString = returnString.Substring(0,words[i].Position) + "ALTER" + returnString.Substring(words[i].Position + words[i].Length, returnString.Length-(words[i].Position + words[i].Length));
					break;
				}
			}
			return returnString;
			
		}
		private bool IsCheckedOutBuOtherUser()
		{
			WordAndPosition[] words  = new WordAndPosition[20000];
			int count = ParseText(words,qcTextEditor.Text);
			
			QueryCommander.VSS.VSSConnection.DBObjectTypes type;
			
			for(int i=0;i<count;i++)
			{
				string objectName = words[i+2].Word;
				if((words[i].Word.ToUpper()=="CREATE" || words[i].Word.ToUpper()=="ALTER")&& 
					(words[i+1].Word.ToUpper()=="TABLE" ||
					words[i+1].Word.ToUpper()=="VIEW" ||
					words[i+1].Word.ToUpper()=="PROCEDURE" ||
					words[i+1].Word.ToUpper()=="FUNCTION"))
				{

					MainForm frm =  (MainForm)MdiParentForm;
					
					foreach(QueryCommander.VSS.VSSConnection vssConnection in frm.vssConnectionCollection)
					{
						if(vssConnection.Database.ToUpper()==this.DatabaseName.ToUpper() && 
							vssConnection.Server.ToUpper()=="this.dbConnection.DataSource.ToUpper()")
						{
							switch(words[i+1].Word.ToUpper())
							{
								case "TABLE":
									type=QueryCommander.VSS.VSSConnection.DBObjectTypes.Table;
									break;
								case "VIEW":
									type=QueryCommander.VSS.VSSConnection.DBObjectTypes.View;
									break;
								case "PROCEDURE":
									type=QueryCommander.VSS.VSSConnection.DBObjectTypes.StoredProcedure;
									break;
								case "FUNCTION":
									type=QueryCommander.VSS.VSSConnection.DBObjectTypes.Function;
									break;
								default:
									return false;

							}
							
							string vssPath = vssConnection.GetVSSPath(type) + "\\" + objectName+".SQL";
							return vssConnection.GetStatus(vssPath)==1;
						}
						break;
					}
				}
			}
			return false;
		}

		private string GetObjectName()
		{
			try
			{
				WordAndPosition[] words  = new WordAndPosition[20000];
				int count = ParseText(words,qcTextEditor.Text);
			
				for(int i=0;i<count;i++)
				{
					if((words[i].Word.ToUpper()=="CREATE" && (words[i+1].Word.ToUpper()=="PROCEDURE" || words[i+1].Word.ToUpper()=="FUNCTION" || words[i+1].Word.ToUpper()=="VIEW")) || 
						(words[i].Word.ToUpper()=="ALTER" && (words[i+1].Word.ToUpper()=="PROCEDURE" || words[i+1].Word.ToUpper()=="FUNCTION" || words[i+1].Word.ToUpper()=="VIEW")))
					{
						if(words[i+2].Word.ToUpper()=="DBO")
							return words[i+3].Word;
						else
							return words[i+2].Word;
					}
				}
				return "";
			}
			catch
			{
				return "";
			}
		}

		private int PreviusIndexOf(string character)
		{
			for(int i=qcTextEditor.SelectionStart;i>0;i--)
			{
				if(qcTextEditor.Text.Substring(i-1,1)==character)
				{
					return i;
				}
			}
			return 0;
		}
		private void ApplyProperty()
		{	
			try
			{
				ArrayList DatabaseObjects = null;
				lastPos			= qcTextEditor.SelectionStart;
				string t		= qcTextEditor.Text.Substring(0,lastPos);
				int lastSpace	= t.LastIndexOf(" ");
				int lastEnter	= t.LastIndexOf("\n");
				int lastTab		= t.LastIndexOf("\t");

				firstPos = lastSpace > lastEnter  ? lastSpace : lastEnter;
				firstPos = firstPos > lastTab  ? firstPos : lastTab;
				firstPos++;
				string word = t.Substring(firstPos,t.Length-firstPos);
				int dotPos = word.IndexOf(".");
				if(dotPos>0)
				{	
					word = word.Substring(dotPos+1);
					firstPos+=dotPos+1;
				}

				if(word.Length>0)
				{
					//Clear
					foreach(ListViewItem l in lstv_Commands.Items)
						l.Remove();

					word = GetAliasTableName(word);
			
					//Properties
					IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
					DatabaseObjects = db.GetDatabasesObjectProperties(DatabaseName,word,dbConnection);
					if(DatabaseObjects == null)
						return;
				
					if(DatabaseObjects.Count>0)
						lstv_Commands.Items.Add("*",2);

					foreach(Database.DBObjectProperties dbObjectProperties in DatabaseObjects)
					{
						lstv_Commands.Items.Add(dbObjectProperties.Name,3);
					}
					//					}
					lastPos++;
					firstPos = lastPos;
					if(lstv_Commands.Items.Count==0)	  //No match
						return;
					else if(lstv_Commands.Items.Count==1) //One Match
					{
						qcTextEditor.Select(firstPos,lastPos-firstPos);
						qcTextEditor.SelectedText = lstv_Commands.Items[0].Text;
					}
					else								  //Selection is required
					{
						DoInsert=true;
						int formHeight = this.Size.Height;
						int formWidth = this.Size.Width;
						lstv_Commands.Width=200;

						Point pt = qcTextEditor.GetPositionFromCharIndex(qcTextEditor.SelectionStart);
						pt.Y += qcTextEditor.Font.Height;

						if(pt.Y + lstv_Commands.Height > formHeight)
							pt.Y = pt.Y- (lstv_Commands.Height+(qcTextEditor.Font.Height/2));

						if(pt.X + lstv_Commands.Width > formWidth)
							pt.X = pt.X - lstv_Commands.Width;

						lstv_Commands.Location = pt;

						lstv_Commands.Visible = true;
						lstv_Commands.Focus();
						lstv_Commands.Items[0].Selected = true;	
					}
				}
			}
			catch{return;}
		}

		private void ComplementWord()
		{	
			try
			{
				int textWidth = 200;
				int fac = 5;
				lastPos		= qcTextEditor.SelectionStart;
				firstPos	= 0;
				ToolTip toolTip1 = new ToolTip();
				toolTip1.AutoPopDelay = 5000;
				toolTip1.InitialDelay = 1000;
				toolTip1.ReshowDelay = 500;
				toolTip1.ShowAlways = true;


				int lastSpace = PreviusIndexOf(" ");
				int lastEnter = PreviusIndexOf("\n");
				int firstTab = PreviusIndexOf("\t");

				if(lastSpace > 0)
					firstPos = lastSpace;
				if(lastEnter > firstPos)
					firstPos = lastEnter;
				if(firstTab > firstPos)
					firstPos = firstTab;

				string word		= (qcTextEditor.Text.Substring(firstPos,lastPos-firstPos));
			
				int dotPos = word.IndexOf(".");
				if(dotPos>0)
				{
					
					word = word.Substring(dotPos+1);
					firstPos+=dotPos+1;
				}
				
				if(word.Length==0)
				{
					// Snippets
					XmlDocument xmlSnippets = new XmlDocument();
					xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
					XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

					foreach(XmlNode node in xmlNodeList[0].ChildNodes)
					{
						ListViewItem lvi = lstv_Commands.Items.Add(node.Attributes["name"].Value,5);
						string statement = node.InnerText;

						statement = statement.Replace(@"\n","\n");
						statement = statement.Replace(@"\t","\t");
						lvi.Tag=statement;
						
					}
				}
				else
				{
					//Clear
					foreach(ListViewItem l in lstv_Commands.Items)
						l.Remove();

					// Variables
					if(word.Substring(0,1)=="@")
					{
						foreach(string var in qcTextEditor.GetVariables(word))
						{
							if((var.Length*fac)>textWidth)
								textWidth=var.Length*fac;

							lstv_Commands.Items.Add(var,2);
						}
					}
					else
					{
						//Reserved Words
						foreach(XmlNode node in syntaxReader.xmlNodeList[0].ChildNodes)
						{
							if(node.Name.StartsWith(word.ToUpper()))
							{
								if((node.Name.Length*fac)>textWidth)
									textWidth=node.Name.Length*fac;
								lstv_Commands.Items.Add(node.Name,2);
							}
						}
			
						//Operations
						IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
//						Database db = new Database();
						ArrayList DatabaseObjects = db.GetDatabasesObjects(DatabaseName,word,dbConnection);

						foreach(Database.DBObject dbObject in DatabaseObjects)
						{
							if((dbObject.Name.Length*fac)>textWidth)
								textWidth=dbObject.Name.Length*fac;
								
							//							ListViewItem lvi;
							switch(dbObject.Type.ToUpper())
							{
								case "V ": //Tables
									lstv_Commands.Items.Add(dbObject.Name,4);
									break;
								case "U ": //Tables
									lstv_Commands.Items.Add(dbObject.Name,4);
									break;
								case "P ": //Stored Procedures
									lstv_Commands.Items.Add(dbObject.Name,1);
									break;
								case "FN": //Functions
									lstv_Commands.Items.Add("dbo." + dbObject.Name,0);
									break;
								case "TF": //Functions
									lstv_Commands.Items.Add("dbo." + dbObject.Name,0);
									break;
								default:
									lstv_Commands.Items.Add(dbObject.Name,2);
									break;
							}
						}
					}
				}

				if(lstv_Commands.Items.Count==0)	  //No match
					return;
				else if(lstv_Commands.Items.Count==1) //One Match
				{
					qcTextEditor.Document.Replace(firstPos,lastPos-firstPos,lstv_Commands.Items[0].Text);
					int pos = firstPos+lstv_Commands.Items[0].Text.Length;
					
					qcTextEditor.SetPosition(pos);

					
				}
				else								  //Selection is required
				{
					DoInsert=true;
					int formHeight = this.Size.Height;
					int formWidth = this.Size.Width;
					lstv_Commands.Width=textWidth;

					Point pt = qcTextEditor.GetPositionFromCharIndex(qcTextEditor.SelectionStart);
					pt.Y += qcTextEditor.Font.Height;

					if(pt.Y + lstv_Commands.Height > formHeight)
						pt.Y = pt.Y- (lstv_Commands.Height+(qcTextEditor.Font.Height/2));

					if(pt.X + lstv_Commands.Width > formWidth)
						pt.X = pt.X - lstv_Commands.Width;

					lstv_Commands.Location = pt;
						
					lstv_Commands.Visible = true;
					lstv_Commands.Focus();
					lstv_Commands.Items[0].Selected = true;	
				}
				
			}
			catch{return;}
		}

		private string ParseHeaderComment()
		{
			string header = "";
			string[] s = qcTextEditor.Text.Split(null);
			bool objectNameIsSet = false;	
			bool parametersIdetified = false;	
			string objectName ="";
			int pos;
			ArrayList objectParameters = new ArrayList();
			ArrayList words = new ArrayList();
			for(int i=0;i<s.Length;i++)
			{
				if ( s[i] == "" ) 
					continue ;
				else
					words.Add(s[i]);
			}

			for(int i=0;i<words.Count;i++)
			{
				
				if(words[i].ToString().ToUpper() == "PROCEDURE" || words[i].ToString().ToUpper() == "FUNCTION" || words[i].ToString().ToUpper() == "VIEW")
				{
					if(words[i-1].ToString().ToUpper() == "CREATE" || words[i-1].ToString().ToUpper() == "ALTER")
					{
						objectName = words[i+1].ToString();

						if(objectName.IndexOf("(") > -1)
							objectName = objectName.Substring(0,objectName.IndexOf("("));

						objectNameIsSet = true;
					}
				}
				pos = words[i].ToString().IndexOf("@");
				if(pos>-1 && objectNameIsSet)
				{
					objectParameters.Add(words[i].ToString().Substring(pos,words[i].ToString().Length -pos));
					parametersIdetified = true;
				}
				else if(parametersIdetified && words[i].ToString().ToUpper().IndexOf("AS")>-1)
					break;
				
			}


			if(qcTextEditor.Text.IndexOf("<member")==-1)
			{
				header += "/*******************************************************************************\n";
				header += "<member name='" + objectName + "'>\n";
				header += "\t<summary></summary>\n";
				header += "\t<revision author='" + SystemInformation.UserName.ToString()  + "' date='" + DateTime.Now.ToString() + "'>Created</revision>\n";

				for(int i=0;i<objectParameters.Count;i++)
					header += "\t<param name='" + objectParameters[i].ToString() + "'></param>\n";
				header += "</member>\n********************************************************************************/\n";
			}
			return header;
			
		}

		private void CopyEmbeddedResource(string resource, string filepath)
		{
			System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
			System.IO.Stream str = a.GetManifestResourceStream(resource);
			System.IO.StreamReader reader = new StreamReader(str);
			System.IO.FileStream fileStream = new FileStream(filepath,System.IO.FileMode.Create);
			System.IO.StreamWriter writer = new StreamWriter(fileStream);
			writer.WriteLine(reader.ReadToEnd());
			writer.Close();
		}
		private void CollectAliases(WordAndPosition[] word, int count)
		{
			for(int i=0;i<count;i++)
			{
				if((word[i].Word.ToUpper()=="JOIN" || word[i].Word.ToUpper()=="FROM"))
				{
					if(word[i+3].Word == null)
						return;
					if(!syntaxReader.IsReservedWord(word[i+2].Word) && word[i+2].Word.Length>0)
					{
						//Alias Found
						if(!Aliases.Contains(word[i+2].Word))
						{
							Aliases.Add(word[i+2].Word,word[i+1].Word);
							AliasList.Add(new Alias(word[i+2].Word,word[i+1].Word));
						}
						i=i+2;
					}
				}
			}
		}

		private int CollectAliases()
		{
			AliasList.Clear();
			Aliases.Clear();
			
			string s = qcTextEditor.Text;
			WordAndPosition[] buffer = new WordAndPosition[10000];
			int count = 0;
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch()) 
			{
				buffer[count].Word = m.Value;
				buffer[count].Position = m.Index;
				buffer[count].Length = m.Length;
				count++;
			}

			CollectAliases(buffer, count);
			return count;
		}
		
		private XmlDocument GetRecentObjects()
		{
			ArrayList dateList = new ArrayList();
			Hashtable objectList = new Hashtable();

			if(!File.Exists(Application.StartupPath+"\\RecentObjects.xml"))
			{
				CopyEmbeddedResource("QueryCommander.Embedded.RecentObjects.xml", Application.StartupPath + "\\RecentObjects.xml");
			}
			XmlDocument doc = new XmlDocument();
			doc.Load(Application.StartupPath + "\\RecentObjects.xml");
			XmlNodeList rootNodeList = doc.GetElementsByTagName("recentobjects");

			XmlNodeList nl = doc.GetElementsByTagName("objectName");
			
			foreach(XmlNode n in nl)
			{
				dateList.Add(Convert.ToDateTime( n.Attributes["changedate"].Value));
				objectList.Add(Convert.ToDateTime( n.Attributes["changedate"].Value),n);
			}
			dateList.Sort(new DateTimeReverserClass());
			rootNodeList[0].RemoveAll();

			for(int i=0;i<dateList.Count;i++)
			{
				if(i>10)
					break;
				XmlNode newNode = (XmlNode)objectList[dateList[i]];
				rootNodeList[0].AppendChild(newNode);
			}
			return doc;
		}
	
		private void SetCurrentStatement()
		{
			try
			{
				SQL.SQLStatement statement = new QueryCommander.SQL.SQLStatement(qcTextEditor.Text,qcTextEditor.SelectionStart,SQL.SQLStatement.SearchOrder.asc);
				string s = statement.Statement;
				int start = qcTextEditor.Text.IndexOf(s);
				qcTextEditor.Select(start,s.Length);
			}
			catch{throw;}
		}
		private int FindFirstNoneCommentedOccurance(string word, string text, int startPos)
		{
			int pos = text.IndexOf(word,startPos);
			if(!IsPositionCommented(pos,text))
				return pos;
			else 
				return FindFirstNoneCommentedOccurance(word,text,pos+word.Length);

		}
		private bool IsPositionCommented(int pos,string text)
		{
			try
			{
				string pat=@"/\*(.|[\r\n])*?\*/ |--.*";
				Regex r = new Regex(pat,RegexOptions.IgnorePatternWhitespace);

				Match m;

				for (m = r.Match(text); m.Success ; m = m.NextMatch())
				{
					if(m.Index<pos && (m.Length+m.Index)>pos)
						return true;
					if(m.Index>pos)
						return false;
				}

				return false;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		#endregion
		#region Public Methods
		public void RefreshLineRangeColor(int firstLine, int toLine)
		{
			//qcTextEditor.SetLineRangeColor(firstLine,toLine);
		}
		public void SendToOutPutWindow()
		{
			if(_outPutContainer==null)
				return;

			MainForm frm = (MainForm)MdiParentForm;

			if( _outPutContainer.dataset.Tables.Count >0)
			{
				if( _outPutContainer.dataset.Tables.Count >1)
					frm.OutputWindow.BrowseTable(_outPutContainer.dataset,_outPutContainer.database.DataAdapter);
				else
					frm.OutputWindow.BrowseTable(_outPutContainer.dataset.Tables[0],_outPutContainer.database.DataAdapter);
			}
			else
				frm.OutputWindow.tabControl1.TabPages.Clear();
			
			frm.OutputWindow.AddMessage(_outPutContainer.message);
			frm.statusBar.Panels[3].Text = _outPutContainer.statusText;
			frm.OutputWindow.Activate();
			frm.TaskList.ApplyTask("Query executed successfully");

		}
		
		/// <summary>
		/// Openeds a new query window displaing the requested constructor (alter statement) 
		/// </summary>
		public void GoToDefenition()
		{
			this.Cursor=Cursors.WaitCursor;
			string objectName = qcTextEditor.GetCurrentWord();
			objectName = objectName.Substring(objectName.IndexOf(".")+1);
			if(objectName.Length==0)
			{
				MessageBox.Show("The referenced object was not found","Go to reference",MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Cursor=Cursors.Default;
				return;
			}
			MainForm frm = (MainForm)MdiParentForm;
			frm.m_FrmDBObjects.CreateConstructorString(objectName);
			this.Cursor=Cursors.Default;
		}
		/// <summary>
		/// Displays all database objects referencing selected object 
		/// </summary>
		public void GoToReference()
		{
			this.Cursor=Cursors.WaitCursor;
			string objectName = qcTextEditor.GetCurrentWord();
			if(objectName.Length==0)
			{
				MessageBox.Show("The referenced object was not found","Go to reference",MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Cursor=Cursors.Default;
				return;
			}

			IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
			//Database db = new Database();
			ArrayList DatabaseObjects = db.GetDatabasesReferencedObjects(DatabaseName,objectName,dbConnection);

			FrmReferenceObjects frmReferenceObjects = new FrmReferenceObjects(DatabaseObjects,objectName);
			this.Cursor=Cursors.Default;
			if(frmReferenceObjects.ShowDialogWindow(this) ==DialogResult.OK)
			{
				MainForm frm = (MainForm)MdiParentForm;
				frm.m_FrmDBObjects.CreateConstructorString(frmReferenceObjects.ReferencedObject);
			}
			this.Cursor=Cursors.Default;
		}
		/// <summary>
		/// Searches for specified pattern. Called from FrmSearch
		/// </summary>
		/// <param name="pathern"></param>
		/// <param name="startPos"></param>
		/// <param name="richTextBoxFinds"></param>
		/// <returns></returns>
		public int Find(Regex regex, int startPos)
		{	
			string context= this.qcTextEditor.Text.Substring(startPos);

			Match m = regex.Match(context);
			if(!m.Success)
			{	
				MessageBox.Show("The specified text was not found.","QueryCommander",MessageBoxButtons.OK, MessageBoxIcon.Information);
				return 0;
			}

			int line=qcTextEditor.Document.GetLineNumberForOffset(m.Index+startPos);
			qcTextEditor.ActiveTextAreaControl.TextArea.ScrollTo(line);

			qcTextEditor.Select(m.Index+startPos,m.Length);
			_findNextRegex=regex;
			_findNextStartPos=m.Index+startPos;

			return m.Index+m.Length+startPos;

		}
		public void FindNext()
		{
			if(_findNextRegex!=null)
				Find(_findNextRegex,_findNextStartPos+1);
		}
		/// <summary>
		/// Searches for specified pattern and replaces it. Called from FrmSearch
		/// </summary>
		/// <param name="pathern"></param>
		/// <param name="startPos"></param>
		/// <param name="richTextBoxFinds"></param>
		/// <returns></returns>
		public int Replace(Regex regex, int startPos, string replaceWith)
		{	
			if(qcTextEditor.SelectedText.Length>0)
			{
				int start=qcTextEditor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
				//int start = qcTextEditor.SelectionStart;
				int length = qcTextEditor.SelectedText.Length;
				qcTextEditor.Document.Replace(start, length,replaceWith);
				
				return Find(regex,length+start);

			}

			string context= this.qcTextEditor.Text.Substring(startPos);

			Match m = regex.Match(context);
			
			if(!m.Success)
			{	
				MessageBox.Show("The specified text was not found.","QueryCommander",MessageBoxButtons.OK, MessageBoxIcon.Information);
				return 0;
			}
			qcTextEditor.Document.Replace(m.Index+startPos, m.Length,replaceWith);
			
			return m.Index+m.Length+startPos;

		}
		public void ReplaceAll(Regex regex, string replaceWith)
		{	
			string context= this.qcTextEditor.Text;

			Match m;// = regex.Match(context);
			
			for (m = regex.Match(context); m.Success ; m = m.NextMatch()) 
			{	
				qcTextEditor.Document.Replace(m.Index, m.Length,replaceWith);
			}
		}
		/// <summary>
		/// Calls FrmGotoLine
		/// </summary>
		public void GoToLine()
		{
			int lineNumber = qcTextEditor.GetLineFromCharIndex(qcTextEditor.SelectionStart);
			FrmGotoLine frmGotoLine = new FrmGotoLine(this,lineNumber,qcTextEditor.Document.LineSegmentCollection.Count);
			frmGotoLine.Show();
		}
		/// <summary>
		/// Sets cursor to requested line
		/// </summary>
		/// <param name="line"></param>
		public void GoToLine(int line)
		{
			int offset = qcTextEditor.Document.GetLineSegment(line).Offset;
			int length = qcTextEditor.Document.GetLineSegment(line).Length;
			qcTextEditor.ActiveTextAreaControl.TextArea.ScrollTo(line);
			qcTextEditor.Select(offset,length);
		}
		
		/// <summary>
		/// Returns the name of requested alia
		/// </summary>
		/// <param name="alias"></param>
		/// <returns></returns>
		public string GetAliasTableName(string alias)
		{
			SQL.SQLStatement statement = new QueryCommander.SQL.SQLStatement(qcTextEditor.Text,qcTextEditor.SelectionStart,SQL.SQLStatement.SearchOrder.asc);
			return statement.GetAliasTableName(alias);
		}
		/// <summary>
		/// Sets [dbConnection] and [DatabaseName]
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="conn"></param>
		public void SetDatabaseConnection(string dbName, IDbConnection conn)
		{
			this.Text = _OrginalName + " [" + dbName + "]"; 
			dbConnection = conn;
			//conn.ChangeDatabase(dbName);

			if(conn is SqlConnection)
			{
				try{conn.ChangeDatabase(dbName);}
				catch
				{
					conn = new SqlConnection(conn.ConnectionString);
					conn.Open();
					conn.ChangeDatabase(dbName);
				}
				DatabaseName = dbName;
				qcTextEditor.SetHighLightingStragegy("SQL");
			}
			else
			{
				DatabaseName = conn.Database;
				qcTextEditor.SetHighLightingStragegy("ORACLESQL");
			}

			
			MainForm frm = (MainForm)MdiParentForm;
			frm.SetPandelInfo("dbConnection.DataSource", dbConnection.Database);
		}

		/// <summary>
		/// This is where it hapends...
		/// </summary>
		public void RunQuery()
		{
			MainForm frm = (MainForm)MdiParentForm;
			
			string msg="";
			this.Cursor = Cursors.WaitCursor;
			string SQLstring;
			string rowCountString="";
			TimeSpan executionTime;
			DataSet ds =null;

			// VSS
			if(IsCheckedOutBuOtherUser())
			{
				MessageBox.Show("Object is checked out by other user", "Source Control");
				return;
			}
			// Plugin
			ExecutePlugin(Common.TriggerTypes.OnBeforeQueryExecution,new QueryCommander.PlugIn.Core.CallContext(dbConnection,qcTextEditor.Text,null));

			frm.statusBar.Panels[3].Text="Executing query...";

			try
			{
				// Handling InfoMessages
				qcTextEditor.ClearInfoMessages();
				
				/// Handling Comment header
				ComplementHeader();
				
				frm.OutputWindow.Text = "Output";
				
				// Resets exception underlining
				if(qcTextEditor.SelectedText.Length>1)
				{
					SQLstring = qcTextEditor.SelectedText;
//					qcTextEditor.SetSelectionUnderlineStyle( QCRichEditor.UnderlineStyle.None );
//					qcTextEditor.SetSelectionUnderlineColor( QCRichEditor.UnderlineColor.White );
				}
				else
				{
					SQLstring = qcTextEditor.Text;

//					qcTextEditor.SelectAll();
//					qcTextEditor.SetSelectionUnderlineStyle( QCRichEditor.UnderlineStyle.None );
//					qcTextEditor.SetSelectionUnderlineColor( QCRichEditor.UnderlineColor.White );
				}

				IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
//				Database db = new Database();

				/********************************************
					* The Query is expected to return a dataset 
					********************************************/

				// RunWithIOStatistics hits the user about none efficient queries

				QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
				if(settings.Exists())
				{
					if(settings.RunWithIOStatistics)
						SQLstring = "SET STATISTICS IO ON\n" + SQLstring + "\nSET STATISTICS IO OFF";
				}
				
				DateTime dt = DateTime.Now;
	
				ds = db.ExecuteCommand_DataSet (SQLstring, dbConnection,DatabaseName);

				executionTime = DateTime.Now.Subtract(dt);

				if(ds != null)
				{
					// Handling InfoMessages
					if(_sqlInfoMessages.Count>0)
					{
						if(this.syntaxReader.RunWithIOStatistics)
							msg+="The option [Run Query with IO statistics] is set to on.\n";
						
						foreach (SqlError err in _sqlInfoMessages)
						{
							if(err.Message.IndexOf("Changed database context")>-1)
								continue;

							if(err.Message.IndexOf("###")>-1)
							{
								rowCountString="Rowcount: " + err.Message.Substring(3) + "\tExecutiontime:" + executionTime.Hours.ToString() + ":" + executionTime.Seconds.ToString()+ ":" +executionTime.Milliseconds.ToString();
								msg+=rowCountString;
								continue;
							}
							msg+=err.Message.Replace("\n"," ") + "\n";

							if(ds.Tables.Count==0)
								continue;

							int rowCount = ds.Tables[0].Rows.Count;
							// Sample: Table 'Employee'. Scan count 7, logical reads 14, physical reads 0, read-ahead reads 0.
							
							if(err.Message.IndexOf("Table ") > -1 && err.Message.IndexOf("Scan count ")>0)
							{
								int start = err.Message.IndexOf("Scan count ")+11;
								string s = err.Message.Substring(start,err.Message.IndexOf(",")-start);
								int scanCount = Convert.ToInt16(s);
								double percent = (Convert.ToDouble( scanCount)/Convert.ToDouble(rowCount))*100.0;
								if(percent >= Convert.ToDouble( this.syntaxReader.DifferencialPercentage))
								{
					
									string tableName = err.Message.Substring(7,err.Message.IndexOf("'",7)-7);
									int charIndex = qcTextEditor.GetCharIndexForTableDefenition(tableName);
									Point p = qcTextEditor.GetPositionFromCharIndex(charIndex+tableName.Length);
									p.Y = p.Y + 2;
									int bottonY = qcTextEditor.GetPositionFromCharIndex(qcTextEditor.Text.Length).Y;
									double percentPositionFormTop = Convert.ToDouble( p.Y) / Convert.ToDouble(bottonY);

									if(percent >= this.syntaxReader.DifferencialPercentage+50)
										qcTextEditor.AddInfoMessages(p,QueryCommander.Editor.TextEditorControlWrapper.InfoMessage.MessageType.Warning,percentPositionFormTop,
											"Scan count exceed " + percent.ToString() + "%\n" + err.Message);
									else
										qcTextEditor.AddInfoMessages(p,QueryCommander.Editor.TextEditorControlWrapper.InfoMessage.MessageType.Info,percentPositionFormTop,
											"Scan count exceed " + percent.ToString() + "%\n" + err.Message);
	
								}
							}
						}
						_sqlInfoMessages.Clear();
					}
					frm.statusBar.Panels[3].Text="";
					frm.TaskList.Visible = false;

					if(rowCountString=="" && ds.Tables.Count>0)
						rowCountString="Rowcount: " + ds.Tables[0].Rows.Count.ToString() + "\tExecutiontime:" + executionTime.Hours.ToString() + ":" + executionTime.Seconds.ToString()+ ":" +executionTime.Milliseconds.ToString();
								
					// Sends the result to the output window
					_outPutContainer=new OutPutContainer(ds,db,msg,executionTime,true,rowCountString);
					SendToOutPutWindow();
				}
			}
			catch(Exception ex)
			{
				string error = ex.Message;
				
				if(error=="Database cannot be null, the empty string, or string of only whitespace.")
					error += "\nRight click on a database node in the Microsoft SQL Servers window to the left. Click [Use database].";

				frm.OutputWindow.Text = "Output";
				this.Cursor = Cursors.Default;
				frm.ShowTaskWindow();
				if(ex is System.Xml.XmlException)
				{
					frm.TaskList.ApplyTask(msg + "\n\n" +"XML Exception message\n" + error);
				}
				else
					frm.TaskList.ApplyTask(msg + "\n\n" +"Server message\n" + error);

				frm.TaskList.Activate();
				frm.statusBar.Panels[3].Text="";

				if(ex.Message.IndexOf("Line")>-1)
				{
					try
					{
						int start=ex.Message.IndexOf("Line")+4;
						int length = ex.Message.IndexOf(":",start)-start;
						string line = ex.Message.Substring(start,length);
						int l = Convert.ToInt32(line);
//						qcTextEditor.SelectLine(l);
//						qcTextEditor.SetSelectionUnderlineStyle( QCRichEditor.UnderlineStyle.Wave );
//						qcTextEditor.SetSelectionUnderlineColor( QCRichEditor.UnderlineColor.Red );

					}
					catch
					{
						return;	
					}
				}
				
			}
			this.Cursor = Cursors.Default;

			//Plugin
			ExecutePlugin(Common.TriggerTypes.OnAfterQueryExecution,new QueryCommander.PlugIn.Core.CallContext(dbConnection,qcTextEditor.Text,ds));
			
		}
		
		/// <summary>
		/// Selects current line before calling RunQuery
		/// </summary>
		public void RunQueryLine()
		{
			Point pt = qcTextEditor.GetPositionFromCharIndex(qcTextEditor.SelectionStart);
			pt.X=0;
			int lineStartPosition = qcTextEditor.GetCharIndexFromPosition(pt);
			int lineEndPosition = qcTextEditor.Text.IndexOf("\n",lineStartPosition);
			if(lineEndPosition==-1)
				lineEndPosition=qcTextEditor.Text.Length;

			qcTextEditor.Select(lineStartPosition, lineEndPosition - lineStartPosition);
			RunQuery();
		}
		/// <summary>
		/// Selects current query before calling RunQuery
		/// </summary>
		public void RunCurrentQuery()
		{
			SetCurrentStatement();
			RunQuery();
		}
		/// <summary>
		/// Generates insert statements based on current query
		/// </summary>
		public void CreateInsertStatement()
		{
			qcTextEditor.SuspendLayout();
			string SQLstring;
			string Result = "";
			MainForm frm = (MainForm)MdiParentForm;
			try
			{
				if(qcTextEditor.SelectedText.Length>1)
					SQLstring = qcTextEditor.SelectedText;
				else
					SQLstring = qcTextEditor.Text;

				IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
//				Database db = new Database();
				Result = db.GetInsertStatements(SQLstring,dbConnection,DatabaseName);

				if(Result.Length>0)
				{
					frm.NewQueryform();

					frm.ActiveQueryForm.SetDatabaseConnection(this.DatabaseName, this.dbConnection);
					frm.ActiveQueryForm.Content = Result;
				}
				qcTextEditor.ResumeLayout();
			}
			catch(Exception ex)
			{
				frm.ShowTaskWindow();
				frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
				frm.TaskList.Activate();
			}
		}

		/// <summary>
		/// Generates update statements based on current query
		/// </summary>
		public void CreateUpdateStatement()
		{
			qcTextEditor.SuspendLayout();
			string SQLstring;
			string Result = "";
			MainForm frm = (MainForm)MdiParentForm;
			try
			{
				if(qcTextEditor.SelectedText.Length>1)
					SQLstring = qcTextEditor.SelectedText;
				else
					SQLstring = qcTextEditor.Text;

				IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
//				Database db = new Database();
				Result = db.GetUpdateStatements(SQLstring,dbConnection,DatabaseName);

				if(Result.Length>0)
				{
					frm.NewQueryform();

					frm.ActiveQueryForm.SetDatabaseConnection(this.DatabaseName, this.dbConnection);
					frm.ActiveQueryForm.Content = Result;
				}
				qcTextEditor.ResumeLayout();
			}
			catch(Exception ex)
			{
				frm.ShowTaskWindow();
				frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
				frm.TaskList.Activate();
			}
		}

		/// <summary>
		/// Undo next action in the undo buffer
		/// </summary>
		public void Undo()
		{
			qcTextEditor.UndoAction();
		}
		/// <summary>
		/// 
		/// </summary>
		public void Paste()
		{
			
			qcTextEditor.Paste();
			
		}
		/// <summary>
		/// 
		/// </summary>
		public void Copy()
		{
			qcTextEditor.Copy();
		}
		public void Cut()
		{
			qcTextEditor.Cut();
		}
		/// <summary>
		/// Adds a Comment header
		/// </summary>
		public void InsertHeader()
		{
			qcTextEditor.SuspendLayout();
			string header = ParseHeaderComment();
			qcTextEditor.Text = header+qcTextEditor.Text; 
			qcTextEditor.ResumeLayout();
		}
		/// <summary>
		/// Alters the comment header with a revision tag
		/// </summary>
		public void AddRevisionCommentSection()
		{
			int startpos = qcTextEditor.Text.IndexOf("</member>",0);
			if(startpos<1)
				return;
			startpos = qcTextEditor.Text.LastIndexOf("</revision>") + 11;
			qcTextEditor.Text = qcTextEditor.Text.Substring(0,startpos) + "\n\t<revision author='" + SystemInformation.UserName.ToString()  + "' date='" + DateTime.Now.ToString() + "'>Altered</revision>" + qcTextEditor.Text.Substring(startpos);
			qcTextEditor.Refresh();
		
		}
		
		/// <summary>
		/// Consolidates all xml comment headers 
		/// </summary>
		public void GetXmlDocFile()
		{
			string whereConcitions="";
			FrmAlterDocumentationOutput frm = new FrmAlterDocumentationOutput();
			frm.ShowDialog(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				if(frm.chbView.Checked)
					whereConcitions="'V'";
				if(frm.chbSP.Checked)
					if(whereConcitions.Length>0)
						whereConcitions+=",'P'";
					else
						whereConcitions="'P'";
				if(frm.chbFn.Checked)
					if(whereConcitions.Length>0)
						whereConcitions+=",'FN','TF'";
					else
						whereConcitions="'FN','TF'";
			}
			
			this.Cursor = Cursors.WaitCursor;
			string doc = "<?xml version='1.0' encoding='UTF-8'?>\n<!-- Generated by QueryCommander-->\n<?xml-stylesheet type='text/xsl' href='doc.xsl'?>\n<members>\n";
			XmlDocument xmlDoc = new XmlDocument();	
			try
			{
				IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
//				Database db = new Database();
				doc += db.GetXmlDoc(DatabaseName,dbConnection,whereConcitions) + "\n</members>";
				
				xmlDoc.LoadXml(doc);
			}
			catch(Exception ex)
			{
				int startpos = ex.Message.IndexOf("Line ") + 5;
				int endpos = ex.Message.IndexOf(",",startpos);
				int line = Convert.ToInt16( ex.Message.Substring(startpos, endpos-startpos));
				FrmXMLError frmXMLError = new FrmXMLError(ex.Message, doc,line);
				frmXMLError.ShowDialog(this);
				return;
			}


			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.AddExtension=true;
			saveFileDialog.DefaultExt="xml";
			saveFileDialog.FileName = DatabaseName + " Procedures";
			saveFileDialog.Title = "Save Documentation file";
			saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"  ;

			DialogResult result = saveFileDialog.ShowDialog();
			if(result == DialogResult.OK) 
			{
				string saveFileName = saveFileDialog.FileName;
				try
				{
					xmlDoc.Save(saveFileName);

				} 
				catch(Exception exp)
				{
					MessageBox.Show("An error occurred while attempting to save the file. The error is:" 
						+ System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
					return;
				}
				string xslPath = saveFileName.Substring(0,saveFileName.LastIndexOf("\\"));
				CopyEmbeddedResource("QueryCommander.Embedded.doc.xsl", xslPath + "\\doc.xsl");

				System.Diagnostics.Process.Start("IExplore.exe",saveFileName);


				this.Cursor = Cursors.Default;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetCurrentWord()
		{
			return qcTextEditor.GetCurrentWord();
		}
		/// <summary>
		/// Save query statement to file
		/// </summary>
		/// <param name="path"></param>
		public void SaveAs(string path)
		{
			qcTextEditor.SaveFile(path);//, RichTextBoxStreamType.PlainText);
			FileName = path;
		}
		
		/// <summary>
		/// Gives the user the option to alter the header
		/// </summary>
		public void ComplementHeader()
		{
			string header="";
			try
			{
				QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
				if(settings.Exists())
				{
					if(!settings.ShowFrmDocumentHeader)
						return;
				}
				int start = qcTextEditor.Text.IndexOf("<member");
				int end = qcTextEditor.Text.IndexOf("</member>");
				if(start>-1 && end >-1)
				{
					end += 9; //Add length of </member>
					header = qcTextEditor.Text.Substring(start, end-start);
					FrmDocumentHeader frm = new FrmDocumentHeader(header);
					if(frm.ShowDialogWindow(this)==DialogResult.OK)
					{
						qcTextEditor.Text = qcTextEditor.Text.Replace(header,frm.Header);
						qcTextEditor.Refresh();
						
					}
					XmlDocument _doc = new XmlDocument();
					_doc.LoadXml(header);
					XmlNodeList nList =  _doc.GetElementsByTagName("member");
					XmlNode n = nList[0].Attributes.GetNamedItem("name");
				}
			}
			catch(System.Xml.XmlException ex)
			{
				int pos=0;
				for(int i=0;i<ex.LineNumber-1;i++)
				{
					pos = header.IndexOf("\n",pos+1);
				}

				string lineText = header.Substring(pos,ex.LinePosition) + "<-\n\n\tMake sure the text in well formated\n\nref: http://www.w3c.org\n";
				
				XmlException xmlEx = new XmlException(ex.Message + "\n" + lineText,ex.InnerException,ex.LineNumber,ex.LinePosition);
				
				throw xmlEx;
			}

		}

		/// <summary>
		/// Calls MainForm.PopulateRecentItems
		/// </summary>
		/// <param name="objectName"></param>
		public void AddToRecentObjects(string objectName)
		{
			if(objectName.Trim().Length==0)
				return;

			bool nodeExists=false;
			
			try
			{
				XmlDocument doc = GetRecentObjects();

				XmlNodeList rootNodeList = doc.GetElementsByTagName("recentobjects");

				XmlNodeList nl = doc.GetElementsByTagName("objectName");
			
				foreach(XmlNode n in nl)
				{
					if(n.InnerText==objectName)
					{
						n.Attributes["changedate"].Value = DateTime.Now.ToString();
						nodeExists =true;
						break;
					}
				}

				if(!nodeExists)
				{
					XmlElement xmlelem=doc.CreateElement("","objectName","");
					XmlText xmltext=doc.CreateTextNode(objectName);
					xmlelem.AppendChild(xmltext);
					xmlelem.SetAttribute("changedate",  DateTime.Now.ToString() );
					doc.ChildNodes.Item(1).AppendChild(xmlelem);
					XmlElement elem = doc.CreateElement("objectName");
					elem.SetAttribute("changedate",  DateTime.Now.ToLongTimeString());
				}
			
				doc.Save(Application.StartupPath + "\\RecentObjects.xml");
				MainForm frm = (MainForm)MdiParentForm;
				frm.PopulateRecentItems();
			}
			catch
			{
				throw;
			}
		
		}

		/// <summary>
		/// xml2Data - CreateTablesScript
		/// </summary>
		public void GetCreateTablesScriptFromXMLFile()
		{
			try
			{
				FrmChooseXMLFile frm = new FrmChooseXMLFile();
				frm.rbData.Checked=false;
				frm.rbStructure.Checked=true;

				if(frm.ShowDialogWindow(this)==DialogResult.OK)
				{
					string file = frm.FileName;
					bool createKeys = frm.CreateKeys;
					XmlTextReader reader = new XmlTextReader(file);
					XMLDatabase xmlDatabase = new XMLDatabase(reader);
					string script="";

					if(frm.rbStructure.Checked)
					{
						script = xmlDatabase.GetDatabaseSQLScript(createKeys);
					}
					else
					{
						script = xmlDatabase.GetInsertScript(createKeys);
					}

					this.Content = script;
				}
			}
			catch(Exception ex)
			{
				if(ex.Message=="XmlContainsAttributes")
				{
					MessageBox.Show("QueryCommander XML-import only support XmlElement in this version\n\nSample:\n<PERSON>\n\t<FIRSTNAME>John</FIRSTNAME>\n\t<LASTNAME>Smith</LASTNAME>\n</PERSON>","XMLAttributes not supported",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				throw;
			}
		}
		/// <summary>
		/// xml2Data - Import data
		/// </summary>
		public void GetInsertScriptFromXMLFile()
		{
			try
			{
				FrmChooseXMLFile frm = new FrmChooseXMLFile();
				frm.rbData.Checked=true;
				frm.rbStructure.Checked=false;

				if(frm.ShowDialogWindow(this)==DialogResult.OK)
				{
					string file = frm.FileName;
					bool createKeys = frm.CreateKeys;
					XmlTextReader reader = new XmlTextReader(file);
					XMLDatabase xmlDatabase = new XMLDatabase(reader);
					string script="";

					if(frm.rbStructure.Checked)
					{
						script = xmlDatabase.GetDatabaseSQLScript(createKeys);
					}
					else
					{
						script = xmlDatabase.GetInsertScript(createKeys);
					}

					this.Content = script;
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}
		
		/// <summary>
		/// Calls Plug-in method
		/// </summary>
		/// <param name="triggerType"></param>
		/// <param name="callContext"></param>
		public void ExecutePlugin(Common.TriggerTypes triggerType, QueryCommander.PlugIn.Core.CallContext callContext)
		{
			try
			{
				string ret=null;
				MainForm frm = (MainForm)MdiParentForm;
				foreach(string fileName in frm.Plugins)
				{
					if(fileName.IndexOf("Interop.")>-1)
						continue;

					if(IsTriggerType(fileName,triggerType))
					{
						object obj = GetTriggerTypeObject(fileName,triggerType);
						PropertyInfo property = obj.GetType().GetProperty("ExecutionType");
						Common.ExecutionTypes executionType = (Common.ExecutionTypes)property.GetValue(obj,null);
	
						switch(triggerType)
						{
							case Common.TriggerTypes.OnBeforeQueryExecution:
								ret = (string)((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnBeforeQueryExecution1)obj).Execute(callContext,this.Handle,((MainForm)MdiParentForm).plugInVariables);
								break;
							case Common.TriggerTypes.OnAfterQueryExecution:
								ret = (string)((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnAfterQueryExecution1)obj).Execute(callContext,this.Handle,((MainForm)MdiParentForm).plugInVariables);
								break;
							default:
								return;
						}
						
						switch(executionType)
						{
							case Common.ExecutionTypes.InsertAtPoint:
								int pos = qcTextEditor.SelectionStart;
								qcTextEditor.SelectedText=(string)ret;
								break;
							case Common.ExecutionTypes.InsertToNewQueryWindow:
								frm.NewQueryform();
								frm.ActiveQueryForm.Content=(string)ret;
								break;
							case Common.ExecutionTypes.ReplaceToQueryWindow:
								this.Content = (string)ret;
								break;
						}
					}	
				}
			}
			catch(Exception ex)
			{
				return;
			}
		}
		/// <summary>
		/// Calls Plug-in method
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="menuItem"></param>
		
		public void ExecutePlugin(QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1 obj, MenuItem menuItem)
		{
			QueryCommander.PlugIn.Core.CallContext callContext = new QueryCommander.PlugIn.Core.CallContext(this.dbConnection,qcTextEditor.Text,null);

			object ret = obj.Execute(callContext,this.Handle,((MainForm)MdiParentForm).plugInVariables,menuItem);
			switch(obj.ExecutionType)
			{
				case Common.ExecutionTypes.InsertAtPoint:
					int pos = qcTextEditor.SelectionStart;
					qcTextEditor.SelectedText=(string)ret;
					break;
				case Common.ExecutionTypes.InsertToNewQueryWindow:
					MainForm frm = (MainForm)MdiParentForm;
					frm.NewQueryform();
					frm.ActiveQueryForm.Content=(string)ret;
					break;
				case Common.ExecutionTypes.ReplaceToQueryWindow:
					this.Content = (string)ret;
					break;
			}
		}
		#endregion
		#region Context menu
		private void miCopy_Click(object sender, System.EventArgs e)
		{
			this.Copy();
		}
		private void miCut_Click(object sender, System.EventArgs e)
		{
			this.qcTextEditor.Cut();
		}
		private void miPaste_Click(object sender, System.EventArgs e)
		{
			this.Paste();
		}
		private void miGoToDefinision_Click(object sender, System.EventArgs e)
		{
			this.GoToDefenition();
		}
		private void miGoToRererence_Click(object sender, System.EventArgs e)
		{
			this.GoToReference();
		}
		private void miOptions_Click(object sender, System.EventArgs e)
		{
			MainForm frm = (MainForm)MdiParentForm;
			
			FrmOption frmOption = new FrmOption(frm);
			frmOption.ShowDialog();

		}
		private void miAddToSnippet_Click(object sender, System.EventArgs e)
		{
			FrmAddToSnippet frm = new FrmAddToSnippet(qcTextEditor.Text);
			frm.ShowDialogWindow(this);

		}
		private void miSnippet_Click(object sender, System.EventArgs e)
		{
			string statement = ((SnippetMenuItem)sender).statement;

			statement = statement.Replace(@"\n","\n");
			statement = statement.Replace(@"\t","\t");

			int cursorPos = qcTextEditor.SelectionStart;

			if(statement.IndexOf("{}")>-1)
			{
				cursorPos = cursorPos + statement.IndexOf("{}");
				statement = statement.Replace("{}","");
			}
			qcTextEditor.SelectedText=statement;
			qcTextEditor.SelectionStart = cursorPos;
			qcTextEditor.Refresh();

			


		}
		
		private void miRunCurrentQuery_Click(object sender, System.EventArgs e)
		{
			this.RunCurrentQuery();
		}
		
		private void qcTextEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				string word = qcTextEditor.GetCurrentWord();
				IDataObject iData = Clipboard.GetDataObject();
				
				MenuItem miCopy = new MenuItem("&Copy");
				MenuItem miCut = new MenuItem("C&ut");
				MenuItem miPaste = new MenuItem("&Paste");
				MenuItem miSeparator = new MenuItem("-");
				MenuItem miGoToDefinision = new MenuItem("Go to &Definision");
				MenuItem miGoToRererence = new MenuItem("Go to &Reference");
				MenuItem miSeparator2 = new MenuItem("-");
				MenuItem miRunCurrentQuery = new MenuItem("Run &current query");
				MenuItem miSeparator3 = new MenuItem("-");
				MenuItem miOptions = new MenuItem("&Options");
				MenuItem miSeparator4 = new MenuItem("-");
				MenuItem miSnippets = new MenuItem("&Snippets");
				MenuItem miAddToSnippets = new MenuItem("&Add to snippets");

				// Events				
				miCopy.Click += new System.EventHandler(this.miCopy_Click);
				miCut.Click += new System.EventHandler(this.miCut_Click);
				miPaste.Click += new System.EventHandler(this.miPaste_Click);
				miGoToDefinision.Click += new System.EventHandler(this.miGoToDefinision_Click);
				miGoToRererence.Click += new System.EventHandler(this.miGoToRererence_Click);
				miRunCurrentQuery.Click += new System.EventHandler(this.miRunCurrentQuery_Click);
				miOptions.Click += new System.EventHandler(this.miOptions_Click);
				miAddToSnippets.Click += new System.EventHandler(this.miAddToSnippet_Click);

				if(!iData.GetDataPresent(DataFormats.Text))
					miPaste.Enabled=false;

				// Clear all previously added MenuItems.
				cmShortcutMeny.MenuItems.Clear();
 
				cmShortcutMeny.MenuItems.Add(miCopy);
				cmShortcutMeny.MenuItems.Add(miCut);
				cmShortcutMeny.MenuItems.Add(miPaste);
				cmShortcutMeny.MenuItems.Add(miSeparator);
				cmShortcutMeny.MenuItems.Add(miGoToDefinision);
				cmShortcutMeny.MenuItems.Add(miGoToRererence);
				cmShortcutMeny.MenuItems.Add(miSeparator2);
				cmShortcutMeny.MenuItems.Add(miRunCurrentQuery);
				cmShortcutMeny.MenuItems.Add(miSeparator3);
				cmShortcutMeny.MenuItems.Add(miOptions);
				cmShortcutMeny.MenuItems.Add(miSeparator4);
				cmShortcutMeny.MenuItems.Add(miSnippets);


				// Snippets
				XmlDocument xmlSnippets = new XmlDocument();
				xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
				XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

				if(qcTextEditor.SelectedText.Length>1)
					miSnippets.MenuItems.Add(miAddToSnippets);

				foreach(XmlNode node in xmlNodeList[0].ChildNodes)
				{
					SnippetMenuItem snippet = new SnippetMenuItem();
					snippet.Text = node.Attributes["name"].Value;
					snippet.statement = node.InnerText;
					snippet.Click+= new System.EventHandler(this.miSnippet_Click);

					miSnippets.MenuItems.Add(snippet);
				}

				cmShortcutMeny.Show(qcTextEditor,new Point(e.X,e.Y));
				
			}
		}
		#endregion
		#region Plugin
		/// <summary>
		/// Checks if assembly implements requested interface
		/// </summary>
		/// <param name="assemblyFileName"></param>
		/// <param name="triggerType"></param>
		/// <returns></returns>
		private bool IsTriggerType(string assemblyFileName,Common.TriggerTypes triggerType)
		{
			bool returnValue=false;
			Assembly assem = Assembly.LoadFile(assemblyFileName);
			Type[] tt = assem.GetTypes();
			
			for(int i=0;i<tt.Length;i++)
			{
				Common.TriggerTypes checkType=triggerType;
				Type type = assem.GetType(tt[i].FullName);
				object obj = Activator.CreateInstance(type);

				if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnAfterQueryExecution1)
					checkType = Common.TriggerTypes.OnAfterQueryExecution;
				else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnBeforeQueryExecution1)
					checkType = Common.TriggerTypes.OnBeforeQueryExecution;
				else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1)
					checkType = Common.TriggerTypes.OnMenuClick;
				else
					returnValue = false;

				if( checkType==triggerType)
					return true;
				else
					returnValue = false;
			}
			return returnValue;
		}

		/// <summary>
		/// Get the object implementing requeted interface
		/// </summary>
		/// <param name="assemblyFileName"></param>
		/// <param name="triggerType"></param>
		/// <returns></returns>
		private object GetTriggerTypeObject(string assemblyFileName,Common.TriggerTypes triggerType)
		{
			try
			{
				Assembly assem = Assembly.LoadFile(assemblyFileName);
				Type[] tt = assem.GetTypes();
			
				for(int i=0;i<tt.Length;i++)
				{
					Common.TriggerTypes checkType=triggerType;
					Type type = assem.GetType(tt[i].FullName);
				
					object obj = Activator.CreateInstance(type);
				
					if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnAfterQueryExecution1)
						checkType = Common.TriggerTypes.OnAfterQueryExecution;
					else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnBeforeQueryExecution1)
						checkType = Common.TriggerTypes.OnBeforeQueryExecution;
					else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1)
						checkType = Common.TriggerTypes.OnMenuClick;
				

					if( checkType==triggerType)
						return obj;

				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		
		private Common.ExecutionTypes GetReturnType(string assemblyFileName)
		{
			Assembly assem = Assembly.LoadFile(assemblyFileName);
			Type[] tt = assem.GetTypes();
			Type type = assem.GetType(tt[0].FullName);
			object obj = Activator.CreateInstance(type);
			PropertyInfo property = type.GetProperty("ExecutionType");
			object propetyValue = property.GetValue(obj,null);
			return (Common.ExecutionTypes)propetyValue;
		}
		
		private object LateBoundCall(
			string assemblyFileName, 
			QueryCommander.PlugIn.Core.CallContext callcontext,
			Common.TriggerTypes triggerType)
		{
			string methodName	= "Execute"; 
			Type type=null;
			Object obj=null;
			object[] parameters = new object[2]{callcontext,this.Handle}; 

			// Load the assembly to use.
			Assembly assem = Assembly.LoadFile(assemblyFileName);
			Type[] tt = assem.GetTypes();

			for(int ii=0;ii<tt.Length;ii++)
			{
				Common.TriggerTypes checkType=triggerType;
				type = assem.GetType(tt[ii].FullName);
				
				// Create an instance of the type.
				obj = Activator.CreateInstance(type);

				if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnAfterQueryExecution1)
					checkType = Common.TriggerTypes.OnAfterQueryExecution;
				else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnBeforeQueryExecution1)
					checkType = Common.TriggerTypes.OnBeforeQueryExecution;
				else if(obj is QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1)
					checkType = Common.TriggerTypes.OnMenuClick;

				if( checkType==triggerType)
					break;

			}
			// Invoke the method. 
			try
			{
				Type[] typeArray = new Type[(parameters == null ? 0 : parameters.Length)];
				if (typeArray.Length > 0) // only if we have more then 0 parameters
					for (int i = 0; i<typeArray.Length; i++) // Loop the parameters
						typeArray[i]=parameters[i].GetType(); // and get their types
				
				// Get the method, with the method signature stated in typeArray, to call from the type.
				MethodInfo method = type.GetMethod(methodName,typeArray);

				object ret =  method.Invoke(obj, parameters);
				if(ret!=null)
					return ret;
				else
					return null;

			}
			catch(Exception ex)
			{
				// If the inner parameter is not null , the current exception was raised as a result 
				// of the inner exception being thrown by a method invoked via reflection.
 
				// So this is done because the golfif exception is capsuleted in the innerExcpetion
				if(ex.InnerException !=null)
					throw(ex.InnerException);  
				else
					throw(ex);  
			}
		}
		
		private object LateBoundCall(
			string assemblyFileName, 
			QueryCommander.PlugIn.Core.CallContext callcontext)
		{
			string methodName	= "Execute"; 
			object[] parameters = new object[2]{callcontext,this.Handle}; 
			// Load the assembly to use.
			Assembly assem = Assembly.LoadFile(assemblyFileName);
			
			// Get the type to use from the assembly.
			Type[] tt = assem.GetTypes();
			Type type = assem.GetType(tt[0].FullName);
			
			// Create a Type array.
			Type[] typeArray = new Type[(parameters == null ? 0 : parameters.Length)];
			if (typeArray.Length > 0) // only if we have more then 0 parameters
				for (int i = 0; i<typeArray.Length; i++) // Loop the parameters
					typeArray[i]=parameters[i].GetType(); // and get their types
			
			// Get the method, with the method signature stated in typeArray, to call from the type.
			MethodInfo method = type.GetMethod(methodName,typeArray);
			// Create an instance of the type.
			Object obj = Activator.CreateInstance(type);
			
			// Invoke the method. 
			try
			{
				object ret =  method.Invoke(obj, parameters);
				if(ret!=null)
					return ret;
				else
					return null;

			}
			catch(Exception ex)
			{
				// If the inner parameter is not null , the current exception was raised as a result 
				// of the inner exception being thrown by a method invoked via reflection.
 
				// So this is done because the golfif exception is capsuleted in the innerExcpetion
				if(ex.InnerException !=null)
					throw(ex.InnerException);  
				else
					throw(ex);  
			}
		}

		private void AddPluginMenuItems()
		{
			MainForm frm = (MainForm)MdiParentForm;

			foreach(string fileName in frm.Plugins)
			{
				if(fileName.IndexOf("Interop.")>-1)
					continue;

				object obj = GetTriggerTypeObject(fileName,Common.TriggerTypes.OnMenuClick);
				bool exists = false;
				if(obj!=null)
				{
					Assembly assem = Assembly.LoadFile(fileName);
					Type[] tt = assem.GetTypes();
					Type type = assem.GetType(tt[0].FullName);
					foreach(MenuItem mi in frm.menuItem_Plugins.MenuItems)
						if(mi.Text==((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1)obj).MenuItem.Text)
						{
							exists=true;
							break;
						}

					if(!exists)
						frm.AddPluginMenuItem( ((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1)obj).MenuItem,obj);
				}	
			}	
		}
		
		#endregion
		#region Classes	
		/// <summary>
		/// Custom MenuItem used for snippets
		/// </summary>
		public class SnippetMenuItem:MenuItem
		{
			public string statement ="";
		}
		public class DateTimeReverserClass : IComparer  
		{
			// Calls CaseInsensitiveComparer.Compare with the parameters reversed.
			int IComparer.Compare( Object x, Object y )  
			{
				DateTime dx = (DateTime)x;
				DateTime dy = (DateTime)y;
				if(dx>dy)
					return -1;
				else
					return 1;
			}
		}
		private class Alias
		{
			public Alias(string alias, string table)
			{
				AliasName = alias;
				TableName = table;

			}

			public string AliasName;
			public string TableName;
		}
		private class OutPutContainer
		{
			public OutPutContainer(DataSet dataset, IDatabaseManager database, string message, TimeSpan executionTime,bool query, string statusText)
			{
				this.dataset=dataset;
				this.database=database;
				this.message=message;
				this.executionTime=executionTime;
				this.query=query;
				this.statusText=statusText;
			}
			public DataSet dataset;
			public IDatabaseManager database;
			public string message;
			public TimeSpan executionTime;
			public string statusText;
			bool	query;
		}
		#endregion		
	}
}












