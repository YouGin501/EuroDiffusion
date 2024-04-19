using System;
using System.IO;

class RenjuGame
{
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
				int[,] board = new int[19, 19];

				for (int i = 0; i < 19; i++)
				{
					string[] stoneValues = lines[currentIndex].Split();
					for (int j = 0; j < 19; j++)
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

		for (int i = 0; i < 19; i++)
		{
			for (int j = 0; j < 19; j++)
			{
				if (board[i, j] != 0)
				{
					for (int d = 0; d < 4; d++)
					{
						int count = 1;
						int x = i + dx[d];
						int y = j + dy[d];

						while (x >= 0 && x < 19 && y >= 0 && y < 19 && board[x, y] == board[i, j])
						{
							count++;
							x += dx[d];
							y += dy[d];
						}

						if (count == 5)
						{
							result.Winner = board[i, j];
							result.X = i + 1;
							result.Y = j + 1;
						}
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

		foreach(var line in lines)
		{
			if (line == lines[0] || line == string.Empty)
				continue;

			if(line.Split().Length != 19)
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