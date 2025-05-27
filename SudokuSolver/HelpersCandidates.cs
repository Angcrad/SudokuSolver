using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class HelpersCandidates
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
		public static bool SingleCandidate(ref GridSquare[,] gridSquares)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					
					if (gridSquares[row, col].Values.Count == 1)
					{
						gridSquares[row, col].SetValue(gridSquares[row, col].Values[0]);
						return true;
					}
				}
			}
			return false;
		}
		public static void SetText(ref GridSquare[,] gridSquares)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					gridSquares[row, col].Text = GridSquare.FormatValues(gridSquares[row, col].Values, gridSquares[row, col].Value);
					if (gridSquares[row, col].Text.Length == 1)
					{
						gridSquares[row, col].Font = new Font("Consolas", 30, FontStyle.Bold);
					}
					else
					{
						gridSquares[row, col].Font = new Font("Consolas", 12, FontStyle.Bold);
					}
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
		public static void IsOnlyInOne(int fila, int columna, ref GridSquare[,] gridSquares)
		{
			bool flag = false;
			List<int[]> coordenadas = new List<int[]>();
			for (int row = fila; row < fila + 3; row++)
			{
				for (int col = columna; col < columna + 3; col++)
				{
					if (gridSquares[row, col].Value == 0)
					{
						foreach (int i in gridSquares[row, col].Values)
						{
							coordenadas.Clear();
							for (int r = fila; r < fila + 3; r++)
							{
								for (int c = columna; c < columna + 3; c++)
								{
									if (gridSquares[r, c].Values.Contains(i))
									{
										coordenadas.Add([r, c]);
									}
								}
							}
							InRow(ref gridSquares, coordenadas, fila, columna, row, col, i);
							InCol(ref gridSquares, coordenadas, fila, columna, row, col, i);
						}
					}
				}
			}
		}
		private static void InRow(ref GridSquare[,] gridSquares, List<int[]> coordenadas, int fila, int columna, int row, int col, int value)
		{
			List<int> values = new List<int>();
			foreach (int[] i in coordenadas)
			{
				values.Add(i[0]);
			}
			if (values.Distinct().Count() == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					if (!(i >= columna && i < columna + 3))
					{
						gridSquares[values[0], i].Values.Remove(value);
					}
					
				}
			}
		}
		private static void InCol(ref GridSquare[,] gridSquares, List<int[]> coordenadas, int fila, int columna, int row, int col, int value)
		{
			List<int> values = new List<int>();
			foreach (int[] i in coordenadas)
			{
				values.Add(i[1]);
			}
			if (values.Distinct().Count() == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					if (!(i >= fila && i < fila + 3))
					{
						gridSquares[i, values[0]].Values.Remove(value);
					}
					
				}
			}
		}
	}
}
