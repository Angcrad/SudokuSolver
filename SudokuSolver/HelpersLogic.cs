using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace SudokuSolver
{
	internal class HelpersLogic
	{
		public static bool IsOnlyNumberInColumn(ref GridSquare[,] gridSquares)
		{
			int cont = 0;
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					int tempInt = 0;
					foreach (int i in gridSquares[row, col].Values)
					{
						cont = 0;
						for (int r = 0; r < 9; r++)
						{
							if (gridSquares[r, col].Values.Contains(i))
							{
								cont++;
							}
						}
						if (cont == 1)
						{
							tempInt = i;
							break;
						}
					}
					if (cont == 1)
					{
						gridSquares[row, col].SetValue(tempInt);
						return true;
					}
				}
			}
			return false;
		}
		public static bool IsOnlyNumberInBox(int fila, int columna, ref GridSquare[,] gridSquares)
		{
			int cont = 0;
			for (int row = fila; row < fila + 3; row++)
			{
				for (int col = columna; col < columna + 3; col++)
				{
					int tempInt = 0;
					foreach (int i in gridSquares[row, col].Values)
					{
						cont = 0;
						for (int r = fila; r < fila + 3; r++)
						{
							for (int c = columna; c < columna + 3; c++)
							{
								if (gridSquares[r, c].Values.Contains(i))
								{
									cont++;
								}
							}
						}
						if (cont == 1)
						{
							tempInt = i;
							break;
						}
					}
					if (cont == 1)
					{
						gridSquares[row, col].SetValue(tempInt);
						return true;
					}
				}
			}
			return false;
		}
		public static bool IsOnlyNumberInRow(ref GridSquare[,] gridSquares)
		{
			int cont = 0;
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					int tempInt = 0;
					foreach (int i in gridSquares[row, col].Values)
					{
						cont = 0;
						for (int c = 0; c < 9; c++)
						{
							if (gridSquares[row, c].Values.Contains(i))
							{
								cont++;
							}
						}
						if (cont == 1)
						{
							tempInt = i;
							break;
						}
					}
					if (cont == 1)
					{
						gridSquares[row, col].SetValue(tempInt);
						return true;
					}
				}
			}
			return false;
		}
		public static void LoadPuzzleFromJson(string filePath, ref GridSquare[,] gridSquares)
		{
			if (!File.Exists(filePath))
			{
				MessageBox.Show("File not found.");
				return;
			}

			string json = File.ReadAllText(filePath);
			int[][] puzzleData = JsonSerializer.Deserialize<int[][]>(json);

			if (puzzleData.Length != 9 || puzzleData.Any(row => row.Length != 9))
			{
				MessageBox.Show("Invalid puzzle format.");
				return;
			}
			
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					int value = puzzleData[row][col];
					if (gridSquares[row, col] != null)
					{
						gridSquares[row, col].SetValue(value);
						if (value != 0)
							gridSquares[row, col].BackColor = Color.LightGray; // indicate pre-filled
						else
							gridSquares[row, col].BackColor = Color.White;
					}
				}
			}
		}

	}
}
