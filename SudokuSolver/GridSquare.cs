using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class GridSquare : Button
	{
		public int RowID { get; set; }
		public int ColID { get; set; }
		public List<int> Values { get; set; }

		public GridSquare(int row, int col, List<int> values)
		{
			RowID = row;
			ColID = col;
			Values = values;

			this.Text = values.Count > 0 ? ConcatenateValues(values) : string.Empty;
			this.Width = 80;
			this.Height = 80;
			this.Margin = new Padding(1);
			this.Font = new Font("Segoe UI", 13, FontStyle.Bold);
		}
		private String ConcatenateValues(List<int> values)
		{
			string temp = "";
			int j = 0;
			foreach(int i in values)
			{
				temp += values[j++].ToString();
				temp += "  ";
			}
			return temp;
		}
	}

}
