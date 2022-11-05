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

namespace QueryCommander.PlugIn.Core
{
	/// <summary>
	/// Summary description for Common.
	/// </summary>
	public abstract class Common
	{
		/// <summary>
		/// ExecutionType instructs QueryCommander what to do with the result. 
		/// None					= Executing this plugin will have no effect on QueryCommander.
		/// InsertAtPoint			= The return of this plugin (Execute) is ment to be inserted at the current cursor posistion.  			
		/// InsertToNewQueryWindow	= The return of this plugin (Execute) is ment to be inserted into a new query window.
		/// ReplaceToQueryWindow	= The return of this plugin (Execute) is ment to replace the content in the active query window. 
		/// </summary>
		public enum ExecutionTypes
		{
			/// <summary>
			/// Executing this plugin will have no effect on QueryCommander.
			/// </summary>
			None,
			/// <summary>
			/// The return of this plugin (Execute) is ment to be inserted at the current cursor posistion.
			/// </summary>
			InsertAtPoint,
			/// <summary>
			/// The return of this plugin (Execute) is ment to be inserted into a new query window.
			/// </summary>
			InsertToNewQueryWindow,
			/// <summary>
			/// The return of this plugin (Execute) is ment to replace the content in the active query window. 
			/// </summary>
			ReplaceToQueryWindow
		}

		/// <summary>
		/// Only used by QueryCommander
		/// </summary>
		public enum TriggerTypes
		{
			/// <summary>
			/// Only used by QueryCommander
			/// </summary>
			OnMenuClick,
			/// <summary>
			/// Only used by QueryCommander
			/// </summary>
			OnBeforeQueryExecution,
			/// <summary>
			/// Only used by QueryCommander
			/// </summary>
			OnAfterQueryExecution,
		}
	}
}
