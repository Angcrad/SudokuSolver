using System;
using System.Collections.Generic;
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

		public Form1()
		{
			InitializeComponent();
			InitializeGrid();
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

					panelGrid.Controls.Add(gridSquare);
				}
			}

			panelGrid.Paint += DrawGridLines;

			// Resize form to fit grid
			this.ClientSize = new Size(panelGrid.Right + 10, panelGrid.Bottom + 10);
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
				if (keyData >= Keys.D1 && keyData <= Keys.D9)
				{
					int value = keyData - Keys.D0;
					selectedSquare.SetValue(value);
					return true;
				}
				else if (keyData >= Keys.NumPad1 && keyData <= Keys.NumPad9)
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
