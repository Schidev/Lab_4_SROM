using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем поддержку русского языка в консоли
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GF number_1 = new GF("100100100100111101011010101010000101010101001");
            GF number_2 = new GF("111011010101000000111101010100011111101010101");
            GF number_3 = new GF("111010100101111011110110101101011111101010101");
            GF number_4 = new GF("11");

            Console.WriteLine("\nПроверка существования ГОНБ");
            if (number_1.Optim())
            {
                Console.WriteLine("\n\nСуществует ГОНБ");
            }
            Console.WriteLine("\nВозведение в квадрат");
            Console.WriteLine("\n\n{0} \nв квадрате будет \n{1}", number_1.Element, (number_1.Square()).Element);

            Console.WriteLine("\nСумма элементов: a + b = c");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_1.Element, number_2.Element, (number_1 + number_2).Element, "");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_2.Element, number_3.Element, (number_2 + number_3).Element, "");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_3.Element, number_1.Element, (number_3 + number_1).Element, "");

            Console.WriteLine("\nПроизведение элементов: a * b = c");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_1.Element, number_2.Element, (number_1 ^ number_2).Element, "");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_2.Element, number_3.Element, (number_2 ^ number_3).Element, "");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n(Ok) \n{3}", number_3.Element, number_1.Element, (number_3 ^ number_1).Element, "");

            Console.WriteLine("\nСтепень: a в степени b равно c");
            Console.WriteLine("\n\n{0} \n** \n{1} \n= \n{2} \n(Ok) \n{3}", number_1.Element, number_4.Element, number_1.Pow(number_4), "");

            Console.WriteLine("\nОбратный к элементу: a^-1 = b");
            Console.WriteLine("\n\n{0}^-1 \n= \n{1} \n(Ok) \n{2}", number_1.Element, number_1.YY(), "");

            Console.WriteLine("\nСлед элемента: Tr(a) = b;");
            number_1._Trace();
            number_2._Trace();

            Console.WriteLine("\n\nПРОВЕРКА: ");

            Console.WriteLine("\nСимметричность сложения: a + b = b + a");
            Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n== \n{3} \n= \n{1} \n+ \n{0}", number_1.Element, number_2.Element, (number_1 + number_2).Element, (number_2 + number_1).Element);

            Console.WriteLine("\nСимметричность умножения: a * b = b * a");
            Console.WriteLine("\n\n{0} \n* \n{1} \n= \n{2} \n== \n{3} \n= \n{1} \n* \n{0}", number_1.Element, number_2.Element, (number_1 ^ number_2).Element, (number_2 ^ number_1).Element);

            Console.WriteLine("\nДистрибутивность: (a + b) * c = (a * c) + (b * c)");
            Console.WriteLine("\n\n({0} + {1}) \n* \n{2} \n= \n{3} \n== \n{4} \n= \n({0} * {2}) \n+ \n({1} * {2})", number_1.Element, number_2.Element, number_3.Element, ((number_1 + number_2) ^ number_3).Element, ((number_1 ^ number_3) + (number_2 ^ number_3)).Element);

            Console.WriteLine("\nПроизведение обратного на сам элемент: a^-1 * a = 1");
            Console.WriteLine("\n\n{0}^-1 * \n{0} \n= \n{1} \n(Ok) \n{2}", number_1.Element, (number_1.YY() ^ number_1).Element, "");
            Console.WriteLine("\n\n{0}^-1 * \n{0} \n= \n{1} \n(Ok) \n{2}", number_2.Element, (number_2.YY() ^ number_2).Element, "");
            Console.WriteLine("\n\n{0}^-1 * \n{0} \n= \n{1} \n(Ok) \n{2}", number_3.Element, (number_3.YY() ^ number_3).Element, "");
        }
    }
}
