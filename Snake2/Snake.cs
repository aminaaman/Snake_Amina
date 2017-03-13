using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
namespace Snake_Amina
{
	public class Snake
	{
		public List<Point> body;
		public char sign;
		public ConsoleColor color;

		public Snake()
		{
			sign = '*';
			body = new List <Point>();
			body.Add(new Point(10, 10));
			color = ConsoleColor.Yellow;
		}
		public void Move(int dx, int dy)
		{
			for (int i = body.Count - 1; i > 0; --i)
			{
				body[i].x = body[i - 1].x;
				body[i].y = body[i - 1].y;
			}
			body[0].x += dx;
			body[0].y += dy;
			if (body[0].x >= 40)
				body[0].x = 0;
			if (body[0].y >= 21)
				body[0].y = 0;
			if (body[0].x < 0)
				body[0].x = 39;
			if (body[0].y < 0)
				body[0].y = 20;
		}
		public bool SnakeInSnake()
		{
			for (int i = body.Count - 1; i > 0; --i)
				if (body[0].x == body[i].x && body[0].y == body[i].y)
					return true;
			return false;
		}
		public bool SnakeInWall(Wall wall)
		{
			for (int i = wall.body.Count - 1; i >= 0; --i)
				if (body[0].x == wall.body[i].x && body[0].y == wall.body[i].y)
					return true;
			return false;
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
		public void Clear() //
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
				File.Delete("snake.xml");
				FileStream fs = new FileStream("snake.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
				XmlSerializer xs = new XmlSerializer(this.GetType());
				xs.Serialize(fs, this);
				fs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
				Console.ReadKey();
			}
		}
		public Snake Resume()
		{
			FileStream fs = new FileStream("snake.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			XmlSerializer xs = new XmlSerializer(typeof(Snake));
			Snake s = (Snake)xs.Deserialize(fs);
			fs.Close();
			return s;
		}
	}
}
