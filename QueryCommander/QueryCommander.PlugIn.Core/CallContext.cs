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
using System.Data;
using System.Data.SqlClient;

namespace QueryCommander.PlugIn.Core
{
	/// <summary>
	/// CallContext encapsulates QueryCommander properties.
	/// </summary>
	public class CallContext
	{
		private System.Data.IDbConnection _dbConnection;
		private string _queryWindowContext;
		private DataSet _resultDataSet = null;

		/// <summary>
		/// Only used by QueryCommander
		/// </summary>
		/// <param name="dbConnection">Active QueryCommander SqlConnection</param>
		/// <param name="queryWindowContext">Content of the current query window</param>
		/// <param name="resultDataSet">Only set using the IPlugin_OnAfterQueryExecution1 interface</param>
		public CallContext(System.Data.IDbConnection dbConnection,
			string queryWindowContext,
			DataSet resultDataSet)
		{
			this._dbConnection=dbConnection;
			this._queryWindowContext=queryWindowContext;
			this._resultDataSet=resultDataSet;
		}

		/// <summary>
		/// Active QueryCommander SqlConnection
		/// </summary>
		public System.Data.IDbConnection DbConnection
		{
			get{return _dbConnection;}
		}

		/// <summary>
		/// Content of the current query window
		/// </summary>
		public string QueryWindowContext
		{
			get{return _queryWindowContext;}
		}

		/// <summary>
		/// Only set using the IPlugin_OnAfterQueryExecution1 interface
		/// </summary>
		public DataSet ResultDataSet
		{
			get{return _resultDataSet;}
		}
	}
}
