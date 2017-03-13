using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Snake_Amina
{
	class MainClass
	{
		public static int d = 1;


		public static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Console.SetWindowSize(75, 30);

			Console.SetCursorPosition(27, 10);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("$--Welcome to SNAKE--$");

			Console.SetCursorPosition(27, 12);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("PRESS ANY KEY TO START!");
			Console.ReadKey();
			Console.Clear();


			Game game = new Game();
			game.wall.Draw();
			Thread thread = new Thread(game.Move);
			thread.Start();
			while (true)
			{
				ConsoleKeyInfo pressed = Console.ReadKey();
				if (pressed.Key == ConsoleKey.UpArrow)
					d = 1;
				if (pressed.Key == ConsoleKey.DownArrow)
					d = 2;
				if (pressed.Key == ConsoleKey.RightArrow)
					d = 3;
				if (pressed.Key == ConsoleKey.LeftArrow)
					d = 4;
				if (pressed.Key == ConsoleKey.S)
					d = 5;
				if (pressed.Key == ConsoleKey.R)
					d = 6;
			}

		}
	}
}
