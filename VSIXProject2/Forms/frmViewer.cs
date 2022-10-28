using ServiceLayer.Implementations;
using ServiceLayer.Interfaces;
using SharedLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSIXProject2.Forms
{
    public partial class frmViewer : Form
    {
        public AppKeyObject appKeyObject { get; set; }
        public frmViewer(AppKeyObject t)
        {
            InitializeComponent();
            this.appKeyObject = t;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                string selection = richTextBox1.SelectedText;
                IStoredProcedureService i = new StoredProcedureService(appKeyObject.dbType);
                DataTable dataTable = i.ExecuteQuery(selection, appKeyObject);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
