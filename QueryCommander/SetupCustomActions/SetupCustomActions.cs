using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;

namespace SetupCustomActions
{

	/// <summary>
	/// Summary description for SetupCustomActions.
	/// </summary>
	[RunInstaller(true)]
	public class SetupCustomActions : System.Configuration.Install.Installer
	{

		protected override void OnCommitted(IDictionary savedState)
		{
			base.OnCommitted (savedState);
//			string filePath = Context.Parameters["targetDir"] + @"\QueryCommander.exe";
//			System.Windows.Forms.MessageBox.Show(filePath);
//			string filePath = Context.Parameters["targetDir"] + @"\QueryCommander.exe";
//			System.Diagnostics.Process.Start("notepad.exe");
		}

		protected override void OnAfterInstall(IDictionary savedState)
		{
			base.OnAfterInstall (savedState);
			string filePath = Context.Parameters["targetDir"] + "QueryCommander.exe";
			System.Diagnostics.Process.Start(filePath);
		}


		public override void Install(IDictionary stateSaver)
		{
			base.Install (stateSaver);

		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetupCustomActions()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
