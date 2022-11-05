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
using System.Diagnostics;
using QueryCommander.WinGui;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for Exception.
	/// </summary>
	/// 
	/// <revision author="wmmabet" date="2004-01-13">Removed warning/fixed xml-comments.</revision>
	public abstract class ExceptionHandler
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ExceptionHandler()
		{
		}

		/// <summary>
		/// Error hanlding function.
		/// </summary>
		/// <param name="currentForm">Current form</param>
		/// <param name="e">Exception</param>
		public static void ErrorHandler(System.Windows.Forms.Form currentForm, Exception e)
		{
			if(e is System.Reflection.TargetInvocationException)
				e = e.InnerException;
                

			FrmExceptionMessage frm = new FrmExceptionMessage(currentForm, e);
			frm.ShowDialog();
		}
	}
}
