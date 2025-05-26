namespace SudokuSolver
{
	public partial class Form1 : Form
	{
		private const int GridSize = 9;
		private TableLayoutPanel tableBaseGrid;
		private TableLayoutPanel tableOverlayGrid;
		private readonly int squareSize = 75;      // Match button size
		public Form1()
		{
			InitializeComponent();
			GenerateBaseGrid();
			GenerateOverlayGrid();
		}
		private void GenerateOverlayGrid()
		{
			tableOverlayGrid = new TableLayoutPanel
			{
				RowCount = GridSize,
				ColumnCount = GridSize,
				AutoSize = true,
				Location = new Point(0, 0),
				BackColor = Color.Transparent
			};

			for (int i = 0; i < GridSize; i++)
			{
				tableOverlayGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, squareSize));
				tableOverlayGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, squareSize));
			}

			var rand = new Random();

			for (int row = 0; row < GridSize; row++)
			{
				for (int col = 0; col < GridSize; col++)
				{
					int value = rand.Next(0, 15);
					var square = new SimpleSquare(row + 1, col + 1, value)
					{
						Visible = value >= 1 && value <= 9,
						FlatStyle = FlatStyle.Flat,
						BackColor = Color.Transparent
					};

					tableOverlayGrid.Controls.Add(square, col, row);
				}
			}

			panelGrid.Controls.Add(tableOverlayGrid);
			tableOverlayGrid.BringToFront(); // Ensure overlay is above
		}
		private void GenerateBaseGrid()
		{
			tableBaseGrid = new TableLayoutPanel
			{
				RowCount = GridSize,
				ColumnCount = GridSize,
				AutoSize = true,
				Location = new Point(0, 0)
			};

			for (int i = 0; i < GridSize; i++)
			{
				tableBaseGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, squareSize));
				tableBaseGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, squareSize));
			}

			for (int row = 0; row < GridSize; row++)
			{
				for (int col = 0; col < GridSize; col++)
				{
					var values = new List<int>();
					for (int j = 0; j < 9; j++)
					{
						values.Add(j + 1);
					}

					var square = new GridSquare(row + 1, col + 1, values)
					{
						Visible = true
					};
					tableBaseGrid.Controls.Add(square, col, row);
				}
			}
			
			panelGrid.Controls.Add(tableBaseGrid);

			// Resize the form based on grid dimensions
			
			int margin = 2;           // 1px on each side
			int totalSize = (squareSize + margin * 2) * GridSize;

			this.ClientSize = new Size(totalSize + 20, totalSize + 40);

		}

	}

}
