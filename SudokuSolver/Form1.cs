using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
namespace SudokuSolver
{

	public partial class Form1 : Form
	{
		private const int GridSize = 9;
		private const int CellSize = 75;
		private const int MarginSize = 1;
		private Panel panelGrid;
		private GridSquare selectedSquare;
		private GridSquare[,] gridSquares = new GridSquare[9, 9];
		private static List<GridSquare[,]> history = new List<GridSquare[,]>();
		private System.Windows.Forms.Timer updateTimer;
		private Button ButtonReset;
		private Button ButtonUndo;
		private Button buttonLoad;

		public Form1()
		{
			InitializeComponent();
			InitializeGrid();
			InitializeTimer();
		}
		private void InitializeTimer()
		{
			updateTimer = new System.Windows.Forms.Timer();
			updateTimer.Interval = 100; // Run approximately 60 times per second (16ms per tick)
			updateTimer.Tick += UpdateLoop;
			updateTimer.Start();
		}
		
		private void UpdateLoop(object sender, EventArgs e)
		{
			if(HelpersErrors.IsAllRight(gridSquares))
			{
				for (int row = 0; row < 9; row++)
				{
					for (int col = 0; col < 9; col++)
					{
						if (!gridSquares[row, col].IsSelected)
						{
							gridSquares[row, col].BackColor = Color.White;
						}
					}
				}
				HelpersCandidates.ReSetCandidates(ref gridSquares);
				HelpersCandidates.CheckRow(ref gridSquares);
				HelpersCandidates.CheckCol(ref gridSquares);
				HelpersCandidates.CheckBox(0, 0, ref gridSquares);
				HelpersCandidates.CheckBox(3, 0, ref gridSquares);
				HelpersCandidates.CheckBox(6, 0, ref gridSquares);
				HelpersCandidates.CheckBox(0, 3, ref gridSquares);
				HelpersCandidates.CheckBox(3, 3, ref gridSquares);
				HelpersCandidates.CheckBox(6, 3, ref gridSquares);
				HelpersCandidates.CheckBox(0, 6, ref gridSquares);
				HelpersCandidates.CheckBox(3, 6, ref gridSquares);
				HelpersCandidates.CheckBox(6, 6, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(0, 0, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(3, 0, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(6, 0, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(0, 3, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(3, 3, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(6, 3, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(0, 6, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(3, 6, ref gridSquares);
				HelpersCandidates.IsOnlyInOne(6, 6, ref gridSquares);
				HelpersCandidates.SetText(ref gridSquares);
			}
			if(HelpersErrors.IsAllRight(gridSquares))
			{	
				if(
				HelpersCandidates.SingleCandidate(ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInRow(ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInColumn(ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(0, 0, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(3, 0, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(6, 0, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(0, 3, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(3, 3, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(6, 3, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(0, 6, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(3, 6, ref gridSquares)
				|| HelpersLogic.IsOnlyNumberInBox(6, 6, ref gridSquares)
					)
				{
					//do nothing, short-circuit evaluation
				}
			}
		}
		private void ButtonReset_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					gridSquares[i, j].SetValue(0);
				}
			}
			history.Clear();
		}

		private void ButtonUndo_Click(object sender, EventArgs e)
		{
			if(history.Count > 1)
			{
				history.RemoveAt(history.Count - 1);
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						gridSquares[i, j].SetValue(history[history.Count - 1][i, j].Value);
					}
				}
			}
		}
		private void InitializeGrid()
		{
			int spacing = CellSize + 2 * MarginSize;
			panelGrid = new Panel
			{
				Location = new Point(10, 10),
				Size = new Size(GridSize * spacing, GridSize * spacing),
				BackColor = Color.White
			};
			int buttonSpacing = 20;
			int buttonWidth = 100;
			int buttonHeight = 30;
			int buttonX = panelGrid.Right + buttonSpacing;
			int buttonY = panelGrid.Top;
			ButtonReset = new Button
			{
				Text = "Reset",
				Location = new Point(buttonX, buttonY),
				Size = new Size(buttonWidth, buttonHeight)
			};
			ButtonReset.Click += ButtonReset_Click;
			this.Controls.Add(ButtonReset);

			ButtonUndo = new Button
			{
				Text = "Undo",
				Location = new Point(buttonX, buttonY + buttonHeight + 10),
				Size = new Size(buttonWidth, buttonHeight)
			};
			ButtonUndo.Click += ButtonUndo_Click;
			this.Controls.Add(ButtonUndo);
			this.Controls.Add(panelGrid);
			this.Width = ButtonReset.Right + buttonSpacing + 20;
			this.Height = Math.Max(panelGrid.Bottom, ButtonUndo.Bottom) + buttonSpacing;

			buttonLoad = new Button
			{
				Text = "Load Puzzle",
				Location = new Point(ButtonUndo.Left, ButtonUndo.Bottom + 10),
				Size = new Size(100, 30)
			};
			buttonLoad.Click += (s, e) =>
			{
				using (OpenFileDialog ofd = new OpenFileDialog())
				{
					ofd.Filter = "JSON Files (*.json)|*.json";
					if (ofd.ShowDialog() == DialogResult.OK)
					{
						history.Clear();
						HelpersLogic.LoadPuzzleFromJson(ofd.FileName, ref gridSquares);
					}
				}
			};
			this.Controls.Add(buttonLoad);

			for (int row = 0; row < GridSize; row++)
			{
				for (int col = 0; col < GridSize; col++)
				{
					var values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // placeholder
					var gridSquare = new GridSquare(row, col, values, 0, new Font("Consolas", 12, FontStyle.Bold), false)
					{
						Location = new Point(col * spacing, row * spacing),
						FlatStyle = FlatStyle.Flat,
						FlatAppearance = { BorderSize = 1, BorderColor = Color.LightGray }
					};
					gridSquare.Click += GridSquare_Click;
					gridSquares[row, col] = gridSquare;
					panelGrid.Controls.Add(gridSquare);
					//gridSquare.OnValueSet = HandleValueSet;

				}
			}
			panelGrid.Paint += DrawGridLines;
			// Resize form to fit grid
			this.ClientSize = new Size(ButtonReset.Right + buttonSpacing + 20, Math.Max(panelGrid.Bottom, ButtonUndo.Bottom) + buttonSpacing);
		}
		private void HandleValueSet(int row, int col, int value)
		{
			for (int c = 0; c < 9; c++)
			{
				if (value != 0)
				{
					//checkRow(row, c, value);
				}				
			}
			/*for (int r = 0; r < 9; r++)
			{
				for (int c = 0; c < 9; c++)
				{
					//gridSquares[r, c].Values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
					if ((r == row || c == col || SameBox(row, col, r, c)) && gridSquares[r, c].Value == 0)						
					{
						if (gridSquares[r, c].Values.Contains(value) && value != 0)
						{
							gridSquares[r, c].Values[value - 1] = 0;
							gridSquares[r, c].Text = GridSquare.FormatValues(gridSquares[r, c].Values, value);
						}
					}
				}
			}*/
		}
		
		private void DrawGridLines(object sender, PaintEventArgs e)
		{
			int spacing = CellSize + 2 * MarginSize;
			using (Pen thickPen = new Pen(Color.Black, 3))
			{
				for (int i = 0; i <= 9; i++)
				{
					if (i % 3 == 0)
					{
						// Vertical line
						e.Graphics.DrawLine(thickPen, i * spacing, 0, i * spacing, panelGrid.Height);
						// Horizontal line
						e.Graphics.DrawLine(thickPen, 0, i * spacing, panelGrid.Width, i * spacing);
					}
				}
			}
		}
		private void GridSquare_Click(object sender, EventArgs e)
		{
			if (selectedSquare != null)
				selectedSquare.SetSelected(false);

			selectedSquare = sender as GridSquare;
			selectedSquare.SetSelected(true);

			this.ActiveControl = null; // avoid focusing the button
		}
		private static void AddToHistory(GridSquare[,] gridSquares)
		{
			GridSquare[,] auxSquare = new GridSquare[9, 9];
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					var values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // placeholder
					var gridSquare = new GridSquare(i, j, values, 0, new Font("Consolas", 12, FontStyle.Bold), false);
					auxSquare[i,j] = gridSquare;
					auxSquare[i, j].SetValue(gridSquares[i, j].Value);
				}
			}
			history.Add(auxSquare);
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (selectedSquare != null)
			{
				if (keyData >= Keys.D0 && keyData <= Keys.D9)
				{
					int value = keyData - Keys.D0;
					selectedSquare.SetValue(value);
					AddToHistory(gridSquares);
					return true;
				}
				else if (keyData >= Keys.NumPad0 && keyData <= Keys.NumPad9)
				{
					int value = keyData - Keys.NumPad0;
					selectedSquare.SetValue(value);
					AddToHistory(gridSquares);
					return true;
				}
				else if (keyData == Keys.Back || keyData == Keys.Delete)
				{
					selectedSquare.SetValue(0);
					AddToHistory(gridSquares);
					return true;
				}
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
