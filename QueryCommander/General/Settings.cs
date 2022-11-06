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
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace QueryCommander.Config
{
	/// <summary>
	/// Summary description for TextEditorControlWrapperSettings.
	/// </summary>
	[Serializable]
	public class Settings
	{
		public bool ShowEOLMarkers=false;
		public bool ShowSpaces=false;
		public bool ShowTabs=false;
		public bool ShowLineNumbers=true;
		public bool ShowMatchingBracket=true;
		
		public string fontFamily;
		public GraphicsUnit fontGraphicsUnit;
		public float fontSize;
		public FontStyle fontStyle;

		public bool RunWithIOStatistics=false;
		public int  DifferencialPercentage=101;
		public bool  ShowFrmDocumentHeader=true;

		public bool ShowStartPage=false;
		
		public bool Exists()
		{
			string filepath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings.config");
			return File.Exists(filepath);
		}
		public static Settings Load()
		{
			Settings _settings=new Settings();
			try
			{
				string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
				//This will strip just the working path name:
				//C:\Program Files\MyApplication
				string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

				string filename = System.IO.Path.Combine(strWorkPath, "Settings.config");

		//		string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings.config");
				XmlSerializer ser = new XmlSerializer(typeof(Settings));
				TextReader reader = new StreamReader(filename);
				_settings = (Settings)ser.Deserialize(reader);
				reader.Close();
				return _settings;
			}
			catch(Exception ex)
			{
				
				_settings.ShowEOLMarkers=false;
				_settings.ShowSpaces=false;
				_settings.ShowTabs=false;
				return _settings;
			}
			
		}
		public void Save()
		{
			//string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings.config");
			//XmlSerializer ser = new XmlSerializer(typeof(Settings));
			//TextWriter writer = new StreamWriter(filename);
			//ser.Serialize(writer, this);
			//writer.Close();
		}
		
		public Font GetFont()
		{
			return new Font(this.fontFamily,
				this.fontSize,
				this.fontStyle,
				this.fontGraphicsUnit);

		}
	}
}
