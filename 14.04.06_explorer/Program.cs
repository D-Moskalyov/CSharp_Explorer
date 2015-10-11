using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cars_23._03._14
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileSystemInfo> fSI = new List<FileSystemInfo>();
            string path;
            int index = 0;
            int lineCnt = 0;
            ConsoleKeyInfo key_info = new ConsoleKeyInfo();
            bool exist = false; 
            DirectoryInfo di;
            DirectoryInfo diCopy;
            do
            {
                Console.WriteLine("Введите директорию");
                path = Console.ReadLine();
                di = new DirectoryInfo(path);
                exist = di.Exists;
            } while (!exist);
            fSI.AddRange(di.GetFiles().ToList());
            fSI.AddRange(di.GetDirectories().ToList());
            PrintTree(index, fSI);
            lineCnt = fSI.Count;
            do
            {
                switch (key_info.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index > 0) index--;
                            else index = lineCnt;
                            PrintTree(index, fSI);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (index < lineCnt) index++;
                            else index = 0;
                            PrintTree(index, fSI);
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            if (index == 0)
                            {
                                if (di.Parent != null)
                                {
                                    di = di.Parent;
                                    fSI.Clear();
                                    fSI.AddRange(di.GetFiles().ToList());
                                    fSI.AddRange(di.GetDirectories().ToList());
                                    lineCnt = fSI.Count;
                                }
                                else
                                    break;
                            }
                            else
                            {
                                if ((fSI[index - 1].Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    di = (DirectoryInfo)fSI[index - 1];
                                    fSI.Clear();
                                    fSI.AddRange(di.GetFiles().ToList());
                                    fSI.AddRange(di.GetDirectories().ToList());
                                    lineCnt = fSI.Count;
                                }
                                else
                                    break;
                            }
                            index = 0;
                            PrintTree(index, fSI);
                            break;
                        }
                    case ConsoleKey.C:
                        {
                            if (index != 0)
                            {
                                if (!((fSI[index - 1].Attributes & FileAttributes.Directory) == FileAttributes.Directory))
                                {
                                    do
                                    {
                                        Console.WriteLine("Введите дирректорию");
                                        path = Console.ReadLine();
                                        diCopy = new DirectoryInfo(path);
                                        exist = diCopy.Exists;
                                        if (exist)
                                        {
                                            if ((diCopy.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                                            {
                                                ((FileInfo)fSI[index - 1]).CopyTo(diCopy.FullName + "\\" + fSI[index - 1].Name.ToString());
                                            }
                                            else
                                            {
                                                Console.WriteLine("Не верно. Отмена? (Y - отмена)");
                                                key_info = Console.ReadKey(true);
                                                if (key_info.Key == ConsoleKey.Y)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Не верно. Отмена? (Y - отмена)");
                                            key_info = Console.ReadKey(true);
                                            if (key_info.Key == ConsoleKey.Y)
                                            {
                                                break;
                                            }
                                        }
                                    } while (!exist);
                                }
                                //else index = 1;
                                PrintTree(index, fSI);
                                break;
                            }
                            break;
                        }
                    default:
                        break;
                }
                key_info = Console.ReadKey(true);
            } while (key_info.Key != ConsoleKey.Escape);
        }
        static void PrintTree(int indx, List<FileSystemInfo> fsi)
        {
            Console.Clear();
            if (indx == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("...");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
                Console.WriteLine("...");
            for (int i = 1; i <= fsi.Count; i++)
            {
                if (indx == i)
                {
                    if ((fsi[i - 1].Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(fsi[i - 1].ToString());
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(fsi[i - 1].ToString());
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                else
                    Console.WriteLine(fsi[i - 1].ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("c для копирования файлов");
        }
    }
}