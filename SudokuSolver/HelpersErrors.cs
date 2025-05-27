using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	internal class HelpersErrors
	{
		public static bool IsAllRight(GridSquare[,] gridSquares)
		{
			return CheckRowValues(gridSquares) && CheckColValues(gridSquares) && CheckBoxValues(gridSquares);
		}
		private static bool CheckRowValues(GridSquare[,] gridSquares)
		{
			List<int> valuesList = new List<int>();
			bool flag = true;
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					//gridSquares[row, col].BackColor = Color.White;
					valuesList.Clear();
					if (gridSquares[row, col].Value != 0)
					{
						for (int c = 0; c < 9; c++)
						{
							if (gridSquares[row, c].Value != 0)
							{
								valuesList.Add(gridSquares[row, c].Value);
							}
						}
						if (valuesList.Count > 0)
						{
							flag = flag && valuesList.Count == valuesList.Distinct().Count();
							if (!flag)
							{
								for (int i = 0; i < 9; i++)
								{
									gridSquares[row, i].BackColor = Color.Red;
								}
								return flag;
							}
						}
					}
				}
			}
			return flag;
		}
		private static bool CheckColValues(GridSquare[,] gridSquares)
		{
			List<int> valuesList = new List<int>();
			bool flag = true;
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					//gridSquares[row, col].BackColor = Color.White;
					valuesList.Clear();
					if (gridSquares[row, col].Value != 0)
					{
						for (int r = 0; r < 9; r++)
						{
							if (gridSquares[r, col].Value != 0)
							{
								valuesList.Add(gridSquares[r, col].Value);
							}
							if (valuesList.Count > 0)
							{
								flag = flag && valuesList.Count == valuesList.Distinct().Count();
								if (!flag)
								{
									for (int i = 0; i < 9; i++)
									{
										gridSquares[i, col].BackColor = Color.Red;
									}
									return flag;
								}
							}
						}
					}
				}
			}
			return flag;
		}
		private static bool CheckOneBoxValues(int fila, int columna, GridSquare[,] gridSquares)
		{
			List<int> valuesList = new List<int>();
			bool flag = true;
			for (int row = fila; row < fila + 3; row++)
			{
				for (int col = columna; col < columna + 3; col++)
				{
					//gridSquares[row, col].BackColor = Color.White;
					valuesList.Clear();
					if (gridSquares[row, col].Value != 0)
					{
						for (int r = fila; r < fila + 3; r++)
						{
							for (int c = columna; c < columna + 3; c++)
							{
								if (gridSquares[r, c].Value != 0)
								{
									valuesList.Add(gridSquares[r, c].Value);
								}
								if (valuesList.Count > 0)
								{
									flag = flag && valuesList.Count == valuesList.Distinct().Count();
									if (!flag)
									{
										for (int i = fila; i < fila + 3; i++)
										{
											for (int j = columna; j < columna + 3; j++)
											{
												gridSquares[i, j].BackColor = Color.Red;
											}
										}
										return flag;
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}
		private static bool CheckBoxValues(GridSquare[,] gridSquares)
		{
			return CheckOneBoxValues(0, 0, gridSquares) &&
			CheckOneBoxValues(3, 0, gridSquares) &&
			CheckOneBoxValues(6, 0, gridSquares) &&
			CheckOneBoxValues(0, 3, gridSquares) &&
			CheckOneBoxValues(3, 3, gridSquares) &&
			CheckOneBoxValues(6, 3, gridSquares) &&
			CheckOneBoxValues(0, 6, gridSquares) &&
			CheckOneBoxValues(3, 6, gridSquares) &&
			CheckOneBoxValues(6, 6, gridSquares);
		}
	}
}
