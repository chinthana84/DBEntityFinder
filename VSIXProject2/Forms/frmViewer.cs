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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsSyntaxHighlighter;

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
                if (string.IsNullOrEmpty(selection))
                {
                    return;
                }
                DataTable dataTable = new DBService(appKeyObject).GetResults(selection);
                dataGridView1.DataSource = dataTable;
                sysntaxHighli();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void sysntaxHighli()
        {
            var syntaxHighlighter = new SyntaxHighlighter(richTextBox1);

            // multi-line comments
            syntaxHighlighter.AddPattern(new PatternDefinition(new Regex(@"/\*(.|[\r\n])*?\*/", RegexOptions.Multiline | RegexOptions.Compiled)), new SyntaxStyle(Color.DarkSeaGreen, false, true));
            // singlie-line comments
            syntaxHighlighter.AddPattern(new PatternDefinition(new Regex(@"--.*?$", RegexOptions.Multiline | RegexOptions.Compiled)), new SyntaxStyle(Color.Green, false, true));
            // numbers
            syntaxHighlighter.AddPattern(new PatternDefinition(@"\d+\.\d+|\d+"), new SyntaxStyle(Color.Purple));
            // double quote strings
            syntaxHighlighter.AddPattern(new PatternDefinition(@"\""([^""]|\""\"")+\"""), new SyntaxStyle(Color.Red));
            // single quote strings
            //  syntaxHighlighter.AddPattern(new PatternDefinition(@"\'([^']|\'\')+\'"), new SyntaxStyle(Color.Salmon));
            // keywords1
            syntaxHighlighter.AddPattern(new PatternDefinition("for", "foreach", "int", "var"), new SyntaxStyle(Color.Blue));
            // keywords2
            syntaxHighlighter.AddPattern(new CaseInsensitivePatternDefinition("ADD",
"ALL",
"ALTER",
"AND",
"ANY",
"AS",
"ASC",
"AUTHORIZATION",
"BACKUP",
"BEGIN",
"BETWEEN",
"BREAK",
"BROWSE",
"BULK",
"BY",
"CASCADE",
"CASE",
"CHECK",
"CHECKPOINT",
"CLOSE",
"CLUSTERED",
"COALESCE",
"COLLATE",
"COLUMN",
"COMMIT",
"COMPUTE",
"CONSTRAINT",
"CONTAINS",
"CONTAINSTABLE",
"CONTINUE",
"CONVERT",
"CREATE",
"CROSS",
"CURRENT",
"CURRENT_DATE",
"CURRENT_TIME",
"CURRENT_TIMESTAMP",
"CURRENT_USER",
"CURSOR",
"DATABASE",
"DBCC",
"DEALLOCATE",
"DECLARE",
"DEFAULT",
"DELETE",
"DENY",
"DESC",
"DISK",
"DISTINCT",
"DISTRIBUTED",
"DOUBLE",
"DROP",
"DUMP",
"ELSE",
"END",
"ERRLVL",
"ESCAPE",
"EXCEPT",
"EXEC",
"EXECUTE",
"EXISTS",
"EXIT",
"EXTERNAL",
"FETCH",
"FILE",
"FILLFACTOR",
"FOR",
"FOREIGN",
"FREETEXT",
"FREETEXTTABLE",
"FROM",
"FULL",
"FUNCTION",
"GOTO",
"GRANT",
"GROUP",
"HAVING",
"HOLDLOCK",
"IDENTITY",
"IDENTITY_INSERT",
"IDENTITYCOL",
"IF",
"IN",
"INDEX",
"INNER",
"INSERT",
"INTERSECT",
"INTO",
"IS",
"JOIN",
"KEY",
"KILL",
"LEFT",
"LIKE",
"LINENO",
"LOAD",
"MERGE",
"NATIONAL",
"NOCHECK",
"NONCLUSTERED",
"NOT",
"NULL",
"NULLIF",
"OF",
"OFF",
"OFFSETS",
"ON",
"OPEN",
"OPENDATASOURCE",
"OPENQUERY",
"OPENROWSET",
"OPENXML",
"OPTION",
"OR",
"ORDER",
"OUTER",
"OVER",
"PERCENT",
"PIVOT",
"PLAN",
"PRECISION",
"PRIMARY",
"PRINT",
"PROC",
"PROCEDURE",
"PUBLIC",
"RAISERROR",
"READ",
"READTEXT",
"RECONFIGURE",
"REFERENCES",
"REPLICATION",
"RESTORE",
"RESTRICT",
"RETURN",
"REVERT",
"REVOKE",
"RIGHT",
"ROLLBACK",
"ROWCOUNT",
"ROWGUIDCOL",
"RULE",
"SAVE",
"SCHEMA",
"SECURITYAUDIT",
"SELECT",
"SEMANTICKEYPHRASETABLE",
"SEMANTICSIMILARITYDETAILSTABLE",
"SEMANTICSIMILARITYTABLE",
"SESSION_USER",
"SET",
"SETUSER",
"SHUTDOWN",
"SOME",
"STATISTICS",
"SYSTEM_USER",
"TABLE",
"TABLESAMPLE",
"TEXTSIZE",
"THEN",
"TO",
"TOP",
"TRAN",
"TRANSACTION",
"TRIGGER",
"TRUNCATE",
"TRY_CONVERT",
"TSEQUAL",
"UNION",
"UNIQUE",
"UNPIVOT",
"UPDATE",
"UPDATETEXT",
"USE",
"USER",
"VALUES",
"VARYING",
"VIEW",
"WAITFOR",
"WHEN",
"WHERE",
"WHILE",
"WITH",
"WITHIN GROUP",
"WRITETEXT"
), new SyntaxStyle(Color.Blue, true, false));
            // operators
            syntaxHighlighter.AddPattern(new PatternDefinition("+", "-", ">", "<", "&", "|"), new SyntaxStyle(Color.Brown));
            syntaxHighlighter.ReHighlight();


        }
        private void frmViewer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sysntaxHighli();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
