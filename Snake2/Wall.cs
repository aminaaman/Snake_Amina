using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Snake_Amina
{
	public class Wall
	{
		public List<Point> body;
		public char sign;
		public ConsoleColor color;

		public Wall()
		{
		}
		public Wall(string FileName)
		{
			sign = '#';
			color = ConsoleColor.Red;
			body = new List<Point>();
			StreamReader sr = new StreamReader(FileName);
			int n = int.Parse(sr.ReadLine());
			for (int i = 0; i < n; ++i)
			{
				string s = sr.ReadLine();
				for (int j = 0; j < s.Length; j++)
				{
					if (s[j] == '*')
					{
						body.Add(new Point(j, i));
					}
				}
			}
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
				File.Delete("wall.xml");
				FileStream fs = new FileStream("wall.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
		public Wall Resume()
		{
			FileStream fs = new FileStream("wall.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			XmlSerializer xs = new XmlSerializer(typeof(Wall));
			Wall s = (Wall)xs.Deserialize(fs);
			fs.Close();
			return s;
		}
	}
}
