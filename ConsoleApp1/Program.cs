using System;
using System.IO;

class RenjuGame
{
	private const int _boardSize = 19;
	private const int _directions = 4;
	private const int _winCount = 5;

	static void Main(string[] args)
	{
		string inputFile = "../../../input.txt";

		try
		{
			string[] lines = File.ReadAllLines(inputFile);
			int testCases = int.Parse(lines[0]);

			if (CheckBoardSize(lines, testCases) != true)
			{
				Console.WriteLine("Wrong board size input");
				return;
			}
			int currentIndex = 1;

			for (int t = 0; t < testCases; t++)
			{
				int[,] board = new int[_boardSize, _boardSize];

				for (int i = 0; i < _boardSize; i++)
				{
					string[] stoneValues = lines[currentIndex].Split();
					for (int j = 0; j < _boardSize; j++)
					{
						board[i, j] = int.Parse(stoneValues[j]);
					}
					currentIndex++;
				}

				var result = CheckWinner(board);
				Console.WriteLine(result.Winner);

				if (result.Winner != 0)
				{
					int[] coordinates = { result.X, result.Y };
					Console.WriteLine($"{coordinates[0]} {coordinates[1]}\n");
				}

				currentIndex++;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred: " + ex.Message);
		}
	}

	static WinnerInfo CheckWinner(int[,] board)
	{
		int[] dx = { 1, 0, 1, 1 };
		int[] dy = { 0, 1, 1, -1 };

		var result = new WinnerInfo();

		for (int i = 0; i < _boardSize; i++)
		{
			for (int j = 0; j < _boardSize; j++)
			{
				if (board[i, j] == 0)
				{
					continue;
				}
				for (int d = 0; d < _directions; d++)
				{
					int count = 1;
					int x = i + dx[d];
					int y = j + dy[d];

					while (x >= 0 && x < _boardSize && y >= 0 && y < _boardSize && board[x, y] == board[i, j])
					{
						count++;
						x += dx[d];
						y += dy[d];
					}

					if (count == _winCount)
					{
						result.Winner = board[i, j];
						result.X = i + 1;
						result.Y = j + 1;
					}
				}
			}
		}

		return result;
	}

	static bool CheckBoardSize(string[] lines, int testCases)
	{
		if ((lines.Count() * testCases) % 20 != 0)
		{
			return false;
		}

		int emptyLinesCount = 0;

		foreach(var line in lines)
		{
			if (line == lines[0] || line == string.Empty)
			{
				emptyLinesCount++;
				continue;
			}

			if (line.Split().Length != _boardSize)
			{
				return false;
			}
		}

		if(emptyLinesCount > testCases)
		{
			return false;
		}

		return true;
	}

	private struct WinnerInfo
	{
		public int Winner;

		public int X;

		public int Y;
	} 
}