

using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using ServiceLayer;
using ServiceLayer.Implementations;
using ServiceLayer.Interfaces;
using SharedLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using VSIXProject2.Forms;
using Task = System.Threading.Tasks.Task;

namespace VSIXProject2
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class Command1
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1818c514-d10e-496b-9367-5b9dd9bff6cb");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command1"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Command1 Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }



        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Command1(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            //ThreadHelper.ThrowIfNotOnUIThread();
            DTE dte = Package.GetGlobalService(typeof(SDTE)) as DTE;

            try
            {
                
                AppKeyObject appKeyObject = null;
                appKeyObject = GetAppKeyObject();
                if (appKeyObject == null)
                {
                    throw new ApplicationException("Keynot found");
                }

                string selection = await GetSelection(ServiceProvider);
                if (string.IsNullOrEmpty(selection))
                {
                    return;
                }

                DataTable dataTable = new DBService(appKeyObject).GetDBObject(selection);
               
                if (dataTable.Rows.Count == 1)
                {
                    frmViewer frmViewer = new frmViewer(appKeyObject);
                    frmViewer.richTextBox1.Text = dataTable.Rows[0]["text"].ToString();
                    frmViewer.Text = appKeyObject.Value + " " + appKeyObject.dbType;
                    frmViewer.ShowDialog();
                }
                else if (dataTable.Rows.Count > 1)
                {
                    MessageBox.Show($"multiple objects found in same name");
                }
                else
                {
                    MessageBox.Show($"{ selection}  object not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private   AppKeyObject GetAppKeyObject()
        {
            List<string> allFiles = new List<string>();
            bool isFound = false;
            AppKeyObject appKeyObject = null;

            IVsSolution solution = (IVsSolution)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(IVsSolution));
            solution.GetSolutionInfo(out string solutionDirectory, out string solutionName, out string solutionDirectory2);
            var solutionPath = solutionDirectory;// + System.IO.Path.GetFileNameWithoutExtension(solutionName);

            allFiles.AddRange(Directory.GetFiles(solutionPath, "web.config", SearchOption.AllDirectories));
            allFiles.AddRange(Directory.GetFiles(solutionPath, "app.config", SearchOption.AllDirectories));

            foreach (var file in allFiles)
            {
                if (isFound) break;
                string contents = File.ReadAllText(file);
                var xml = XElement.Parse(contents);
                try
                {
                    if (xml.Element("findObjectVX") != null)
                    {
                        foreach (var item in xml.Element("findObjectVX").Descendants("add"))
                        {
                            if (item.Attribute("key").Value == "FINDSPKEY.NET.ADDON")
                            {
                                appKeyObject = new AppKeyObject();
                                appKeyObject.Key = item.Attribute("key").Value;
                                appKeyObject.Value = item.Attribute("value").Value;
                                appKeyObject.dbType = item.Attribute("dbType").Value;
                                break;
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

            return appKeyObject;
        }

        private async Task<string> GetSelection(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider)
        {
            var service = await serviceProvider.GetServiceAsync(typeof(SVsTextManager));
            var textManager = service as IVsTextManager2;
            IVsTextView view;
            int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out view);

            view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn);//end could be before beginning
            var start = new TextViewPosition(startLine, startColumn);
            var end = new TextViewPosition(endLine, endColumn);

            view.GetSelectedText(out string selectedText);

            TextViewSelection selection = new TextViewSelection(start, end, selectedText);
            return selection.Text;
        }
    }
}
