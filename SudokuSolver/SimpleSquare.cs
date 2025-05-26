using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class SimpleSquare : Button
	{
		public int RowID { get; set; }
		public int ColID { get; set; }
		public int Value { get; set; }

		public SimpleSquare(int row, int col, int value)
		{
			RowID = row;
			ColID = col;
			Value = value;

			this.Text = value.ToString();
			this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Size = new Size(75, 75);
			this.Margin = new Padding(1);
		}
	}
}
