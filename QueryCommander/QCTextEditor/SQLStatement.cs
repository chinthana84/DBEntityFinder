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
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using QueryCommander.General;

namespace QueryCommander.SQL
{
	/// <summary>
	/// SQLStatement is hanling the capturing of individual statemets.
	/// </summary>
	public class SQLStatement
	{
		#region constructor
		public SQLStatement(string text, int startPos, SearchOrder searchOrder)
		{
			int count;
			int endPosition=0;
			int NumberOfParamethers=0;
			_lastStartPos=startPos;
			_stopStatements = new ArrayList( (ICollection)_stops);

			if(searchOrder == SearchOrder.desc)
				_startPosition = text.ToUpper().IndexOf("SELECT");
			else
			{
				_text = text;
				count = ParseText();
				for(int i=count;i>0;i--)
				{
					if(_words[i].Position <= startPos && (_words[i].Position + _words[i].Length) >= startPos)
					{
						startPos = _words[i].Position+_words[i].Length;
						break;
					}
				}
				_text = text.Substring(0,startPos);
				count = ParseText();
				
				//Find start position
				for(int i=count;i>0;i--)
				{
					NumberOfParamethers += GetNumberOfParamethers(_words[i].Word);
					if(_stopStatements.Contains(_words[i].Word.ToUpper()) && NumberOfParamethers==0)
					{
						_startPosition = _words[i].Position;
						break;
					}
				}

				_text=text;
				count = ParseText();
				endPosition=_text.Length-_startPosition;
				//Find end position
				for(int i=0;i<count;i++)
				{
					NumberOfParamethers += GetNumberOfParamethers(_words[i].Word);
					if(_stopStatements.Contains(_words[i].Word.ToUpper()) && NumberOfParamethers==0 && _words[i].Position>_startPosition)
					{
						endPosition = _words[i].Position-_startPosition;
						break;
					}
				}
			}
			if(_startPosition<0)
				return;

			_text = text.Substring(_startPosition,endPosition);
			count = ParseText();
			_isStatement=true;
			CollectAliases(count);
		}

		#endregion
		#region private members
		string _text;
		WordAndPosition[] _words  = new WordAndPosition[20000];
		int _startPosition =0;
		int _endPosition=0;
		int _lastStartPos=0;
		bool _isStatement;
		ArrayList _stopStatements;
		Hashtable _aliases = new Hashtable();
		SyntaxReader _syntaxReader = new SyntaxReader();
		
		
		string[] _stops = new string[21] { "DECLARE","SET", "CREATE", "ALTER", "TRUNCATE", "IF", 
											 "ELSE", "ELSEIF", "WHILE", "BEGIN", "END", "EXEC", "DROP", "ROLLBACK", "COMMIT", "GOTO", "GO",
											 "UPDATE", "INSERT", "DELETE", "SELECT"};
		#endregion
		#region public members
		public string Statement
		{
			get{return _text.Substring(0,_endPosition);}
		}
		public int StartPosition
		{
			get {return _startPosition + _lastStartPos;}
		}
		public int EndPosition
		{
			get {return _endPosition + _lastStartPos;}
		}
		public bool IsStatement
		{
			get
			{
				return _isStatement;
			}
		}
		public ArrayList AliasList = new ArrayList();
		public enum SearchOrder{asc, desc}
		#endregion
		#region private methods
		private int GetNumberOfParamethers(string word)
		{
			int openParamether = word.Length - (word.Length - word.Replace("(","").Length);
			int closeParamether = word.Length - (word.Length - word.Replace(")","").Length);
			return openParamether-closeParamether;
		}
		private int ParseText()
		{
//			NumberOfParamethers += GetNumberOfParamethers(_words[i].Word);
//			if(_stopStatements.Contains(_words[i].Word.ToUpper()) && NumberOfParamethers==0)

			int NumberOfParamethers=0;
			int count=0;
			bool cont = true;
			int endCount = SplitText(_text);
			if(endCount==0)
				return 0;

			string currentWord=_words[count].Word;
			count++;
			string tmp = _words[count].Word.ToUpper();

			while(cont)
			{
				NumberOfParamethers += GetNumberOfParamethers(_words[count].Word);
				count++;
				if(_words[count+1].Word==null)
				{
					count++;
					break;
				}
				if(NumberOfParamethers==0)
				{
					if(_stopStatements.Contains(_words[count].Word.ToUpper()))
						cont = false;
				}
				if(count >= endCount)
				{
					cont = false;
				}
			}

//			while(!_stopStatements.Contains(_words[count].Word.ToUpper())  && 
//					count < endCount  && NumberOfParamethers==0)
//			{
//				NumberOfParamethers += GetNumberOfParamethers(_words[count].Word);
//				count++;
//				if(_words[count+1].Word==null)
//				{
//					count++;
//					break;
//				}
//			}
			_endPosition = _words[count-1].Position + _words[count-1].Length;
			return endCount-1;
		}
	
		private int SplitText(string s)
		{
			_words.Initialize();
			int count = 0;
			
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch()) 
			{
				_words[count].Word = m.Value;
				_words[count].Position = m.Index;
				_words[count].Length = m.Length;
				count++;
			}
			return count;
		}
		private void CollectAliases(int count)
		{
			for(int i=0;i<count;i++)
			{
				if((_words[i].Word.ToUpper()=="JOIN" || _words[i].Word.ToUpper()=="FROM" || _words[i].Word.ToUpper()==","))
				{
					if(_words[i+2].Word == null)
						return;
					if(!_syntaxReader.IsReservedWord(_words[i+2].Word) && _words[i+2].Word.Length>0)
					{
						string aliasName= _words[i+2].Word.ToUpper();
						string tableName= _words[i+1].Word.ToUpper();

						if(aliasName==".")
						{
							aliasName=_words[i+4].Word.ToUpper();
							tableName=_words[i+3].Word.ToUpper();
						}

						//if(!_aliases.Contains(_words[i+2].Word))
						//Alias Found
						if(!_aliases.Contains(aliasName))
						{
							_aliases.Add(aliasName,tableName);
							AliasList.Add(new Alias(aliasName,tableName));
						}
						i=i+2;
					}
					else if(_words[i+2].Word.ToUpper()=="AS" && _words[i+3].Word.Length>0)
					{
						string aliasName= _words[i+3].Word.ToUpper();
						string tableName= _words[i+1].Word.ToUpper();

						if(aliasName==".")
						{
							aliasName=_words[i+5].Word.ToUpper();
							tableName=_words[i+3].Word.ToUpper();
						}

						//Alias Found
						if(!_aliases.Contains(aliasName))
						{
							_aliases.Add(aliasName,tableName);
							AliasList.Add(new Alias(aliasName,tableName));
						}
						i=i+2;
					}
				}
			}
		}
		#endregion
		#region public methods
		public string GetAliasTableName(string alias)
		{
			if(!_aliases.Contains(alias.ToUpper()))
				return alias;

			string TableName = (string)_aliases[alias.ToUpper()];

			if(TableName.Length>0)
				return TableName;

			return alias;
		}
		#endregion
	}
	#region Classes
	public class SQLStatements :ArrayList
	{
		public SQLStatements(string text)
		{
			int nextStartPos = 0;

			SQLStatement statement = new SQLStatement(text.Substring(nextStartPos),nextStartPos,SQLStatement.SearchOrder.desc);
			nextStartPos = statement.EndPosition;
			if(statement.IsStatement)
				

				
				while (statement.IsStatement)
				{
					this.Add(statement);
					statement = new SQLStatement(text.Substring(nextStartPos),nextStartPos,SQLStatement.SearchOrder.desc);
					nextStartPos = statement.EndPosition;
				}

		}

		public void Add(SQLStatement statement)
		{
			base.Add (statement);
		}

	}
	
	public class Alias
	{
		public Alias(string alias, string table)
		{
			AliasName = alias;
			TableName = table;

		}

		public string AliasName;
		public string TableName;
	}
	#endregion
}
