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
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace QueryCommander.VSS
{
	/// <summary>
	/// Summary description for VSSConnectionCollection.
	/// </summary>
	[Serializable]
	public class VSSConnectionCollection :CollectionBase
	{
		//public virtual void Add(VSSConnection vssConnection)
		public virtual int Add(VSSConnection vssConnection)
		{
			return this.List.Add(vssConnection);
		}		
		public virtual VSSConnection this[int Index]
		{
			get{return (VSSConnection)this.List[Index];}
		}
		public VSSConnection FindByDataBase(string server, string database)
		{
			foreach(VSSConnection vssConnection in this)
			{
				if(vssConnection.Server.ToUpper()==server.ToUpper() && vssConnection.Database.ToUpper()==database.ToUpper())
					return vssConnection;
			}
			return null;
		}
	}
	public class VSSConnectionCollectionFactory
	{
		public static void Save(VSSConnectionCollection vssConnectionCollection)
		{
			try
			{
				string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "VSSConnectionCollection.config");
				XmlSerializer ser = new XmlSerializer(typeof(VSSConnectionCollection));
				TextWriter writer = new StreamWriter(filename);
				ser.Serialize(writer, vssConnectionCollection);
				writer.Close();
			}
			catch(Exception ex)
			{
				return;
			}
		}
		public static VSSConnectionCollection Load()
		{
			try
			{
				VSSConnectionCollection vssConnectionCollection;
				string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "VSSConnectionCollection.config");
				XmlSerializer ser = new XmlSerializer(typeof(VSSConnectionCollection));
				TextReader reader = new StreamReader(filename);
				vssConnectionCollection = (VSSConnectionCollection)ser.Deserialize(reader);
				reader.Close();
				return vssConnectionCollection;
			}
			catch
			{
				return new VSSConnectionCollection();
			}
		}
	}

	[Serializable]
	public class VSSHitoryItem
	{
		public string Text;
		public string Username;
		public string Date;
		public string Action;
	}
	/// <summary>
	/// Summary description for VSSConnectionCollection.
	/// </summary>
	[Serializable]
	public class VSSHitoryItemCollection :CollectionBase
	{
		public virtual int Add(VSSHitoryItem vssHitoryItem)
		{
			return this.List.Add(vssHitoryItem);
		}		
		public virtual VSSHitoryItem this[int Index]
		{
			get{return (VSSHitoryItem)this.List[Index];}
		}
		
	}
}



