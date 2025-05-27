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
		public Font Fuente { get; set; }
		public bool IsSelected { get; set; }

		public GridSquare(int row, int col, List<int> values, int value, Font font, bool isSelected)
		{
			RowID = row;
			ColID = col;
			Values = values;
			Value = value;
			Fuente = font;
			IsSelected = isSelected;
			this.Text = FormatValues(Values, Value);
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Size = new Size(75, 75);
			this.Margin = new Padding(1);
			Value = value;
		}
		public static String FormatValues(List<int> values, int value)
		{
			string temp = "";
			int j = 0;
			if (values.Count != 0)
			{
				foreach (int i in values)
				{
					while (++j < i)
					{
						temp += "  ";
						if (j % 3 == 0)
						{
							temp += "\n";
						}
					}
					//tempVal = values[j++];
					if (values.Contains(j))
					{
						temp += j.ToString();
						temp += " ";
					}
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
			while (temp.Length > 1 && temp.Length < 20)
			{
				temp += " ";
			}
			return temp;
		}
		public void SetSelected(bool selected)
		{
			if(selected)
			{
				this.BackColor = Color.LightBlue;
				IsSelected = true;
			}
			else
			{
				this.BackColor = Color.White;
				IsSelected = false;
			}
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
