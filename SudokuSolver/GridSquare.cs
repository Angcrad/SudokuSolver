using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class GridSquare : Button
	{
		public int RowID { get; set; }
		public int ColID { get; set; }

		public GridSquare(int row, int col, int value)
		{
			RowID = row;
			ColID = col;
			this.Text = value.ToString();
			this.Width = 40;
			this.Height = 40;
			this.Margin = new Padding(1);
			this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
		}
	}
}
