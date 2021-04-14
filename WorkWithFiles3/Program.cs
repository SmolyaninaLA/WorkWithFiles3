using System;
using System.IO;


class Program
{
    static void Main(string[] args)
      {
        // получим папку
        long totalDel = 0;
        

        Console.WriteLine("Введите путь ");
        string pathDir = Console.ReadLine();


        if (!Directory.Exists(pathDir))
        {
            Console.WriteLine("Нет указанго пути");
        }
        else
        {
            try
            {
                var dirc = new DirectoryInfo(pathDir);

                long totalsize = GetDirSize(dirc);

                Console.WriteLine("Исходный размер папки : {0} байт", totalsize);

                string[] subdir = Directory.GetDirectories(pathDir);
                string[] fl = Directory.GetFiles(pathDir);

                DateTime dt, curdt;

                TimeSpan diff;

                foreach (string sd in subdir)
                {


                    dt = Directory.GetLastWriteTime(sd);
                    curdt = DateTime.Now;
                    diff = curdt - dt;
                    var dr = new DirectoryInfo(sd);
                    long size;
                    size = GetDirSize(dr);

                    if (diff.TotalMinutes > 30.0)
                    {
                        try
                        {
                            totalDel += size;

                            dr.Delete(true);
                        }
                        catch (Exception ex)
                        {
                            totalDel -= size;
                             Console.WriteLine(" Произошла ошибка : {0}", ex.Message);
                        }
                    }

                } 

              

                foreach (string fi in fl)
                {
                    var filefordel = new FileInfo(fi);

                    dt = filefordel.LastWriteTime;
                   curdt = DateTime.Now;
                    diff = curdt - dt;


                        if (diff.TotalMinutes > 30.0)
                        {
                            try
                            {
                                totalDel += filefordel.Length;
                           
                                   filefordel.Delete();
                             }
                        catch (Exception ex)
                            {
                            totalDel -= filefordel.Length;
                            Console.WriteLine(" Произошла ошибка обработки файлов: {0}", ex.Message);
                            }
                    }
                   


                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(" Произошла ошибка обработки папки: {0}", exc.Message);
            }
            finally
            {
                Console.WriteLine("Освобождено : {0} байт", totalDel);
              

                var dirc = new DirectoryInfo(pathDir);
                long totalsize = GetDirSize(dirc);

                Console.WriteLine("Текущий размер папки : {0} байт", totalsize);
                Console.ReadKey();

            }
        } 
    }

    public static long GetDirSize(DirectoryInfo dir)
    {
        long size = 0;
        FileInfo[] fileInDir = dir.GetFiles();

        // посчитали размер файлов
        foreach (FileInfo f in fileInDir)
        {
            size += f.Length;
        }

        // размер в директории
        DirectoryInfo[] Dir = dir.GetDirectories();

        foreach (DirectoryInfo d in Dir)
        {
            size += GetDirSize(d);
        }

        return size;
    }

    
}
