﻿using System;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Snake_Amina
{
	public class Game
	{
		public Snake snake;
		public Wall wall;
		public Food food;
		int cnt = 0, lvl = 1;
		public Game()
		{
			wall = new Wall("lvl1.txt");
			snake = new Snake();
			food = new Food(snake, wall);
		}
		void Save()
		{
			snake.Save();
			wall.Save();
			food.Save();
			File.Delete("cnt_lvl.xml");
			FileStream fs = new FileStream("cnt_lvl.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			Point x = new Point(cnt, lvl);
			XmlSerializer xs = new XmlSerializer(typeof(Point));
			xs.Serialize(fs, x);
			fs.Close();
		}
		void Resume()
		{
			snake = snake.Resume();
			snake.body.Remove(snake.body[0]);
			wall = wall.Resume();
			food = food.Resume();
			FileStream fs = new FileStream("cnt_lvl.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			XmlSerializer xs = new XmlSerializer(typeof(Point));
			Point x = (Point)xs.Deserialize(fs);
			cnt = x.x;
			lvl = x.y;
			fs.Close();
			Console.Clear();
			food.Draw();
			wall.Draw();
		}
		bool GameOver()
		{
			if (snake.SnakeInWall(wall))
				return true;
			if (snake.SnakeInSnake())
				return true;
			return false;
		}
		bool CanEat()
		{
			if (snake.body[0].x == food.body[0].x && snake.body[0].y == food.body[0].y)
				return true;
			return false;
		}
		public void Move()
		{
			while (true)
			{
				snake.Clear();
				if (MainClass.d == 1)
					snake.Move(0, -1);
				if (MainClass.d == 2)
					snake.Move(0, 1);
				if (MainClass.d == 3)
					snake.Move(1, 0);
				if (MainClass.d == 4)
					snake.Move(-1, 0);
				if (MainClass.d == 5)
					Save();
				if (MainClass.d == 6)
					Resume();
				if (GameOver())
				{
					Console.Clear();
					Console.SetCursorPosition(10, 10);
					Console.WriteLine("Game Over");
					Environment.Exit(0);
				}
				if (CanEat())
				{
					food.Clear();
					snake.body.Add(new Point(food.body[0].x, food.body[0].y));
					food = new Food(snake, wall);
					++cnt;
					if (cnt % 5 == 0)
					{
						++lvl;
						if (lvl <= 3)
						{
							if (lvl == 2)
								wall = new Wall("lvl2.txt");
							if (lvl == 3)
								wall = new Wall("lvl3.txt");
							
							snake = new Snake();
							food = new Food(snake, wall);
							Console.Clear();
							wall.Draw();
						}
					}
				}
				snake.Draw();
				food.Draw();
				Console.SetCursorPosition(50, 10);
				Console.Write("Curent score: ");
				Console.WriteLine(cnt);
				Console.SetCursorPosition(55, 10);
				Thread.Sleep(300);
			}
		}
	}
}
