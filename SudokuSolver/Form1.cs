namespace SudokuSolver
{
	public partial class Form1 : Form
	{
		private const int GridSize = 9;
		private List<int> numbers;

		public Form1()
		{
			InitializeComponent();
			InitializeNumbers();
			GenerateGrid();
		}

		private void InitializeNumbers()
		{
			numbers = new List<int>();
			var rand = new Random();
			for (int i = 0; i < GridSize * GridSize; i++)
			{
				numbers.Add(rand.Next(1, 100)); // or however you want to fill it
			}
		}
		private void GenerateGrid()
		{
			panelGrid.Controls.Clear();
			panelGrid.SuspendLayout();
			panelGrid.AutoSize = true;

			var table = new TableLayoutPanel
			{
				RowCount = GridSize,
				ColumnCount = GridSize,
				Dock = DockStyle.Fill,
				AutoSize = true
			};

			for (int i = 0; i < GridSize; i++)
			{
				table.RowStyles.Add(new RowStyle(SizeType.Absolute, 75));
				table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
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

					var square = new GridSquare(row + 1, col + 1, values);

					// Optional: add click to show list
					square.Click += (s, e) =>
					{
						var btn = s as GridSquare;
						string valueList = string.Join(", ", btn.Values);
						MessageBox.Show($"Row: {btn.RowID}, Col: {btn.ColID}, Values: {valueList}");
					};

					table.Controls.Add(square, col, row);
				}
			}

			panelGrid.Controls.Add(table);
			panelGrid.ResumeLayout();

			// Resize the form based on grid dimensions
			int squareSize = 75;      // Match button size
			int margin = 2;           // 1px on each side
			int totalSize = (squareSize + margin * 2) * GridSize;

			this.ClientSize = new Size(totalSize + 20, totalSize + 40);

		}

	}

}
