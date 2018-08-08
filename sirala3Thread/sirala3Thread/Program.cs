using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace sirala3Thread
{
    class Program
    {
        static int[] sayilar;
        static void Main(string[] args)
        {

            sayilar = new int[800];
            Random r = new Random();
            for (int i = 0; i < 800; i++)
            {
                int k = i;
                sayilar[i] = r.Next(1,5000);
                for (int j = 0; j < i; j++)
                {
                    if (sayilar[i]==sayilar[j])
                    {
                        i--;
                        break;
                    }
                }
            }//800 tane benzersiz eleman oluştu
            EkranaYazdir(0, 800, 0);
            Thread t1 = new Thread(() => Sirala(0, 300,1));
            Thread t2 = new Thread(() => Sirala(300, 800,2));
            Thread t3 = new Thread(() => Sirala(0, 800, 3));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            t3.Start();
            t3.Join();
        }
        static int hak = 0;
        static void Sirala(int bas,int son,int thread)
        { 
                int i, j, moved;
                for (i = bas + 1; i < son ; i++)
                {
                    moved = sayilar[i];
                    j = i;
                    while (j > bas && sayilar[j - 1] > moved)
                    {
                        sayilar[j] = sayilar[j - 1];
                        j--;
                    }
                    sayilar[j] = moved;
                }
                EkranaYazdir(bas, son,thread);
                hak++;
                if(hak==3)
                DosyayaYazdir();     
        }
        static void EkranaYazdir(int bas,int son,int sira)
        {        
                if(sira==0)
                    Console.Write("benzersiz rastgele 800 eleman:"+ Environment.NewLine);
                else
                Console.Write(sira+".thread'in yaptığı iş:"+Environment.NewLine);
                for (int i = bas; i < son; i++)
                {
                    Console.Write(sayilar[i] + " -- ");
                    if (i % 10 == 9)
                        Console.Write(Environment.NewLine);
                }
                Console.WriteLine("devam etmek için bir tuşa basın:"+Environment.NewLine);
                Console.ReadLine();
        }
        static void DosyayaYazdir()
        {
            
                StreamWriter dosya = File.CreateText("son.txt");
                for (int i = 0; i < 800; i++)
                {
                    dosya.WriteLine((i+1)+". sayı:   "+sayilar[i]);
                }
                dosya.Close();
        }
    }
}
