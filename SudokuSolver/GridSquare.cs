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

			this.Text = FormatValues(values);
			this.Font = new Font("Consolas", 12, FontStyle.Bold);
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Size = new Size(75, 75);
			this.Margin = new Padding(1);
		}
		private String FormatValues(List<int> values)
		{
			string temp = "";
			int j = 0;
			foreach(int i in values)
			{
				temp += values[j++].ToString();
				temp += " ";
				if (j % 3 == 0)
				{
					temp += "\n";
				}
			}
			return temp;
		}
	}

}
