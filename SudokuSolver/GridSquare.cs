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
		//public Action<int, int, int> OnValueSet; // (row, col, value)
		public int RowID { get; set; }
		public int ColID { get; set; }
		public List<int> Values { get; set; }
		public int Value { get; set; }

		public GridSquare(int row, int col, List<int> values, int value)
		{
			RowID = row;
			ColID = col;
			Values = values;
			Value = value;

			this.Text = FormatValues(Values, Value);
			this.Font = new Font("Consolas", 12, FontStyle.Bold);
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Size = new Size(75, 75);
			this.Margin = new Padding(1);
			Value = value;
		}
		public static String FormatValues(List<int> values, int value)
		{
			string temp = "";
			int tempVal = 0;
			int j = 0;
			if (values.Count != 0)
			{
				foreach (int i in values)
				{
					tempVal = values[j++];
					if (tempVal == 0)
					{
						temp += " ";
					}
					else
					{
						temp += tempVal.ToString();
					}
					temp += " ";
					if (j % 3 == 0)
					{
						temp += "\n";
					}
				}
			}
			else
			{
				temp = value.ToString();
			}				
			return temp;
		}
		public void SetSelected(bool selected)
		{
			this.BackColor = selected ? Color.LightBlue : SystemColors.Control;
		}

		public void SetValue(int value)
		{
			Value = value;
			Values.Clear();
			if (Value == 0)
			{
				Values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // placeholder
			}
			this.Text = FormatValues(Values, Value);
			if (value > 0)
			{ 
				//OnValueSet?.Invoke(RowID, ColID, value);
			}
		}

	}

}
