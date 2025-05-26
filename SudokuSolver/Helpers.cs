using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class Helpers
	{
		public static void ReSetCandidates(ref GridSquare[,] gridSquares)
		{
			for (int r = 0; r < 9; r++)
			{
				for (int c = 0; c < 9; c++)
				{
					if (gridSquares[r, c].Value == 0)
					{
						gridSquares[r, c].Values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
					}
				}
			}
		}
		public static void SetText(ref GridSquare[,] gridSquares)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					gridSquares[row, col].Text = GridSquare.FormatValues(gridSquares[row, col].Values, gridSquares[row, col].Value);
				}
			}
		}
		public static void CheckRow(ref GridSquare[,] gridSquares)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					if (gridSquares[row, col].Value != 0)
					{
						for (int c = 0; c < 9; c++)
						{
							if (gridSquares[row, c].Values.Count > 0)
							{
								gridSquares[row, c].Values.Remove(gridSquares[row, col].Value);
							}
						}
					}
				}
			}
		}
		public static void CheckCol(ref GridSquare[,] gridSquares)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					if (gridSquares[row, col].Value != 0)
					{
						for (int r = 0; r < 9; r++)
						{
							if (gridSquares[r, col].Values.Count > 0)
							{
								gridSquares[r, col].Values.Remove(gridSquares[row, col].Value);
							}
						}
					}
				}
			}
		}
		public static void CheckBox(int fila, int columna, ref GridSquare[,] gridSquares)
		{
			for (int row = fila; row < fila + 3; row++)
			{
				for (int col = columna; col < columna + 3; col++)
				{
					if (gridSquares[row, col].Value != 0)
					{
						for (int r = fila; r < fila + 3; r++)
						{
							for (int c = columna; c < columna + 3; c++)
							{
								if (gridSquares[r, c].Values.Count > 0)
								{
									gridSquares[r, c].Values.Remove(gridSquares[row, col].Value);
								}
							}
						}
					}
				}
			}
		}
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
						if(valuesList.Count > 0)
						{
							//return valuesList.Count == valuesList.Distinct().Count();
							flag = flag && valuesList.Count == valuesList.Distinct().Count();
							if (!flag)
							{
								int tempint = 0;
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
								//return valuesList.Count == valuesList.Distinct().Count();
								flag = flag && valuesList.Count == valuesList.Distinct().Count();
								if (!flag)
								{
									int tempint = 0;
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
		private static bool CheckOneBoxValues(int fila, int columna, GridSquare[,] gridSquares)
		{
			List<int> valuesList = new List<int>();
			bool flag = true;
			for (int row = fila; row < fila + 3; row++)
			{
				for (int col = columna; col < columna + 3; col++)
				{
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
									//return valuesList.Count == valuesList.Distinct().Count();
									flag = flag && valuesList.Count == valuesList.Distinct().Count();
									if (!flag)
									{
										int tempint = 0;
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}
	}
}
