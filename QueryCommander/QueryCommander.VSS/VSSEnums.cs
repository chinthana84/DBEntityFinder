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

namespace QueryCommander.VSS
{
	/// <summary>
	/// Summary description for VSSenums.
	/// </summary>
	public class VSSEnums
	{
		public enum CheckingOutVersion{
			Ignore = 0,
			Number = 1,
			Label = 2
		};

		//Constants used to identify items in the CheckOut data array
		public enum CheckoutItems{
			UserName = 0,
			Date = 1,
			Version = 2,
			Computer = 3,
			CheckOutFolder = 4,
			Project = 5,
			Comment = 6,
			ItemName = 7
		};

		//Constants used to identify items in the CheckOuts ListBox Columns
		public enum CheckOuts{
			UserName = 0,
			CheckOutFolder = 1,
			Version = 2,
			Date = 3
		};

		//Constants used to identify the current Active Control (file list or project tree)
		public enum Control{
			ProjectTree = 0,
			FileList = 1
		};

		//Constants used to identify items in the File List Array  
		public enum FileList{
			Name = 0,
			ItemType = 1,
			VersionNumber = 2,
			CheckOutState = 3,
			CheckOutFolder = 4,
			CheckOutComment = 5,
			CheckOutUserName = 6,
			CheckedOutMultiple = 7,
			DateTime = 8,
			IsShared = 9,
			IsDeleted = 10
		};

		//Constants used to identify items in the FileList Columns
		public enum FileListColumn{
			Name = 0,
			Version = 1,
			User = 2,
			Date = 3,
			CheckOutFolder = 4
		};

		//Constants used when getting items from History
		public enum GettingVersion{
			Ignore = 0,
			Number = 1,
			Label = 2
		};

		//Constants used to identify items in the History Array
		public enum HistoryItems{
			VersionNumber = 0,
			UserName = 1,
			Date = 2,
			Action = 3,
			Label = 4,
			LabelComment = 5,
			Name = 6,
			Comment = 7,
			Spec = 8
		};

		//File and Project Glyph Constants
		public enum ItemGlyph{
			ProjectClosedGlyph = 0,
			ProjectOpenGlyph = 1,
			FileCheckedOutGlyph = 2,
			FileCheckedOutExclusiveGlyph = 3,
			FileCheckedOutSharedMultipleGlyph = 4,
			FileCheckedOutMultipleGlyph = 5,
			FileCheckedOutSharedGlyph = 6,
			FileGlyph = 7,
			FileSharedGlyph = 8
		};

		//Constants used to identify items the Status Bar panels
		public enum StatusBarPanels{
			Ready = 0,
			UserName = 1,
			Blank = 2,
			Sort = 3,
			ItemCount = 4
		};

		//Constants used to identify Tab Pages
		public enum TabPages{
			General = 0,
			CheckOutStatus = 1,
			Links = 2,
			DeletedItems = 3,
			DiffView = 1,
			Warnings = 2,
			Logging = 3,
			Admin = 4
		}

		//Constants used to identify items for the Users List Columns
		public enum UserItems{
			UserName = 0,
			UserRights = 1,
			LoggedIn = 2
		};
	}
}
