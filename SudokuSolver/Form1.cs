using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
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
		private System.Windows.Forms.Timer updateTimer;
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
			if(Helpers.IsAllRight(gridSquares))
			{
				Helpers.ReSetCandidates(ref gridSquares);
				Helpers.CheckRow(ref gridSquares);
				Helpers.CheckCol(ref gridSquares);
				Helpers.CheckBox(0, 0, ref gridSquares);
				Helpers.CheckBox(3, 0, ref gridSquares);
				Helpers.CheckBox(6, 0, ref gridSquares);
				Helpers.CheckBox(0, 3, ref gridSquares);
				Helpers.CheckBox(3, 3, ref gridSquares);
				Helpers.CheckBox(6, 3, ref gridSquares);
				Helpers.CheckBox(0, 6, ref gridSquares);
				Helpers.CheckBox(3, 6, ref gridSquares);
				Helpers.CheckBox(6, 6, ref gridSquares);
				Helpers.SetText(ref gridSquares);
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
			this.Controls.Add(panelGrid);

			for (int row = 0; row < GridSize; row++)
			{
				for (int col = 0; col < GridSize; col++)
				{
					var values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // placeholder
					var gridSquare = new GridSquare(row, col, values, 0)
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
			this.ClientSize = new Size(panelGrid.Right + 10, panelGrid.Bottom + 10);
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
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (selectedSquare != null)
			{
				if (keyData >= Keys.D0 && keyData <= Keys.D9)
				{
					int value = keyData - Keys.D0;
					selectedSquare.SetValue(value);
					return true;
				}
				else if (keyData >= Keys.NumPad0 && keyData <= Keys.NumPad9)
				{
					int value = keyData - Keys.NumPad0;
					selectedSquare.SetValue(value);
					return true;
				}
				else if (keyData == Keys.Back || keyData == Keys.Delete)
				{
					selectedSquare.SetValue(0);
					return true;
				}
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
