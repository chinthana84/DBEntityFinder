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
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.IO;
using WeifenLuo.WinFormsUI;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.Win32;
using QueryCommander.PlugIn.Core.Interfaces;
using QueryCommander.VSS;
using QueryCommander.Database;
using QueryCommander.General.WorkSpace;
using SharedLayer;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
 
		#region Members
		AsyncMethods asyncMethods = new AsyncMethods();
		IAsyncResult StartPageResult = null;
		GetXmlStartPageDelegate getXmlStartPageDelegate = null;
		XmlDocument xmlStartPage=new XmlDocument();
		FrmSearch frmSearch;
		
		public RichTextBoxFinds richTextBoxFinds = RichTextBoxFinds.None;
		public string searchValue;	
		public int lastSearchPos=0;
		public QueryCommander.VSS.VSSConnectionCollection vssConnectionCollection = null;//new QueryCommander.VSS.VSSConnectionCollection();
		public WorkSpaceCollection workSpaceCollection=null;

	 	private string StartUpFile = Application.StartupPath + @"\startpage.xml";
	 	private string StartUpFileUri =  "http://querycommander.rockwolf.com/startpage.xml";
		private bool _showStartPage;
		private string _commandLineFile ="";
		private string _helpFile ="";
		
		// Dockforms...
		private DeserializeDockContent _deserializeDockContent;
		private FrmStartPage m_frmStartPage = null;// new FrmStartPage(this);
		public FrmDebug DebugWindow = null;//new FrmDebug(this);
		public FrmDBObjects m_FrmDBObjects = null;//new FrmDBObjects(this);
		public FrmWorkSpace frmWorkSpace = null;//new FrmWorkSpace(this);
		public FrmOutput OutputWindow = null;//new FrmOutput(this);
		public FrmTask TaskList = null;//new FrmTask(this);

//		private FrmStartPage m_frmStartPage = new FrmStartPage(this);
//		public FrmDebug DebugWindow = new FrmDebug(this);
//		public FrmDBObjects m_FrmDBObjects = new FrmDBObjects(this);
//		public FrmWorkSpace frmWorkSpace = new FrmWorkSpace(this);
//		public FrmOutput OutputWindow = new FrmOutput(this);
//		public FrmTask TaskList = new FrmTask(this);

		private Options m_options = new Options();
		public ArrayList QueryForms = new ArrayList();
		public System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemView;
		private System.Windows.Forms.MenuItem menuItemPropertyWindow;
		private System.Windows.Forms.MenuItem menuItemOutputWindow;
		private System.Windows.Forms.MenuItem menuItemTaskList;
		private System.Windows.Forms.MenuItem menuItemToolbox;
		private System.Windows.Forms.MenuItem menuItemWindow;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItemCloseAll;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.MenuItem menuItemOptions;
		private WeifenLuo.WinFormsUI.DockPanel dockManager = new WeifenLuo.WinFormsUI.DockPanel();
		
		//public WeifenLuo.WinFormsUI.DockManager dockManager;
		public System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton toolBarButtonNew;
		private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
		private System.Windows.Forms.ToolBarButton toolBarButtonSeparator;
		private System.Windows.Forms.ToolBarButton toolBarButtonSolutionExplorer;
		private System.Windows.Forms.ToolBarButton toolBarButtonPropertyWindow;
		private System.Windows.Forms.ToolBarButton toolBarButtonToolbox;
		private System.Windows.Forms.ToolBarButton toolBarButtonOutputWindow;
		private System.Windows.Forms.ToolBarButton toolBarButtonTaskList;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemToolBar;
		private System.Windows.Forms.MenuItem menuItemStatusBar;
		private System.Windows.Forms.ToolBar toolBar;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton toolBarButtonSeparator2;
		private System.Windows.Forms.ToolBarButton toolBarButtonRun;
		private System.Windows.Forms.MenuItem menuItem_InsertStatement;
		private System.Windows.Forms.MenuItem menuItemXmlDoc;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemInsertHeader;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem miUndo;
		private System.Windows.Forms.MenuItem menuItem_UpdateStatement;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem_Help;
		private System.Windows.Forms.MenuItem menuItem_SaveAs;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem_Save;
		private System.Windows.Forms.StatusBarPanel panel1;
		private System.Windows.Forms.StatusBarPanel panel2;
		private System.Windows.Forms.StatusBarPanel panel3;
		private System.Windows.Forms.StatusBarPanel panel4;
		private System.Windows.Forms.ToolBarButton toolBarButtonSeparator3;
		private System.Windows.Forms.MenuItem menuItemFind;
		private System.Windows.Forms.MenuItem menuItemGoToLine;
		private System.Windows.Forms.MenuItem menuItemPaste;
		private System.Windows.Forms.MenuItem menuItemCopy;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem10;
		public  ArrayList DBConnections = new ArrayList();
		private System.Windows.Forms.MenuItem menuItemGoToDefenition;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.ToolBarButton toolBarEnterpriseManager;
		private System.Windows.Forms.ToolBarButton toolBarprofiler;
		private System.Windows.Forms.ToolBarButton toolBarButtonSeparator4;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItemEnterpriseManager;
		private System.Windows.Forms.MenuItem menuItemProfiler;
		private System.Windows.Forms.MenuItem menuItemRunQuery;
		private System.Windows.Forms.Timer timer_StartUp;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItemGoToReference;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuRecentItems;
		private System.Windows.Forms.MenuItem menuItem15;
		private Chris.Beckett.MenuImageLib.MenuImage menuExtender;
		private System.Windows.Forms.MenuItem menuItemCloseOutputWindow;
		private System.Windows.Forms.MenuItem menuItemRunCurrentQuery;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItemRunQueryLine;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem miImportXMLStructure;
		private System.Windows.Forms.MenuItem miImportXMLData;
		public bool Debug=false;
		public System.Windows.Forms.ContextMenu contextMenuDataBases;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItemManageSnippets;
		static System.Windows.Forms.Form _activeForm;
		public System.Windows.Forms.MenuItem menuItem_Plugins;
		public Hashtable plugInVariables=new Hashtable();
		private Hashtable pluginMenuItemObject = new Hashtable();
		public System.Windows.Forms.MenuItem menuItemFindNext;
		private System.Windows.Forms.MenuItem menuItemCut;
		private System.Windows.Forms.StatusBarPanel panel5;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItemWorkSpaceExplorer;
		private System.Windows.Forms.MenuItem menuItemCompare;
		private System.Windows.Forms.ToolBarButton toolBarButtonStop;
		private System.Windows.Forms.MenuItem menuItemStopQuery;
	
		public FrmQuery  ActiveQueryForm
		{
			get
			{
				if(dockManager.ActiveDocument==null)
					NewQueryform();

				Type t = dockManager.ActiveDocument.GetType();
				if(t.ToString()=="QueryCommander.FrmQuery")
					return (FrmQuery)dockManager.ActiveDocument;
				else
					return null;
			}
			set 
			{
				ActiveQueryForm = value;
			}
		}
		public ArrayList Plugins = new ArrayList();
		#endregion
		#region Default
		public MainForm(string commandLineFile)
		{
			_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
			_commandLineFile = commandLineFile;

			InitDockManager();
			InitializeComponent();
			
			InitDockForms();

			_helpFile=GetHelpFilePath();

			string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//This will strip just the working path name:
			//C:\Program Files\MyApplication
			string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

			string strSettingsXmlFilePath = System.IO.Path.Combine(strWorkPath, "DataSources.config");

			if (!File.Exists(Application.StartupPath + "\\DataSources.config"))
				CopyEmbeddedResource("QueryCommander.DataSources.config", strSettingsXmlFilePath);

			this.panel1.Text  = "QueryCommander : " + System.Windows.Forms.Application.ProductVersion;

		 	vssConnectionCollection = VSSConnectionCollectionFactory.Load();//
			workSpaceCollection = WorkSpaceFactory.Load(System.IO.Path.Combine(strWorkPath, "WorkSpace.config"));

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem_Save = new System.Windows.Forms.MenuItem();
            this.menuItem_SaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.miImportXMLStructure = new System.Windows.Forms.MenuItem();
            this.miImportXMLData = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItemCloseAll = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuRecentItems = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miUndo = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemPaste = new System.Windows.Forms.MenuItem();
            this.menuItemCopy = new System.Windows.Forms.MenuItem();
            this.menuItemCut = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItemFind = new System.Windows.Forms.MenuItem();
            this.menuItemFindNext = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItemGoToLine = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItemManageSnippets = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItemCompare = new System.Windows.Forms.MenuItem();
            this.menuItemView = new System.Windows.Forms.MenuItem();
            this.menuItemPropertyWindow = new System.Windows.Forms.MenuItem();
            this.menuItemToolbox = new System.Windows.Forms.MenuItem();
            this.menuItemWorkSpaceExplorer = new System.Windows.Forms.MenuItem();
            this.menuItemOutputWindow = new System.Windows.Forms.MenuItem();
            this.menuItemTaskList = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemToolBar = new System.Windows.Forms.MenuItem();
            this.menuItemStatusBar = new System.Windows.Forms.MenuItem();
            this.menuItemCloseOutputWindow = new System.Windows.Forms.MenuItem();
            this.menuItemTools = new System.Windows.Forms.MenuItem();
            this.menuItem_InsertStatement = new System.Windows.Forms.MenuItem();
            this.menuItem_UpdateStatement = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemInsertHeader = new System.Windows.Forms.MenuItem();
            this.menuItemXmlDoc = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemGoToDefenition = new System.Windows.Forms.MenuItem();
            this.menuItemGoToReference = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItemRunQuery = new System.Windows.Forms.MenuItem();
            this.menuItemRunCurrentQuery = new System.Windows.Forms.MenuItem();
            this.menuItemStopQuery = new System.Windows.Forms.MenuItem();
            this.menuItemRunQueryLine = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItemEnterpriseManager = new System.Windows.Forms.MenuItem();
            this.menuItemProfiler = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItem_Plugins = new System.Windows.Forms.MenuItem();
            this.menuItemWindow = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItem_Help = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.StatusBarPanel();
            this.panel2 = new System.Windows.Forms.StatusBarPanel();
            this.panel3 = new System.Windows.Forms.StatusBarPanel();
            this.panel4 = new System.Windows.Forms.StatusBarPanel();
            this.panel5 = new System.Windows.Forms.StatusBarPanel();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.toolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSolutionExplorer = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPropertyWindow = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonToolbox = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOutputWindow = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonTaskList = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRun = new System.Windows.Forms.ToolBarButton();
            this.contextMenuDataBases = new System.Windows.Forms.ContextMenu();
            this.toolBarButtonStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator3 = new System.Windows.Forms.ToolBarButton();
            this.toolBarEnterpriseManager = new System.Windows.Forms.ToolBarButton();
            this.toolBarprofiler = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator4 = new System.Windows.Forms.ToolBarButton();
            this.timer_StartUp = new System.Windows.Forms.Timer(this.components);
            this.menuExtender = new Chris.Beckett.MenuImageLib.MenuImage(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel5)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItem3,
            this.menuItemView,
            this.menuItemTools,
            this.menuItem_Plugins,
            this.menuItemWindow,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItem8,
            this.menuItem_Save,
            this.menuItem_SaveAs,
            this.menuItem18,
            this.menuItem17,
            this.menuItem9,
            this.menuItemClose,
            this.menuItemCloseAll,
            this.menuItem4,
            this.menuRecentItems,
            this.menuItem15,
            this.menuItemExit});
            this.menuItemFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemFile.Text = "&File";
            this.menuItemFile.Popup += new System.EventHandler(this.menuItemFile_Popup);
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuExtender.SetMenuImage(this.menuItemNew, "0");
            this.menuItemNew.OwnerDraw = true;
            this.menuItemNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 1;
            this.menuExtender.SetMenuImage(this.menuItemOpen, "1");
            this.menuItemOpen.OwnerDraw = true;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuExtender.SetMenuImage(this.menuItem8, null);
            this.menuItem8.OwnerDraw = true;
            this.menuItem8.Text = "-";
            // 
            // menuItem_Save
            // 
            this.menuItem_Save.Index = 3;
            this.menuExtender.SetMenuImage(this.menuItem_Save, "15");
            this.menuItem_Save.OwnerDraw = true;
            this.menuItem_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem_Save.Text = "&Save";
            this.menuItem_Save.Click += new System.EventHandler(this.menuItem_Save_Click);
            // 
            // menuItem_SaveAs
            // 
            this.menuItem_SaveAs.Index = 4;
            this.menuExtender.SetMenuImage(this.menuItem_SaveAs, null);
            this.menuItem_SaveAs.OwnerDraw = true;
            this.menuItem_SaveAs.Text = "Save &as";
            this.menuItem_SaveAs.Click += new System.EventHandler(this.menuItem_SaveAs_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 5;
            this.menuExtender.SetMenuImage(this.menuItem18, null);
            this.menuItem18.OwnerDraw = true;
            this.menuItem18.Text = "-";
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 6;
            this.menuExtender.SetMenuImage(this.menuItem17, null);
            this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miImportXMLStructure,
            this.miImportXMLData});
            this.menuItem17.OwnerDraw = true;
            this.menuItem17.Text = "&Import";
            // 
            // miImportXMLStructure
            // 
            this.miImportXMLStructure.Index = 0;
            this.menuExtender.SetMenuImage(this.miImportXMLStructure, null);
            this.miImportXMLStructure.OwnerDraw = true;
            this.miImportXMLStructure.Text = "Table structure";
            this.miImportXMLStructure.Click += new System.EventHandler(this.miImportXMLStructure_Click);
            // 
            // miImportXMLData
            // 
            this.miImportXMLData.Index = 1;
            this.menuExtender.SetMenuImage(this.miImportXMLData, null);
            this.miImportXMLData.OwnerDraw = true;
            this.miImportXMLData.Text = "XML Data";
            this.miImportXMLData.Click += new System.EventHandler(this.miImportXMLData_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 7;
            this.menuExtender.SetMenuImage(this.menuItem9, null);
            this.menuItem9.OwnerDraw = true;
            this.menuItem9.Text = "-";
            // 
            // menuItemClose
            // 
            this.menuItemClose.Index = 8;
            this.menuExtender.SetMenuImage(this.menuItemClose, null);
            this.menuItemClose.OwnerDraw = true;
            this.menuItemClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
            this.menuItemClose.Text = "&Close";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuItemCloseAll
            // 
            this.menuItemCloseAll.Index = 9;
            this.menuExtender.SetMenuImage(this.menuItemCloseAll, null);
            this.menuItemCloseAll.OwnerDraw = true;
            this.menuItemCloseAll.Text = "Close &All";
            this.menuItemCloseAll.Click += new System.EventHandler(this.menuItemCloseAll_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 10;
            this.menuExtender.SetMenuImage(this.menuItem4, null);
            this.menuItem4.OwnerDraw = true;
            this.menuItem4.Text = "-";
            // 
            // menuRecentItems
            // 
            this.menuRecentItems.Index = 11;
            this.menuExtender.SetMenuImage(this.menuRecentItems, null);
            this.menuRecentItems.OwnerDraw = true;
            this.menuRecentItems.Text = "Recent &objects";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 12;
            this.menuExtender.SetMenuImage(this.menuItem15, null);
            this.menuItem15.OwnerDraw = true;
            this.menuItem15.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 13;
            this.menuExtender.SetMenuImage(this.menuItemExit, null);
            this.menuItemExit.OwnerDraw = true;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miUndo,
            this.menuItem7,
            this.menuItemPaste,
            this.menuItemCopy,
            this.menuItemCut,
            this.menuItem10,
            this.menuItemFind,
            this.menuItemFindNext,
            this.menuItem14,
            this.menuItemGoToLine,
            this.menuItem19,
            this.menuItemManageSnippets,
            this.menuItem20,
            this.menuItemCompare});
            this.menuItem3.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItem3.Text = "&Edit";
            // 
            // miUndo
            // 
            this.miUndo.Index = 0;
            this.menuExtender.SetMenuImage(this.miUndo, "17");
            this.miUndo.OwnerDraw = true;
            this.miUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.miUndo.Text = "&Undo";
            this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuExtender.SetMenuImage(this.menuItem7, null);
            this.menuItem7.OwnerDraw = true;
            this.menuItem7.Text = "-";
            // 
            // menuItemPaste
            // 
            this.menuItemPaste.Index = 2;
            this.menuExtender.SetMenuImage(this.menuItemPaste, "19");
            this.menuItemPaste.OwnerDraw = true;
            this.menuItemPaste.Text = "&Paste";
            this.menuItemPaste.Click += new System.EventHandler(this.menuItemPaste_Click);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Index = 3;
            this.menuExtender.SetMenuImage(this.menuItemCopy, "18");
            this.menuItemCopy.OwnerDraw = true;
            this.menuItemCopy.Text = "&Copy";
            this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
            // 
            // menuItemCut
            // 
            this.menuItemCut.Index = 4;
            this.menuExtender.SetMenuImage(this.menuItemCut, null);
            this.menuItemCut.OwnerDraw = true;
            this.menuItemCut.Text = "Cu&t";
            this.menuItemCut.Click += new System.EventHandler(this.menuItemCut_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuExtender.SetMenuImage(this.menuItem10, null);
            this.menuItem10.OwnerDraw = true;
            this.menuItem10.Text = "-";
            // 
            // menuItemFind
            // 
            this.menuItemFind.Index = 6;
            this.menuExtender.SetMenuImage(this.menuItemFind, "21");
            this.menuItemFind.OwnerDraw = true;
            this.menuItemFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItemFind.Text = "Find";
            this.menuItemFind.Click += new System.EventHandler(this.menuItemFind_Click);
            // 
            // menuItemFindNext
            // 
            this.menuItemFindNext.Index = 7;
            this.menuExtender.SetMenuImage(this.menuItemFindNext, null);
            this.menuItemFindNext.OwnerDraw = true;
            this.menuItemFindNext.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItemFindNext.Text = "Find next";
            this.menuItemFindNext.Click += new System.EventHandler(this.menuItemFindNext_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 8;
            this.menuExtender.SetMenuImage(this.menuItem14, null);
            this.menuItem14.OwnerDraw = true;
            this.menuItem14.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.menuItem14.Text = "R&eplace";
            this.menuItem14.Click += new System.EventHandler(this.menuItemreplace_Click);
            // 
            // menuItemGoToLine
            // 
            this.menuItemGoToLine.Index = 9;
            this.menuExtender.SetMenuImage(this.menuItemGoToLine, null);
            this.menuItemGoToLine.OwnerDraw = true;
            this.menuItemGoToLine.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.menuItemGoToLine.Text = "&Go to line";
            this.menuItemGoToLine.Click += new System.EventHandler(this.menuItemGoToLine_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 10;
            this.menuExtender.SetMenuImage(this.menuItem19, null);
            this.menuItem19.OwnerDraw = true;
            this.menuItem19.Text = "-";
            // 
            // menuItemManageSnippets
            // 
            this.menuItemManageSnippets.Index = 11;
            this.menuExtender.SetMenuImage(this.menuItemManageSnippets, null);
            this.menuItemManageSnippets.OwnerDraw = true;
            this.menuItemManageSnippets.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.menuItemManageSnippets.Text = "Manage &snippets";
            this.menuItemManageSnippets.Click += new System.EventHandler(this.menuItemManageSnippets_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 12;
            this.menuExtender.SetMenuImage(this.menuItem20, null);
            this.menuItem20.OwnerDraw = true;
            this.menuItem20.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItem20.Text = "Set reserved words to upper case";
            this.menuItem20.Click += new System.EventHandler(this.menuItemUpperCase_Click);
            // 
            // menuItemCompare
            // 
            this.menuItemCompare.Index = 13;
            this.menuExtender.SetMenuImage(this.menuItemCompare, null);
            this.menuItemCompare.OwnerDraw = true;
            this.menuItemCompare.Text = "Compare ";
            this.menuItemCompare.Click += new System.EventHandler(this.menuItemCompare_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.Index = 2;
            this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPropertyWindow,
            this.menuItemToolbox,
            this.menuItemWorkSpaceExplorer,
            this.menuItemOutputWindow,
            this.menuItemTaskList,
            this.menuItem13,
            this.menuItem1,
            this.menuItemToolBar,
            this.menuItemStatusBar,
            this.menuItemCloseOutputWindow});
            this.menuItemView.MergeOrder = 1;
            this.menuItemView.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemView.Text = "&View";
            // 
            // menuItemPropertyWindow
            // 
            this.menuItemPropertyWindow.Enabled = false;
            this.menuItemPropertyWindow.Index = 0;
            this.menuExtender.SetMenuImage(this.menuItemPropertyWindow, null);
            this.menuItemPropertyWindow.OwnerDraw = true;
            this.menuItemPropertyWindow.Text = "&Property Window";
            this.menuItemPropertyWindow.Visible = false;
            // 
            // menuItemToolbox
            // 
            this.menuItemToolbox.Index = 1;
            this.menuExtender.SetMenuImage(this.menuItemToolbox, "2");
            this.menuItemToolbox.OwnerDraw = true;
            this.menuItemToolbox.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.menuItemToolbox.Text = "Server Explorer";
            this.menuItemToolbox.Click += new System.EventHandler(this.menuItemToolbox_Click);
            // 
            // menuItemWorkSpaceExplorer
            // 
            this.menuItemWorkSpaceExplorer.Index = 2;
            this.menuExtender.SetMenuImage(this.menuItemWorkSpaceExplorer, null);
            this.menuItemWorkSpaceExplorer.OwnerDraw = true;
            this.menuItemWorkSpaceExplorer.Text = "Workspace Explorer";
            this.menuItemWorkSpaceExplorer.Click += new System.EventHandler(this.menuItemWorkSpaceExplorer_Click);
            // 
            // menuItemOutputWindow
            // 
            this.menuItemOutputWindow.Index = 3;
            this.menuExtender.SetMenuImage(this.menuItemOutputWindow, "5");
            this.menuItemOutputWindow.OwnerDraw = true;
            this.menuItemOutputWindow.Text = "&Output Window";
            this.menuItemOutputWindow.Click += new System.EventHandler(this.menuItemOutputWindow_Click);
            // 
            // menuItemTaskList
            // 
            this.menuItemTaskList.Index = 4;
            this.menuExtender.SetMenuImage(this.menuItemTaskList, "6");
            this.menuItemTaskList.OwnerDraw = true;
            this.menuItemTaskList.Text = "Task &List";
            this.menuItemTaskList.Click += new System.EventHandler(this.menuItemTaskList_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 5;
            this.menuExtender.SetMenuImage(this.menuItem13, null);
            this.menuItem13.OwnerDraw = true;
            this.menuItem13.Text = "&Startup page";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 6;
            this.menuExtender.SetMenuImage(this.menuItem1, null);
            this.menuItem1.OwnerDraw = true;
            this.menuItem1.Text = "-";
            // 
            // menuItemToolBar
            // 
            this.menuItemToolBar.Checked = true;
            this.menuItemToolBar.Index = 7;
            this.menuExtender.SetMenuImage(this.menuItemToolBar, null);
            this.menuItemToolBar.OwnerDraw = true;
            this.menuItemToolBar.Text = "Tool Bar";
            this.menuItemToolBar.Click += new System.EventHandler(this.menuItemToolBar_Click);
            // 
            // menuItemStatusBar
            // 
            this.menuItemStatusBar.Checked = true;
            this.menuItemStatusBar.Index = 8;
            this.menuExtender.SetMenuImage(this.menuItemStatusBar, null);
            this.menuItemStatusBar.OwnerDraw = true;
            this.menuItemStatusBar.Text = "Status Bar";
            this.menuItemStatusBar.Click += new System.EventHandler(this.menuItemStatusBar_Click);
            // 
            // menuItemCloseOutputWindow
            // 
            this.menuItemCloseOutputWindow.Index = 9;
            this.menuExtender.SetMenuImage(this.menuItemCloseOutputWindow, null);
            this.menuItemCloseOutputWindow.OwnerDraw = true;
            this.menuItemCloseOutputWindow.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.menuItemCloseOutputWindow.Text = "&Close Output Window";
            this.menuItemCloseOutputWindow.Click += new System.EventHandler(this.menuItemCloseOutputWindow_Click);
            // 
            // menuItemTools
            // 
            this.menuItemTools.Index = 3;
            this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_InsertStatement,
            this.menuItem_UpdateStatement,
            this.menuItem5,
            this.menuItemInsertHeader,
            this.menuItemXmlDoc,
            this.menuItem2,
            this.menuItemGoToDefenition,
            this.menuItemGoToReference,
            this.menuItem16,
            this.menuItemRunQuery,
            this.menuItemRunCurrentQuery,
            this.menuItemStopQuery,
            this.menuItemRunQueryLine,
            this.menuItem11,
            this.menuItemEnterpriseManager,
            this.menuItemProfiler,
            this.menuItem12,
            this.menuItemOptions});
            this.menuItemTools.MergeOrder = 2;
            this.menuItemTools.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemTools.Text = "&Tools";
            // 
            // menuItem_InsertStatement
            // 
            this.menuItem_InsertStatement.Index = 0;
            this.menuExtender.SetMenuImage(this.menuItem_InsertStatement, null);
            this.menuItem_InsertStatement.OwnerDraw = true;
            this.menuItem_InsertStatement.Text = "&Create insert statement";
            this.menuItem_InsertStatement.Click += new System.EventHandler(this.menuItem_InsertStatement_Click);
            // 
            // menuItem_UpdateStatement
            // 
            this.menuItem_UpdateStatement.Index = 1;
            this.menuExtender.SetMenuImage(this.menuItem_UpdateStatement, null);
            this.menuItem_UpdateStatement.OwnerDraw = true;
            this.menuItem_UpdateStatement.Text = "Create &update statement";
            this.menuItem_UpdateStatement.Click += new System.EventHandler(this.menuItem_UpdateStatement_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuExtender.SetMenuImage(this.menuItem5, null);
            this.menuItem5.OwnerDraw = true;
            this.menuItem5.Text = "-";
            // 
            // menuItemInsertHeader
            // 
            this.menuItemInsertHeader.Index = 3;
            this.menuExtender.SetMenuImage(this.menuItemInsertHeader, "16");
            this.menuItemInsertHeader.OwnerDraw = true;
            this.menuItemInsertHeader.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.menuItemInsertHeader.Text = "&Insert documentation header";
            this.menuItemInsertHeader.Click += new System.EventHandler(this.menuItemInsertHeader_Click);
            // 
            // menuItemXmlDoc
            // 
            this.menuItemXmlDoc.Index = 4;
            this.menuExtender.SetMenuImage(this.menuItemXmlDoc, "24");
            this.menuItemXmlDoc.OwnerDraw = true;
            this.menuItemXmlDoc.Text = "Create &xml documentation file";
            this.menuItemXmlDoc.Click += new System.EventHandler(this.menuItemXmlDoc_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuExtender.SetMenuImage(this.menuItem2, null);
            this.menuItem2.OwnerDraw = true;
            this.menuItem2.Text = "-";
            // 
            // menuItemGoToDefenition
            // 
            this.menuItemGoToDefenition.Index = 6;
            this.menuExtender.SetMenuImage(this.menuItemGoToDefenition, "22");
            this.menuItemGoToDefenition.OwnerDraw = true;
            this.menuItemGoToDefenition.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.menuItemGoToDefenition.Text = "Go to Defenition";
            this.menuItemGoToDefenition.Click += new System.EventHandler(this.menuItemGoToDefenition_Click);
            // 
            // menuItemGoToReference
            // 
            this.menuItemGoToReference.Index = 7;
            this.menuExtender.SetMenuImage(this.menuItemGoToReference, "23");
            this.menuItemGoToReference.OwnerDraw = true;
            this.menuItemGoToReference.Shortcut = System.Windows.Forms.Shortcut.ShiftF12;
            this.menuItemGoToReference.Text = "Go to Reference";
            this.menuItemGoToReference.Click += new System.EventHandler(this.menuItemGoToReference_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 8;
            this.menuExtender.SetMenuImage(this.menuItem16, null);
            this.menuItem16.OwnerDraw = true;
            this.menuItem16.Text = "-";
            // 
            // menuItemRunQuery
            // 
            this.menuItemRunQuery.Index = 9;
            this.menuExtender.SetMenuImage(this.menuItemRunQuery, "12");
            this.menuItemRunQuery.OwnerDraw = true;
            this.menuItemRunQuery.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.menuItemRunQuery.Text = "Run Query";
            this.menuItemRunQuery.Click += new System.EventHandler(this.menuItemRunQuery_Click);
            // 
            // menuItemRunCurrentQuery
            // 
            this.menuItemRunCurrentQuery.Index = 10;
            this.menuExtender.SetMenuImage(this.menuItemRunCurrentQuery, null);
            this.menuItemRunCurrentQuery.OwnerDraw = true;
            this.menuItemRunCurrentQuery.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.menuItemRunCurrentQuery.Text = "Run &current query";
            this.menuItemRunCurrentQuery.Click += new System.EventHandler(this.menuItemRunCurrentQuery_Click);
            // 
            // menuItemStopQuery
            // 
            this.menuItemStopQuery.Index = 11;
            this.menuExtender.SetMenuImage(this.menuItemStopQuery, null);
            this.menuItemStopQuery.OwnerDraw = true;
            this.menuItemStopQuery.Text = "Stop Query";
            this.menuItemStopQuery.Click += new System.EventHandler(this.menuItemStopQuery_Click);
            // 
            // menuItemRunQueryLine
            // 
            this.menuItemRunQueryLine.Index = 12;
            this.menuExtender.SetMenuImage(this.menuItemRunQueryLine, null);
            this.menuItemRunQueryLine.OwnerDraw = true;
            this.menuItemRunQueryLine.Shortcut = System.Windows.Forms.Shortcut.ShiftF9;
            this.menuItemRunQueryLine.Text = "Run current query &line";
            this.menuItemRunQueryLine.Visible = false;
            this.menuItemRunQueryLine.Click += new System.EventHandler(this.menuItemRunQueryLine_Click_1);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 13;
            this.menuExtender.SetMenuImage(this.menuItem11, null);
            this.menuItem11.OwnerDraw = true;
            this.menuItem11.Text = "-";
            // 
            // menuItemEnterpriseManager
            // 
            this.menuItemEnterpriseManager.Index = 14;
            this.menuExtender.SetMenuImage(this.menuItemEnterpriseManager, "13");
            this.menuItemEnterpriseManager.OwnerDraw = true;
            this.menuItemEnterpriseManager.Text = "SQL Server Enterprise Manager";
            this.menuItemEnterpriseManager.Click += new System.EventHandler(this.menuItemEnterpriseManager_Click);
            // 
            // menuItemProfiler
            // 
            this.menuItemProfiler.Index = 15;
            this.menuExtender.SetMenuImage(this.menuItemProfiler, "14");
            this.menuItemProfiler.OwnerDraw = true;
            this.menuItemProfiler.Text = "SQL Profiler";
            this.menuItemProfiler.Click += new System.EventHandler(this.menuItemProfiler_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 16;
            this.menuExtender.SetMenuImage(this.menuItem12, null);
            this.menuItem12.OwnerDraw = true;
            this.menuItem12.Text = "-";
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Index = 17;
            this.menuExtender.SetMenuImage(this.menuItemOptions, "4");
            this.menuItemOptions.OwnerDraw = true;
            this.menuItemOptions.Text = "&Options";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // menuItem_Plugins
            // 
            this.menuItem_Plugins.Index = 4;
            this.menuItem_Plugins.Text = "Plugins";
            this.menuItem_Plugins.Visible = false;
            // 
            // menuItemWindow
            // 
            this.menuItemWindow.Index = 5;
            this.menuItemWindow.MdiList = true;
            this.menuItemWindow.MergeOrder = 3;
            this.menuItemWindow.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemWindow.Text = "&Window";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 6;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Help,
            this.menuItem6,
            this.menuItemAbout});
            this.menuItemHelp.MergeOrder = 4;
            this.menuItemHelp.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.Index = 0;
            this.menuExtender.SetMenuImage(this.menuItem_Help, null);
            this.menuItem_Help.OwnerDraw = true;
            this.menuItem_Help.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItem_Help.Text = "&Transact-SQL Help";
            this.menuItem_Help.Click += new System.EventHandler(this.menuItem_Help_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuExtender.SetMenuImage(this.menuItem6, null);
            this.menuItem6.OwnerDraw = true;
            this.menuItem6.Text = "-";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 2;
            this.menuExtender.SetMenuImage(this.menuItemAbout, null);
            this.menuItemAbout.OwnerDraw = true;
            this.menuItemAbout.Text = "&About QueryCommander...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 651);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.panel1,
            this.panel2,
            this.panel3,
            this.panel4,
            this.panel5});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(855, 21);
            this.statusBar.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Name = "panel1";
            this.panel1.Text = "QueryCommander";
            this.panel1.Width = 160;
            // 
            // panel2
            // 
            this.panel2.Name = "panel2";
            this.panel2.Width = 150;
            // 
            // panel3
            // 
            this.panel3.Name = "panel3";
            this.panel3.Width = 300;
            // 
            // panel4
            // 
            this.panel4.Name = "panel4";
            this.panel4.Width = 500;
            // 
            // panel5
            // 
            this.panel5.Name = "panel5";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "");
            this.imageList.Images.SetKeyName(15, "");
            this.imageList.Images.SetKeyName(16, "");
            this.imageList.Images.SetKeyName(17, "");
            this.imageList.Images.SetKeyName(18, "");
            this.imageList.Images.SetKeyName(19, "");
            this.imageList.Images.SetKeyName(20, "");
            this.imageList.Images.SetKeyName(21, "");
            this.imageList.Images.SetKeyName(22, "");
            this.imageList.Images.SetKeyName(23, "");
            this.imageList.Images.SetKeyName(24, "");
            this.imageList.Images.SetKeyName(25, "");
            this.imageList.Images.SetKeyName(26, "");
            this.imageList.Images.SetKeyName(27, "");
            this.imageList.Images.SetKeyName(28, "");
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonNew,
            this.toolBarButtonOpen,
            this.toolBarButtonSolutionExplorer,
            this.toolBarButtonSeparator,
            this.toolBarButtonPropertyWindow,
            this.toolBarButtonToolbox,
            this.toolBarButtonSeparator2,
            this.toolBarButtonOutputWindow,
            this.toolBarButtonTaskList,
            this.toolBarButtonRun,
            this.toolBarButtonStop,
            this.toolBarButtonSeparator3,
            this.toolBarEnterpriseManager,
            this.toolBarprofiler,
            this.toolBarButtonSeparator4});
            this.toolBar.ButtonSize = new System.Drawing.Size(150, 36);
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(855, 50);
            this.toolBar.TabIndex = 6;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // toolBarButtonNew
            // 
            this.toolBarButtonNew.ImageIndex = 0;
            this.toolBarButtonNew.Name = "toolBarButtonNew";
            this.toolBarButtonNew.Text = "New";
            this.toolBarButtonNew.ToolTipText = "New";
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.ImageIndex = 1;
            this.toolBarButtonOpen.Name = "toolBarButtonOpen";
            this.toolBarButtonOpen.Text = "Open";
            this.toolBarButtonOpen.ToolTipText = "Open";
            // 
            // toolBarButtonSolutionExplorer
            // 
            this.toolBarButtonSolutionExplorer.ImageIndex = 2;
            this.toolBarButtonSolutionExplorer.Name = "toolBarButtonSolutionExplorer";
            this.toolBarButtonSolutionExplorer.Text = "dd";
            this.toolBarButtonSolutionExplorer.ToolTipText = "Server Explorer";
            this.toolBarButtonSolutionExplorer.Visible = false;
            // 
            // toolBarButtonSeparator
            // 
            this.toolBarButtonSeparator.Name = "toolBarButtonSeparator";
            this.toolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.toolBarButtonSeparator.Visible = false;
            // 
            // toolBarButtonPropertyWindow
            // 
            this.toolBarButtonPropertyWindow.ImageIndex = 3;
            this.toolBarButtonPropertyWindow.Name = "toolBarButtonPropertyWindow";
            this.toolBarButtonPropertyWindow.ToolTipText = "Property Window";
            this.toolBarButtonPropertyWindow.Visible = false;
            // 
            // toolBarButtonToolbox
            // 
            this.toolBarButtonToolbox.ImageIndex = 2;
            this.toolBarButtonToolbox.Name = "toolBarButtonToolbox";
            this.toolBarButtonToolbox.Text = "Browse servers";
            this.toolBarButtonToolbox.ToolTipText = "Microsoft SQL Servers";
            // 
            // toolBarButtonSeparator2
            // 
            this.toolBarButtonSeparator2.Name = "toolBarButtonSeparator2";
            this.toolBarButtonSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonOutputWindow
            // 
            this.toolBarButtonOutputWindow.ImageIndex = 5;
            this.toolBarButtonOutputWindow.Name = "toolBarButtonOutputWindow";
            this.toolBarButtonOutputWindow.ToolTipText = "Output Window";
            this.toolBarButtonOutputWindow.Visible = false;
            // 
            // toolBarButtonTaskList
            // 
            this.toolBarButtonTaskList.ImageIndex = 6;
            this.toolBarButtonTaskList.Name = "toolBarButtonTaskList";
            this.toolBarButtonTaskList.ToolTipText = "Task List";
            this.toolBarButtonTaskList.Visible = false;
            // 
            // toolBarButtonRun
            // 
            this.toolBarButtonRun.DropDownMenu = this.contextMenuDataBases;
            this.toolBarButtonRun.ImageIndex = 12;
            this.toolBarButtonRun.Name = "toolBarButtonRun";
            this.toolBarButtonRun.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.toolBarButtonRun.Text = "Run Query";
            this.toolBarButtonRun.ToolTipText = "Run query";
            // 
            // contextMenuDataBases
            // 
            this.contextMenuDataBases.Popup += new System.EventHandler(this.contextMenuDataBases_Popup);
            // 
            // toolBarButtonStop
            // 
            this.toolBarButtonStop.ImageIndex = 28;
            this.toolBarButtonStop.Name = "toolBarButtonStop";
            this.toolBarButtonStop.Text = "Stop Query";
            this.toolBarButtonStop.ToolTipText = "Stop current execution";
            // 
            // toolBarButtonSeparator3
            // 
            this.toolBarButtonSeparator3.Name = "toolBarButtonSeparator3";
            this.toolBarButtonSeparator3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarEnterpriseManager
            // 
            this.toolBarEnterpriseManager.ImageIndex = 13;
            this.toolBarEnterpriseManager.Name = "toolBarEnterpriseManager";
            this.toolBarEnterpriseManager.Text = "Enterprise Manager";
            this.toolBarEnterpriseManager.ToolTipText = "SQL Server Enterprise Manager";
            // 
            // toolBarprofiler
            // 
            this.toolBarprofiler.ImageIndex = 14;
            this.toolBarprofiler.Name = "toolBarprofiler";
            this.toolBarprofiler.Text = "SQL Profiler";
            this.toolBarprofiler.ToolTipText = "SQL Profiler";
            // 
            // toolBarButtonSeparator4
            // 
            this.toolBarButtonSeparator4.Name = "toolBarButtonSeparator4";
            this.toolBarButtonSeparator4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.toolBarButtonSeparator4.Visible = false;
            // 
            // timer_StartUp
            // 
            this.timer_StartUp.Tick += new System.EventHandler(this.timer_StartUp_Tick);
            // 
            // menuExtender
            // 
            this.menuExtender.ImageList = this.imageList;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(855, 672);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "QueryCommander";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			// .Net framework 1.1 functionality to Enable visual styles.
			
			
			Application.DoEvents();
			Application.EnableVisualStyles();
			Application.DoEvents();

			// Adds the event handler to to the event.
			Application.ThreadException += new ThreadExceptionEventHandler(FrmMain_ThreadException);

			if(args.Length>0)
				Application.Run(new MainForm(args[0]));
			else
				Application.Run(new MainForm(""));
		}

		#endregion
		#region Menu items
		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Close();
			Application.Exit();
		}

		private void menuItemSolutionExplorer_Click(object sender, System.EventArgs e)
		{
			//m_solutionExplorer.Show(dockManager);
		}

		private void menuItemToolbox_Click(object sender, System.EventArgs e)
		{
			if(m_FrmDBObjects.IsActivated)
				m_FrmDBObjects.Hide();
			else
				m_FrmDBObjects.Show(dockManager);
		}
		private void menuItemWorkSpaceExplorer_Click(object sender, System.EventArgs e)
		{
			if(frmWorkSpace.IsActivated)
				frmWorkSpace.Hide();
			else
				frmWorkSpace.Show(dockManager);
		}
		private void menuItemOutputWindow_Click(object sender, System.EventArgs e)
		{
			OutputWindow.Show(dockManager);
		}

		private void menuItemTaskList_Click(object sender, System.EventArgs e)
		{
			TaskList.Show(dockManager);
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			FrmAbout aboutDialog = new FrmAbout();

			//AboutDialog aboutDialog = new AboutDialog();
			aboutDialog.ShowDialogWindow(this);
		}

		private void menuItem_InsertStatement_Click(object sender, System.EventArgs e)
		{
			this.ActiveQueryForm.CreateInsertStatement();
		}

		private void menuItemXmlDoc_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GetXmlDocFile();
		}

		private void menuItemInsertHeader_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.InsertHeader();
		}

		private void miUndo_Click(object sender, System.EventArgs e)
		{
			this.ActiveQueryForm.Undo();
		}

		private void menuItem_UpdateStatement_Click(object sender, System.EventArgs e)
		{
			this.ActiveQueryForm.CreateUpdateStatement();
		}

		private void menuItem_Help_Click(object sender, System.EventArgs e)
		{
			if(_helpFile.Length==0)
			{
				MessageBox.Show("Helpfile (tsqlref.chm) could not be found.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			HelpNavigator navigator = HelpNavigator.KeywordIndex;// .TableOfContents;
			string helpToken =  ActiveQueryForm.GetCurrentWord();
			Help.ShowHelp(this, _helpFile, navigator, helpToken);
		}

		private void menuItem_SaveAs_Click(object sender, System.EventArgs e)
		{
			SaveDocument();
		}

		private void menuItem_Save_Click(object sender, System.EventArgs e)
		{
			if(ActiveQueryForm.FileName.Length>0)
				ActiveQueryForm.SaveAs(ActiveQueryForm.FileName);
			else
				menuItem_SaveAs_Click(sender,e);

		}

		private void menuItemFind_Click(object sender, System.EventArgs e)
		{
			frmSearch = new FrmSearch(ActiveQueryForm);
			frmSearch.TopMost = true;
			frmSearch.Show();//Dialog(ActiveQueryForm);
			frmSearch.Focus();
		}

		private void menuItemGoToLine_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GoToLine();
		}

		private void menuItemPaste_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.Paste();
		}

		private void menuItemCopy_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.Copy();
		}
		private void menuItemCut_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.Cut();
		}
		private void menuItemDebug_Click(object sender, System.EventArgs e)
		{
			if(!Debug)
			{
				DebugWindow.Show(dockManager);
				DebugWindow.Debug=true;
				Debug=true;
				//ActiveQueryForm.qcTextEditor.DebugState = true;
			}
			else
			{
				DebugWindow.Hide();
				DebugWindow.Debug=false;
				Debug=false;
				//ActiveQueryForm.qcTextEditor.DebugState = false;
			}
				
		}

		private void menuItemGoToDefenition_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GoToDefenition();
		}

		private void menuItemProfiler_Click(object sender, System.EventArgs e)
		{
			RunProfilerManager();
		}

		private void menuItemEnterpriseManager_Click(object sender, System.EventArgs e)
		{
			RunEnterpriseManager();
		}

		private void menuItemRunQuery_Click(object sender, System.EventArgs e)
		{
			//			if(OutputWindow.IsActivated)
			//			{
			//				OutputWindow.Visible=false;
			//				ActiveQueryForm.Focus();
			//
			//			}
			//			else
			ActiveQueryForm.RunQuery();
		}
	
		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			NewQueryform();
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();

			openFile.InitialDirectory = Application.ExecutablePath;
			openFile.Filter = "SQL files (*.SQL)|*.SQL|txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
			openFile.FilterIndex = 1;
			openFile.RestoreDirectory = true ;

			if(openFile.ShowDialog() == DialogResult.OK)
			{
				string fullName = openFile.FileName;
				string fileName = Path.GetFileName(fullName);
				string line;
				string fileContent = "";
				if (FindContent(fileName) != null)
				{
					MessageBox.Show("The document: " + fileName + " has already opened!");
					return;
				}
				
				NewQueryform();
				//				FrmQuery frmQuery = new FrmQuery();
				this.ActiveQueryForm.Text = fileName;
				StreamReader sr = new StreamReader(fullName);

				while ((line = sr.ReadLine()) != null) 
				{
					fileContent += "\n" + line;
				}
				sr.Close();
				sr=null;
				this.ActiveQueryForm.Content = fileContent;
				//frmQuery.Show(dockManager);
				try
				{
					this.ActiveQueryForm.FileName = fullName;
				}
				catch (Exception exception)
				{
					this.ActiveQueryForm.Close();
					MessageBox.Show(exception.Message);
				}

			}
			
		}

		private void menuItemFile_Popup(object sender, System.EventArgs e)
		{
			menuItemClose.Enabled = (dockManager.ActiveDocument != null);
			menuItemCloseAll.Enabled = (dockManager.Documents.Length > 0);
		}

		private void menuItemClose_Click(object sender, System.EventArgs e)
		{
			if (dockManager.ActiveDocument != null)
				dockManager.ActiveDocument.Close();
		}

		private void menuItemCloseAll_Click(object sender, System.EventArgs e)
		{
			foreach (DockContent content in dockManager.Documents)
				content.Close();
		}

		private void menuItemOptions_Click(object sender, System.EventArgs e)
		{
			FrmOption frmSettings = new FrmOption(this);
			//FrmSettings frmSettings = new FrmSettings(this);
			frmSettings.ShowDialogWindow(this);
		}

		private void menuItemToolBar_Click(object sender, System.EventArgs e)
		{
			toolBar.Visible = menuItemToolBar.Checked = !menuItemToolBar.Checked;
		}

		private void menuItemStatusBar_Click(object sender, System.EventArgs e)
		{
			statusBar.Visible = menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
		}

		private void menuItemRunQueryLine_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.RunQueryLine();
		}

		private void menuItemGoToReference_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GoToReference();
		}
		
		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			//getXmlStartPageDelegate = new GetXmlStartPageDelegate(asyncMethods.GetXmlStartPage);
			//StartPageResult = getXmlStartPageDelegate.BeginInvoke(StartUpFileUri, out xmlStartPage, null, null);
			//timer_StartUp.Enabled=true;
			//_showStartPage = true;
		}

		private void menuItemreplace_Click(object sender, System.EventArgs e)
		{
			FrmSearch frmSearch = new FrmSearch(ActiveQueryForm,true);
			frmSearch.Show();
			frmSearch.Focus();
		}

		private void menuRecentItem_Click(object sender, System.EventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			m_FrmDBObjects.CreateConstructorString(mi.Text);
			//			MessageBox.Show(mi.Text);
		}

		private void menuItemCloseOutputWindow_Click(object sender, System.EventArgs e)
		{
			OutputWindow.Visible=false;
			ActiveQueryForm.qcTextEditor.Focus();

		}
		
		private void menuItemUpperCase_Click(object sender, System.EventArgs e)
		{
			// FIX!
			ActiveQueryForm.qcTextEditor.SetReseveredWordsToUpperCase();
		}

		private void menuItemCollapse_Click(object sender, System.EventArgs e)
		{
			//ActiveQueryForm.qcTextEditor.Collapse();
		}

		private void menuItemRunCurrentQuery_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.RunCurrentQuery();
		}

		private void menuItemRunQueryLine_Click_1(object sender, System.EventArgs e)
		{
			ActiveQueryForm.RunQueryLine();
		}

		private void miImportXMLStructure_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GetCreateTablesScriptFromXMLFile();
		}

		private void miImportXMLData_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.GetInsertScriptFromXMLFile();
		}
	
		private void menuItemManageSnippets_Click(object sender, System.EventArgs e)
		{
			FrmSnippets frm = new FrmSnippets();
			frm.ShowDialogWindow(this);
		}
		private void PluginMenuItem_Click(object sender, System.EventArgs e)
		{
			IPlugin_OnMenuClick1 obj = (IPlugin_OnMenuClick1)pluginMenuItemObject[sender];
			//(IPlugin_OnMenuClick1)((PluginMenuItem)sender).Tag;
			ActiveQueryForm.ExecutePlugin(obj, (MenuItem)sender);
		}
		
		private void menuItem_AddToWorkSpace_Click(object sender, System.EventArgs e)
		{
			
		}

		private void menuItem_AddWorkSpace_Click(object sender, System.EventArgs e)
		{
			AddWorkSpace();
		}

		private void menuItemCompare_Click(object sender, System.EventArgs e)
		{
			if(ActiveQueryForm.FileName.Length>0)
				ActiveQueryForm.SaveAs(ActiveQueryForm.FileName);
			else
				menuItem_SaveAs_Click(sender,e);

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = "c:\\" ;
			openFileDialog.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*" ;
			openFileDialog.FilterIndex = 2 ;
			openFileDialog.RestoreDirectory = true ;

			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				QueryCommander.VSS.VSSConnection.TextDiff(openFileDialog.FileName, ActiveQueryForm.FileName);
			}



		}
		private void menuItemStopQuery_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.StopCurrentExecution();
		}
		#endregion
		#region Events
		private void MainForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				try
				{


					string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
					//This will strip just the working path name:
					//C:\Program Files\MyApplication
					string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

					string strSettingsXmlFilePath = System.IO.Path.Combine(strWorkPath, "Plugins");

					// Plugins
					string plugInPath = strSettingsXmlFilePath;
					if(!Directory.Exists(plugInPath))
						Directory.CreateDirectory(plugInPath);
					if(Directory.Exists(plugInPath))
					{
						DirectoryInfo pluginDir=new DirectoryInfo(plugInPath);
						foreach(FileInfo file in pluginDir.GetFiles("*.dll"))
						{
							if(file.Name=="QueryCommander.PlugIn.Core.dll")
								continue;
						
							Plugins.Add(file.FullName);

						}
					}
				}
				catch{}

				LoadDockManager();


				QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
                if (settings.Exists())
                {
                    if (settings.ShowStartPage)
                    {
                        getXmlStartPageDelegate = new GetXmlStartPageDelegate(asyncMethods.GetXmlStartPage);
                        StartPageResult = getXmlStartPageDelegate.BeginInvoke(StartUpFileUri, out xmlStartPage, null, null);

                        timer_StartUp.Enabled = true;
                    }
                }
                else
                {
                    getXmlStartPageDelegate = new GetXmlStartPageDelegate(asyncMethods.GetXmlStartPage);
                    StartPageResult = getXmlStartPageDelegate.BeginInvoke(StartUpFileUri, out xmlStartPage, null, null);

                    timer_StartUp.Enabled = true;
                }

                PopulateDBConnections();
                PopulateRecentItems();
                m_frmStartPage.Activate();
            }
			catch(Exception ex)
			{
				string s = ex.Message;
				throw;
			}

		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockManager.config");
			dockManager.SaveAsXml(configFile);
			VSSConnectionCollectionFactory.Save(vssConnectionCollection);
			WorkSpaceFactory.Save(Application.StartupPath + "\\WorkSpace.config", workSpaceCollection);
		}

		private void menuItemFindNext_Click(object sender, System.EventArgs e)
		{
			ActiveQueryForm.FindNext();
//			if(searchValue.Length>0)
//				lastSearchPos = ActiveQueryForm.Find(searchValue,lastSearchPos,richTextBoxFinds);
		}

		private void MainForm_MdiChildActivate(object sender, System.EventArgs e)
		{

		}
		
		/// <summary>
		/// Catches uncaught thread exceptions.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="t"></param>
		static void FrmMain_ThreadException(object sender, ThreadExceptionEventArgs t) 
		{
			try
			{
				Cursor.Current = Cursors.Default;

				ExceptionHandler.ErrorHandler(_activeForm, t.Exception);
			}
				// The exception handling methods threw an exception.
			catch (Exception e)
			{
				MessageBox.Show(e.Message + "\n\n" + e.StackTrace + "\n\n" +
					"The application will now close.", "Serious uncaught exception error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		private void contextMenuDataBases_Popup(object sender, System.EventArgs e)
		{
		
		}

		

		#endregion
		#region Dockmanager
		private void dockManager_ActiveDocumentChanged(object sender, System.EventArgs e)
		{
			try
			{		
				if (m_options.ActiveDocumentChanged)
				{
					string text = "Event: ActiveDocumentChanged.\n";
					if (dockManager.ActiveDocument != null)
						text += "ActiveDocument.Text = " + dockManager.ActiveDocument.Text;
					else
						text += "ActiveDocument = (null)";

					MessageBox.Show(text);
				}
				_activeForm = (System.Windows.Forms.Form)dockManager.ActiveDocument;
				if(dockManager.ActiveDocument is FrmQuery)
				{
					((FrmQuery)dockManager.ActiveDocument).SendToOutPutWindow();
					((FrmQuery)dockManager.ActiveDocument).qcTextEditor.Select();
				}

			}
			catch
			{
				return;
			}
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == toolBarButtonNew)
				menuItemNew_Click(null, null);
			else if (e.Button == toolBarButtonOpen)
				menuItemOpen_Click(null, null);
			else if (e.Button == toolBarButtonSolutionExplorer)
				menuItemSolutionExplorer_Click(null, null);
			else if (e.Button == toolBarButtonToolbox)
				menuItemToolbox_Click(null, null);
			else if (e.Button == toolBarButtonOutputWindow)
				menuItemOutputWindow_Click(null, null);
			else if (e.Button == toolBarButtonTaskList)
				menuItemTaskList_Click(null, null);
			else if (e.Button == toolBarEnterpriseManager)
				RunEnterpriseManager();
			else if (e.Button == toolBarprofiler)
				RunProfilerManager();
			else if(e.Button==toolBarButtonStop)
				menuItemStopQuery_Click(null,null);
			else if (e.Button == toolBarButtonRun)
			{
				foreach(FrmQuery frm in QueryForms)
				{
					if(frm.IsActivated)
					{
						frm.RunQuery();
						break;
					}
				}
			}
		}
		private DockContent GetContentFromPersistString(string persistString)
		{

			//if (persistString == typeof(FrmDBObjects).ToString())
			if (persistString == typeof(FrmDBObjects).ToString())
				return m_FrmDBObjects;
			else if (persistString == typeof(FrmOutput).ToString())
				return OutputWindow;
			else if (persistString == typeof(FrmTask).ToString())
				return TaskList;
			else if (persistString == typeof(FrmWorkSpace).ToString())
				return frmWorkSpace;

			return null;
			
		}

		private void dockManager_ContentAdded(object sender, WeifenLuo.WinFormsUI.DockContentEventArgs e)
		{
			if (m_options.ContentAdded)
			{
				string text = "Event: ContentAdded.\n";
				text += "Content.Text = " + e.Content.Text;
				MessageBox.Show(text);
			}
		}

		private void dockManager_ContentRemoved(object sender, WeifenLuo.WinFormsUI.DockContentEventArgs e)
		{
			if (m_options.ContentRemoved)
			{
				string text = "Event: ContentRemoved.\n";
				text += "Content.Text = " + e.Content.Text;
				MessageBox.Show(text);
			}
		}

	
		private DockContent FindContent(string text)
		{
			DockContent[] documents = dockManager.Documents;

			foreach (DockContent content in documents)
				if (content.Text == text)
					return content;

			return null;
		}

		#endregion
		#region Private Methods
		private void LoadDockManager()
		{

			string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//This will strip just the working path name:
			//C:\Program Files\MyApplication
			string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

			string configFile = System.IO.Path.Combine(strWorkPath, "DockManager.config");

		//	string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockManager.config");
			if (File.Exists(configFile))
			{
				try
				{
					dockManager.LoadFromXml(configFile, _deserializeDockContent);
				}
				catch
				{
					dockManager.SaveAsXml(configFile,System.Text.Encoding.Default);
					dockManager.LoadFromXml(configFile, _deserializeDockContent);
				}
			}
			else
			{
				dockManager.SaveAsXml(configFile,System.Text.Encoding.Default);
				dockManager.LoadFromXml(configFile, _deserializeDockContent);
			}
				
			dockManager.Parent = this;
		}
		private void InitDockManager()
		{
			// 
			// dockPanel
			// 
			this.dockManager.ActiveAutoHideContent = null;
			this.dockManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockManager.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((System.Byte)(0)));
			this.dockManager.Location = new System.Drawing.Point(0, 28);
			this.dockManager.Name = "dockPanel";
			this.dockManager.Size = new System.Drawing.Size(579, 359);
			this.dockManager.TabIndex = 0;
			this.dockManager.ActiveDocumentChanged +=new EventHandler(dockManager_ActiveDocumentChanged);
			this.Controls.Add(this.dockManager);
		}
		private void InitDockForms()
		{
			m_frmStartPage = new FrmStartPage(this);
			DebugWindow = new FrmDebug(this);
			m_FrmDBObjects = new FrmDBObjects(this);
			frmWorkSpace = new FrmWorkSpace(this);
			OutputWindow = new FrmOutput(this);
			TaskList = new FrmTask(this);
		}
		private void MenuItemDataBases_Click(object sender, System.EventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			int sep = mi.Text.IndexOf(":");
			string server = mi.Text.Substring(0,sep);
			string database = mi.Text.Substring(sep+1,mi.Text.Length-sep-1);

			foreach(MainForm.DBConnection c in DBConnections)
			{
				if(c.ConnectionName == server)
				{
					ActiveQueryForm.SetDatabaseConnection(database,c.Connection);
					break;
				}
			}

		}
		private void RunEnterpriseManager()
		{
			string path = GetEnterpriseManagerFilePath();
			if(path.Length==0)
				MessageBox.Show("Path to SQL Server Enterprise Manager.MSC could not be found.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			else			
				System.Diagnostics.Process.Start(path);
		}
		private void RunProfilerManager()
		{
			string path = GetProfilerFilePath();
			if(path.Length==0)
				MessageBox.Show("Path to SQL Server Enterprise Manager.MSC could not be found.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			else			
				System.Diagnostics.Process.Start(path);
		}
		
		private void PopulateDBConnections()
		{	
			if(_commandLineFile.Length==0)
			{
				FrmDBConnections frm = new FrmDBConnections();
				frm.btnOk_Click(null, null);
				//if(frm.ShowDialogWindow(this)==DialogResult.Cancel)
				//{
				//	if(MessageBox.Show("Do you want to close from QueryCommander?","QueryCommander",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
				//	{
				//		getXmlStartPageDelegate=null;
				//		asyncMethods=null;
				//		Application.Exit();
				//		return;
				//	}
				//}
			}

			try
			{
				OutputWindow.Show(dockManager);
			}
			catch
			{
				OutputWindow.Show(dockManager);
			}
			
			try
			{
				//OutputWindow.Show(dockManager); 
				m_FrmDBObjects.RefreashTreeView(); 
			}
			catch (Exception ex)
			{
				if(ex is SqlException)
				{
					FrmExceptionMessage frm = new FrmExceptionMessage(this,ex);
					frm.ShowDialog(this);

					PopulateDBConnections();
					return;
				}
				else
					throw ex;
			}
			
			NewQueryform();  
		
			if(_commandLineFile.Length>0)
			{
				string fullName = _commandLineFile;
				string fileName = Path.GetFileName(fullName);
				string line;
				string fileContent = "";
				StreamReader sr = new StreamReader(fullName);

				while ((line = sr.ReadLine()) != null) 
				{
					fileContent += "\n" + line;
				}
				sr.Close();
				sr=null;
				
				this.ActiveQueryForm.Text = fileName + " [" + this.ActiveQueryForm.DatabaseName + "]";
				this.ActiveQueryForm.Content = fileContent;
				_commandLineFile="";
				return;
			}

		}

		private void StartUp()
		{
			if(!File.Exists(StartUpFile))
			{
				timer_StartUp.Enabled=true;
			}
		
		}
		private string GetHelpFilePath()
		{
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs");
			string[] keys = rk.GetValueNames();

			foreach(string keyValue in keys)
			{
				if(keyValue.IndexOf("tsqlref.chm")>0)
					return keyValue;
			}
			return "";
		}
		private string GetEnterpriseManagerFilePath()
		{
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs");
			string[] keys = rk.GetValueNames();

			foreach(string keyValue in keys)
			{
				if(keyValue.IndexOf("Enterprise Manager.MSC")>0)
					return keyValue;
			}
			return "";

		}
		private string GetProfilerFilePath()
		{
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs");
			string[] keys = rk.GetValueNames();

			foreach(string keyValue in keys)
			{
				if(keyValue.IndexOf("profiler.exe")>0)
					return keyValue;
			}
			return "";

		}
		private void timer_StartUp_Tick(object sender, System.EventArgs e)
		{
			try
			{
				SyntaxReader syntaxreader =  new SyntaxReader();
				
				if(syntaxreader.HideStartPage)
				{
					timer_StartUp.Enabled=false;
					return;
				}
				
				statusBar.Panels[3].Text = "Opening Start Page...";
				if(	StartPageResult.IsCompleted)
				{
					timer_StartUp.Enabled=false;
					getXmlStartPageDelegate.EndInvoke(out xmlStartPage,StartPageResult);
					PresentStartUpPage();
				}
			}
			catch 
			{ 
				timer_StartUp.Enabled=false;
				statusBar.Panels[3].Text = "";
			}

		}
		
		private void PresentStartUpPage()
		{
			bool update;
			bool fileExist = File.Exists(StartUpFile);
			statusBar.Panels[3].Text = "Presenting Start Page...";
			try
			{
				XmlNodeList nList =  xmlStartPage.GetElementsByTagName("version");
				string latestVersion = nList[0].Attributes.GetNamedItem("latest").Value;
				string downloadUri	= nList[0].Attributes.GetNamedItem("url").Value;
				version thisVer = new version( System.Windows.Forms.Application.ProductVersion);
				version latestVer = new version(latestVersion);

				statusBar.Panels[3].Text = "";

				if(!thisVer.isLatestVersion(latestVer))
				{
					//nList[0].Attributes.GetNamedItem("title").InnerText = "New release ready for download";
					update = true;
				}
				else
				{
					nList[0].Attributes.GetNamedItem("title").InnerText = "You are using the latest release";
					update = false;
				}

				xmlStartPage.Save(StartUpFile);

				FrmStartPage frmStartPage = new FrmStartPage( this );

				if(!thisVer.isLatestVersion(latestVer))
				{
					frmStartPage.Show(dockManager);
					frmStartPage.ShowContent(downloadUri,StartUpFile,update);
				}
				else if(!fileExist)
				{
					frmStartPage.Show(dockManager);
					frmStartPage.ShowContent(downloadUri,StartUpFile,update);
				}
				else if(_showStartPage)
				{
					frmStartPage.Show(dockManager);
					frmStartPage.ShowContent(downloadUri,StartUpFile,update);
				}
				m_frmStartPage = frmStartPage;
				_showStartPage=false;
			}
			catch
			{
				statusBar.Panels[3].Text = "Could not retrieve new version info.";
				return;
			}

		}

		private void PrintStatus(string msg)
		{
			string FILE_NAME = @"c:\querycommander.log";
			StreamWriter sw = File.AppendText(FILE_NAME);

			sw.WriteLine (msg);
			sw.Close();

		
		}
		#endregion
		#region Public Methods
		public static void CopyEmbeddedResource(string resource, string filepath)
		{
			// Copy embedded uml.dtd to same directory as xmi file.
			System.Reflection.Assembly thisExe;
			thisExe = System.Reflection.Assembly.GetExecutingAssembly();
			System.IO.Stream file = thisExe.GetManifestResourceStream(resource);
			XmlDocument doc = new XmlDocument();
			doc.Load(file);
			doc.Save(filepath);
		}
		
		public void SetPandelInfo()
		{
			try
			{
				string conStr = ActiveQueryForm.dbConnection.ConnectionString;
				int spos = conStr.IndexOf("Data Source=")+12;
				int len = conStr.IndexOf(";",spos)-spos;
				string ds = conStr.Substring(spos,len)+"..";
				SetPandelInfo(ds,ActiveQueryForm.DatabaseName);
			}
			catch
			{
				return;
			}
		
		}
		private void SetPandelInfo(string dataSourse, string database)
		{
			this.panel3.Text = "[" + dataSourse + "]..[" + database + "]";
		}
		public void SetPandelPositionInfo(string line, string col)
		{
			this.panel5.Text = String.Format("Ln {0}  Col {1}",line,col);
		}
		public void RefreashDBConnections()
		{
			DataSourceCollection _dataSourceCollection = DataSourceFactory.GetDataSources();
			bool connect=true;

			// Add new conections
			foreach(DataSource ds in _dataSourceCollection)
			{
				if(ds.IsConnected)
				{
					DBConnection dbConnection = new DBConnection();

					string connn=File.ReadAllText(Path.Combine(Path.GetTempPath(), "SaveFileConnect.txt"));

					dbConnection.ConnectionString = Shared.AppKeyObject.Value;// connn;// "Data Source=.;DATABASE=UTP_WMS_01_28;User Id=sa; Password=112;";// ds.ConnectionString;
					 
					dbConnection.ConnectionName  = ds.Name;
					try
					{
						switch (ds.ConnectionType)
						{
							case DataSource.DBConnectionType.MicrosoftSqlClient:
								dbConnection.Connection = new SqlConnection(dbConnection.ConnectionString);
								break;
							case DataSource.DBConnectionType.MicrosoftOleDb:
								dbConnection.Connection = new System.Data.OleDb.OleDbConnection(ds.ConnectionString);
								break;
							case DataSource.DBConnectionType.OracleOleDb:
								dbConnection.Connection = new Oracle.DataAccess.Client.OracleConnection(ds.ConnectionString);
								break;
							case DataSource.DBConnectionType.MySQL:
								dbConnection.Connection = new MySql.Data.MySqlClient.MySqlConnection(ds.ConnectionString);
								break;
							case DataSource.DBConnectionType.SybaseASE:
								dbConnection.Connection = new Sybase.Data.AseClient.AseConnection(ds.ConnectionString);
								break;

						}
						//dbConnection.Connection = new SqlConnection(ds.ConnectionString);
						dbConnection.Connection.Open();
						dbConnection.IsConnected = true;
						dbConnection.InitialCatalog = ds.InitialCatalog;
						dbConnection.FrienlyName = ds.FriendlyName;
						foreach(DBConnection old in DBConnections)
						{
							if(old.ConnectionName == dbConnection.ConnectionName)
							{
								connect=false;
								break;
							}
						}
						if(connect)
							DBConnections.Add(dbConnection);

						connect=true;

					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
	
				// Remove unused conections
				foreach(DBConnection old in DBConnections)
				{
					connect=false;
					foreach(DataSource newDs in _dataSourceCollection)
					{
						if(newDs.Name == old.ConnectionName)
						{
							connect=true;
							break;
						}
					}
					if(!connect)
						DBConnections.Remove(old);
				}
			}
		}
		public void RefreshDataObjectTree()
		{
			m_FrmDBObjects.RefreashTreeView();
		}
		public void ShowTaskWindow()
		{
			try
			{
				TaskList.Show(dockManager);
			}
			catch
			{
				return;
			}
		}
		public void ShowDBObjects()
		{
			m_FrmDBObjects.Show(dockManager);
		}
		public void ActivateMe(FrmQuery frmQuery)
		{
			ActiveQueryForm = frmQuery;
		}
		
		public void NewQueryform()
		{
			DBConnection dbConnection;
			IDbConnection connection=null;
			string DatabaseName="";

			Application.DoEvents();

			if(QueryForms.Count<=1)
			{
				dbConnection = (DBConnection)this.DBConnections[0];
				connection = dbConnection.Connection;
				DatabaseName = dbConnection.Connection.Database;//  dbConnection.InitialCatalog;
			}
			else
			{
				connection = ActiveQueryForm.dbConnection;
				DatabaseName=ActiveQueryForm.DatabaseName;
			}
			FrmQuery frmquery = new FrmQuery( this );
			
			int count = QueryForms.Count+1;
			string text = "Document" + count.ToString();
			while (FindContent(text) != null)
			{
				count ++;
				text = "Document" + count.ToString();
			}
			frmquery.Text = text;//   File.ReadAllText(Path.Combine(Path.GetTempPath(), "SaveFile.txt")); 
			frmquery.Show(dockManager);
			QueryForms.Add(frmquery);
			frmquery.SetDatabaseConnection(DatabaseName,connection);
			frmquery.qcTextEditor.Text = Shared.SearchDefinition;




		}

		public void PopulateRecentItems()
		{
			if(File.Exists(Application.StartupPath+"\\RecentObjects.xml"))
			{
				// clear old items
				this.menuRecentItems.MenuItems.Clear();

				XmlDocument doc = new XmlDocument();
				doc.Load(Application.StartupPath + "\\RecentObjects.xml");

				XmlNodeList rootNodeList = doc.GetElementsByTagName("recentobjects");

				foreach(XmlNode node in rootNodeList[0].ChildNodes)
				{
					// visualize items
					MenuItem item = new MenuItem(node.InnerText,new EventHandler(this.menuRecentItem_Click));

					if(node.InnerText.ToUpper().Substring(0,2)=="FN")
						menuExtender.SetMenuImage(item,"25");
					else
						menuExtender.SetMenuImage(item,"26");

					this.menuRecentItems.MenuItems.Add(item);
				}
			}
		}
		
		public void AlterDatabaseMenuItem(ArrayList dbArr)
		{
			contextMenuDataBases.MenuItems.Clear();
			foreach(QueryCommander.Database.DB db in dbArr)
			{
				System.Windows.Forms.MenuItem mi = new System.Windows.Forms.MenuItem();
				mi.Text = db.Server+":"+db.Name;
				mi.Click += new System.EventHandler(this.MenuItemDataBases_Click);

				menuExtender.SetMenuImage(mi,"27");

				contextMenuDataBases.MenuItems.Add(mi);
			}
		}
		
		public void AddPluginMenuItem(MenuItem menuItem, object tag)
		{
			this.menuExtender.SetMenuImage(menuItem, null);
			menuItem.Click += new System.EventHandler(PluginMenuItem_Click);
			menuItem_Plugins.MenuItems.Add(menuItem);
			menuItem_Plugins.Visible=true;

			pluginMenuItemObject.Add(menuItem,tag);
		}
		
		public void UpdateEditorSettings()
		{
			QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
			foreach(FrmQuery frm in this.QueryForms)
			{
				frm.qcTextEditor.ShowEOLMarkers=settings.ShowEOLMarkers;
				frm.qcTextEditor.ShowSpaces=settings.ShowSpaces;
				frm.qcTextEditor.ShowTabs=settings.ShowTabs;
				frm.qcTextEditor.ShowMatchingBracket=settings.ShowMatchingBracket;
				frm.qcTextEditor.ShowLineNumbers=settings.ShowLineNumbers;
				frm.qcTextEditor.Font=settings.GetFont();
				frm.qcTextEditor.ShowVRuler=false;
			}
		}

		// Workspaces
		public WorkSpace AddWorkSpace()
		{
			WorkSpace newWs=null;
			FrmAddWorkSpace frm = new FrmAddWorkSpace();
			if(frm.ShowDialogWindow(this)==DialogResult.OK)
			{
				string WSName = frm.txtWorkSpace.Text;
				foreach(WorkSpace ws in workSpaceCollection)
				{
					if(ws.Name==WSName)
					{
						MessageBox.Show("A WorkSpace with the same name already exists. Specify a different name","Add Workspace",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						return null;
					}
				}
				newWs = new WorkSpace();
				newWs.Name=WSName;
				workSpaceCollection.Add(newWs);
				return newWs;
			}
			return null;
		}

		public void DeleteWorkSpace(WorkSpace workSpace)
		{
			for(int i=0;i<workSpaceCollection.Count;i++)
			{
				if(workSpaceCollection[i]==workSpace)
				{
					workSpaceCollection.RemoveAt(i);
					return;
				}
			}
		}
		public WorkSpaceItem AddActiveDocumentToWorkspace(WorkSpace workSpace)
		{
			WorkSpaceItem newWorkSpaceItem=null;
			if(ActiveQueryForm.FileName.Length>0)
				ActiveQueryForm.SaveAs(ActiveQueryForm.FileName);
			else
				SaveDocument();

			if(ActiveQueryForm.FileName.Length==0)
				return null;

			string fileName = ActiveQueryForm.FileName.Substring(ActiveQueryForm.FileName.LastIndexOf(@"\")+1);
			
			newWorkSpaceItem = new WorkSpaceItem();
			newWorkSpaceItem.FileName=fileName;
			newWorkSpaceItem.FilePath=ActiveQueryForm.FileName;

			workSpace.WorkSpaceItems.Add(newWorkSpaceItem);
			return newWorkSpaceItem;
		}
		public WorkSpaceItemCollection AddAllDocumentToWorkspace(WorkSpace workSpace)
		{
			WorkSpaceItemCollection wsic = new WorkSpaceItemCollection();

			foreach(FrmQuery frmQuery in this.QueryForms)
			{
				if(frmQuery.FileName.Length>0)
					frmQuery.SaveAs(ActiveQueryForm.FileName);
				else
					SaveDocument(frmQuery);

				if(frmQuery.FileName.Length==0)
					return null;

				string fileName = frmQuery.FileName.Substring(ActiveQueryForm.FileName.LastIndexOf(@"\")+1);
			
				WorkSpaceItem newWorkSpaceItem = new WorkSpaceItem();
				newWorkSpaceItem.FileName=fileName;
				newWorkSpaceItem.FilePath=frmQuery.FileName;
				wsic.Add(newWorkSpaceItem);

				workSpace.WorkSpaceItems.Add(newWorkSpaceItem);
			}
			return wsic;
		}

		public WorkSpaceItem AddAnyDocumentToWorkSpace(WorkSpace workSpace)
		{
			WorkSpaceItem newWorkSpaceItem=null;
			string directoryPath = Application.StartupPath + @"\Scriptfiles";
			if(!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);


			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory=directoryPath;
			openFileDialog.Filter = "SQL Files|*.sql";
			if(openFileDialog.ShowDialog(this)==DialogResult.OK)
			{

				string fileName = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf(@"\")+1);
			
				newWorkSpaceItem = new WorkSpaceItem();
				newWorkSpaceItem.FileName=fileName;
				newWorkSpaceItem.FilePath=openFileDialog.FileName;

				workSpace.WorkSpaceItems.Add(newWorkSpaceItem);
			}
			return newWorkSpaceItem;
		}
		public void DeleteWorkspaceItem(string workspaceName, string filePath)
		{
			foreach(WorkSpace ws in workSpaceCollection)
			{
				if(ws.Name==workspaceName)
				{
					for(int i=0;i<ws.WorkSpaceItems.Count;i++)
					{
						if(ws.WorkSpaceItems[i].FilePath==filePath)
						{
							ws.WorkSpaceItems.RemoveAt(i);
							return;
						}
					}
					return;
				}
			}
		}
		public void SaveDocument()
		{
			string directoryPath = Application.StartupPath + @"\Scriptfiles";
			if(!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);

			// Create a SaveFileDialog to request a path and file name to save to.
			SaveFileDialog saveFile1 = new SaveFileDialog();

			string defaultFileName ="";
			if(ActiveQueryForm.Text.IndexOf("Document1 [")<0)
				defaultFileName=ActiveQueryForm.Text + ".sql";
			// Initialize the SaveFileDialog to specify the RTF extension for the file.
			saveFile1.InitialDirectory =directoryPath;
			saveFile1.DefaultExt = "*.sql";
			saveFile1.Filter = "SQL Files|*.sql";
			saveFile1.FileName=defaultFileName;
			// Determine if the user selected a file name from the saveFileDialog.
			if(saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
				saveFile1.FileName.Length > 0) 
			{
				// Save the contents of the RichTextBox into the file.
				ActiveQueryForm.SaveAs(saveFile1.FileName);
				ActiveQueryForm.Text=defaultFileName;
			}
		}

		

		
		public void SaveDocument(FrmQuery frmQuery)
		{
			string directoryPath = Application.StartupPath + @"\Scriptfiles";
			if(!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);

			// Create a SaveFileDialog to request a path and file name to save to.
			SaveFileDialog saveFile1 = new SaveFileDialog();

			string defaultFileName ="";
			if(frmQuery.Text.IndexOf("Document1 [")<0)
				defaultFileName=frmQuery.Text + ".sql";
			
			// Initialize the SaveFileDialog to specify the RTF extension for the file.
			saveFile1.InitialDirectory =directoryPath;
			saveFile1.DefaultExt = "*.sql";
			saveFile1.Filter = "SQL Files|*.sql";
			saveFile1.FileName=defaultFileName;
			// Determine if the user selected a file name from the saveFileDialog.
			if(saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
				saveFile1.FileName.Length > 0) 
			{
				// Save the contents of the RichTextBox into the file.
				frmQuery.SaveAs(saveFile1.FileName);
				frmQuery.Text=defaultFileName;
			}
		}
		
		#endregion
		#region Classes
		private class version
		{
			public int Major;
			public int Minor;
			public int Build;
			public int Revision;
			public version(string versionString)
			{
				string delimStr = ".";
				char [] delimiter = delimStr.ToCharArray();
				string [] split = null;
				split = versionString.Split(delimiter);
				int i = split.Length;
				if(split.Length>0)
					Major = Convert.ToInt16(split[0]);
				else
					Major = 0;
				if(split.Length>1)
					Minor = Convert.ToInt16(split[1]);
				else
					Minor = 0;
				if(split.Length>2)
					Build = Convert.ToInt16(split[2]);
				else
					Build = 0;
				if(split.Length>3)
					Revision = Convert.ToInt16(split[3]);
				else
					Revision = 0;

			}
		
			public bool isLatestVersion(version latestVersion)
			{
				if(this.Major<latestVersion.Major) return false;
				if(this.Major>latestVersion.Major) return true;

				if(this.Minor<latestVersion.Minor) return false;
				if(this.Minor>latestVersion.Minor) return true;

				if(this.Build<latestVersion.Build) return false;
				if(this.Build>latestVersion.Build) return true;

				if(this.Revision<latestVersion.Revision) return false;
				if(this.Revision>latestVersion.Revision) return true;

				return true;

			}
		}
		public class DBConnection
		{
			public string ConnectionName = "";
			public string FrienlyName="";
			public string ConnectionString = "";
			public IDbConnection Connection;
			public bool IsConnected; 
			public TreeNode treeNode;
			public string InitialCatalog;
		}
		public delegate void GetXmlStartPageDelegate(string StartUpFileUri, out XmlDocument xmlStartPage) ;
		public class AsyncMethods 
		{
			public void GetXmlStartPage(string StartUpFileUri, out XmlDocument xmlStartPage)
			{
                XmlDocument doc = new XmlDocument();
                
					doc.Load(@"D:\ChromeDownload\QueryCommander_Source_3.0.0.7\QueryCommander\bin\\Debug\startpage.xml");



				xmlStartPage = doc;



                //return xmlStartPage;
            }
        }

		public class PluginMenuItem : MenuItem
		{
			object _tag=null;
			public object Tag
			{
				get{return _tag;}
				set{_tag=value;}
			}
		}
		#endregion	
	}
}
