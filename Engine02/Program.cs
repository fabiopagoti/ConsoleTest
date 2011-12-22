using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Memory";

			Random rand = new Random();

			char[,] bräde = {{'A','A','B','B','C','C','D','D'},
							 {'E','E','F','F','G','G','H','H'},
							 {'I','I','J','J','K','K','L','L'},
							 {'M','M','N','N','O','O','P',' '}};

			int rad = 3;
			int kolumn = 7;

			int rad1 = 0;
			int kolumn1 = 0;

			Console.SetWindowSize(17, 15);

			for (int ggr = 0; ggr < 500; ggr++) //blanda 500ggr
			{
				rad1 = rand.Next(4);
				kolumn1 = rand.Next(8);

				bräde[rad, kolumn] = bräde[rad1, kolumn1];

				rad = rad1;
				kolumn = kolumn1;
			}
			bräde[rad, kolumn] = 'P';

			Console.WriteLine("+-+-+-+-+-+-+-+-+");
			Console.WriteLine("| | | | | | | | |");
			Console.WriteLine("+-+-+-+-+-+-+-+-+");
			Console.WriteLine("| | | | | | | | |");
			Console.WriteLine("+-+-+-+-+-+-+-+-+");
			Console.WriteLine("| | | | | | | | |");
			Console.WriteLine("+-+-+-+-+-+-+-+-+");
			Console.WriteLine("| | | | | | | | |");
			Console.WriteLine("+-+-+-+-+-+-+-+-+");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("   Spelare1: 0");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("   Spelare2: 0");

			Console.ForegroundColor = ConsoleColor.Red;

			Console.SetCursorPosition(0, 10);
			Console.Write("-->");

			int x = 1;
			int y = 1;

			int fx = 0;
			int fy = 0;

			int brädey = 0;
			int brädex = 0;

			int bfx = 0;
			int bfy = 0;

			int poäng1 = 0;
			int poäng2 = 0;

			char k1 = ' ';
			char k2 = ' ';

			int bk1x = 0;
			int bk2x = 0;

			int bk1y = 0;
			int bk2y = 0;

			int k1x = 0;
			int k1y = 0;

			int k2x = 0;
			int k2y = 0;

			bool kort1 = true;
			bool spelare1 = true;

			Console.SetCursorPosition(1, 1);

			while (poäng1 + poäng2 < 16)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo info = Console.ReadKey(true);
					ConsoleKey key = info.Key;

					if (key == ConsoleKey.UpArrow)
					{
						fx = 0; fy = -2;
						bfx = 0; bfy = -1;
					}
					if (key == ConsoleKey.DownArrow)
					{
						fx = 0; fy = 2;
						bfx = 0; bfy = 1;
					}
					if (key == ConsoleKey.RightArrow)
					{
						fx = 2; fy = 0;
						bfx = 1; bfy = 0;
					}
					if (key == ConsoleKey.LeftArrow)
					{
						fx = -2; fy = 0;
						bfx = -1; bfy = 0;
					}
					if (bräde[brädey, brädex] != ' ')
					{
						if (key == ConsoleKey.Spacebar)
						{
							Console.Write(bräde[brädey, brädex]);

							fx = 0;
							fy = 0;
							bfx = 0;
							bfy = 0;

							if (kort1)
							{
								k1 = bräde[brädey, brädex];
								bräde[brädey, brädex] = ' ';

								bk1x = brädex;
								bk1y = brädey;

								kort1 = false;
								k1x = x;
								k1y = y;
							}
							else
							{
								k2 = bräde[brädey, brädex];
								bräde[brädey, brädex] = ' ';

								bk2x = brädex;
								bk2y = brädey;

								kort1 = true;

								k2x = x;
								k2y = y;

								if (spelare1)
								{
									if (k1 == k2)
									{
										poäng1++;
										Console.SetCursorPosition(13, 10);
										Console.Write(poäng1);
									}
									else
									{
										System.Threading.Thread.Sleep(2000);


										Console.SetCursorPosition(k1x, k1y);
										Console.Write(" ");

										bräde[bk1y, bk1x] = k1;

										Console.SetCursorPosition(k2x, k2y);
										Console.Write(" ");

										bräde[bk2y, bk2x] = k2;

										spelare1 = false;

										Console.ForegroundColor = ConsoleColor.Green;

										Console.SetCursorPosition(0, 11);
										Console.Write("-->");

										Console.SetCursorPosition(0, 10);
										Console.Write("   ");
									}
								}
								else
								{
									if (k1 == k2)
									{
										poäng2++;
										Console.SetCursorPosition(13, 11);
										Console.Write(poäng2);
									}
									else
									{
										System.Threading.Thread.Sleep(2000);

										Console.SetCursorPosition(k1x, k1y);
										Console.Write(" ");

										bräde[bk1y, bk1x] = k1;

										Console.SetCursorPosition(k2x, k2y);
										Console.Write(" ");

										bräde[bk2y, bk2x] = k2;

										spelare1 = true;

										Console.ForegroundColor = ConsoleColor.Red;

										Console.SetCursorPosition(0, 10);
										Console.Write("-->");

										Console.SetCursorPosition(0, 11);
										Console.Write("   ");
									}
								}
							}
						}
					}
					if (fx != 0 || fy != 0)
					{
						x = x + fx;
						y = y + fy;
						brädex = brädex + bfx;
						brädey = brädey + bfy;
					}
					if (x < 0 || x > 16 || y < 0 || y > 7)
					{
						x = x - fx;
						y = y - fy;
					}
					if (brädex < 0 || brädey < 0 || brädex > 7 || brädey > 3)
					{
						brädex = brädex - bfx;
						brädey = brädey - bfy;
					}
					Console.SetCursorPosition(x, y);
				}
			}
			if (poäng1 > poäng2)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.SetCursorPosition(0, 13);

				Console.Write("Spelare 1 vann!!!");
			}
			if (poäng2 > poäng1)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.SetCursorPosition(0, 13);

				Console.Write("Spelare 2 vann!!!");
			}
			if (poäng1 == poäng2)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.SetCursorPosition(0, 13);

				Console.Write("Det blev oavgjort");
			}
		}
	}
}
