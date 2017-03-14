using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
namespace Snake_Amina
{
	public class Food
	{
		public List<Point> body;
		public char sign;
		public ConsoleColor color;
		public Food()
		{
		}
		public Point RandomFoodGenerator(Snake snake, Wall wall)
		{
			Random rnd = new Random(DateTime.Now.Millisecond);
			int x = rnd.Next(39);
			int y = rnd.Next(19);
			while (true)
			{
				x = rnd.Next(39);
				y = rnd.Next(19);
				int b = 0;
				for (int i = wall.body.Count - 1; i >= 0; --i)
					if (x == wall.body[i].x && y == wall.body[i].y)
						b = 1;
				for (int i = snake.body.Count - 1; i >= 0; --i)
					if (x == snake.body[i].x && y == snake.body[i].y)
						b = 1;
				if (b == 0)
					break;
			}
			return new Point(x, y);
		}
		public Food(Snake snake, Wall wall)
		{
			sign = '$';
			color = ConsoleColor.Green;
			body = new System.Collections.Generic.List<Point>();
			body.Add(RandomFoodGenerator(snake, wall));
		}

		public void Draw()
		{
			Console.ForegroundColor = color;
			foreach (Point p in body)
			{
				Console.SetCursorPosition(p.x, p.y);
				Console.Write(sign);
			}
			Console.CursorVisible = false;
		}
		public void Clear()
		{
			foreach (Point p in body)
			{
				Console.SetCursorPosition(p.x, p.y);
				Console.Write(' ');
			}
			Console.CursorVisible = false;
		}
		public void Save()
		{
			try
			{
				File.Delete("food.xml");
				FileStream fs = new FileStream("food.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
				XmlSerializer xs = new XmlSerializer(typeof(Food));
				xs.Serialize(fs, this);
				fs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e);
				Console.ReadKey();
			}
		}
		public Food Resume()
		{
			FileStream fs = new FileStream("food.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			XmlSerializer xs = new XmlSerializer(typeof(Food));
			Food s = (Food)xs.Deserialize(fs);
			fs.Close();
			return s;
		}
	}
}
