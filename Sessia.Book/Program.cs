using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sessia.Book
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public Book(string title, string author, int year, string genre)
        {
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"Название: {Title}, Автор: {Author}, Год выпуска: {Year}, Жанр: {Genre}";
        }
    }

    class Program
    {
        static List<Book> library = new List<Book>();
        static string fileName = "library.txt";

        static void AddBook()
        {
            Console.WriteLine("Введите название книги:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите автора книги:");
            string author = Console.ReadLine();

            Console.WriteLine("Введите год выпуска:");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Введите жанр:");
                string genre = Console.ReadLine();

                Book book = new Book(title, author, year, genre);
                library.Add(book);
            }
            else
            {
                Console.WriteLine("Неверный формат года.");
            }
        }

        static void RemoveBook()
        {
            Console.WriteLine("Введите номер книги для удаления:");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= library.Count)
            {
                library.RemoveAt(index - 1);
            }
            else
            {
                Console.WriteLine("Неверный номер книги.");
            }
        }

        static void ShowLibrary()
        {
            for (int i = 0; i < library.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {library[i]}");
            }
        }

        static void SaveLibrary()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var book in library)
                {
                    writer.WriteLine($"{book.Title},{book.Author},{book.Year},{book.Genre}");
                }
            }
            Console.WriteLine("Библиотека сохранена.");
        }

        static void LoadLibrary()
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        string title = parts[0];
                        string author = parts[1];
                        if (int.TryParse(parts[2], out int year))
                        {
                            string genre = parts[3];
                            Book book = new Book(title, author, year, genre);
                            library.Add(book);
                        }
                        else
                        {
                            Console.WriteLine($"Неверный формат года в строке: {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Неверный формат строки: {line}");
                    }
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Сохранить библиотеку");
            Console.WriteLine("4. Выйти");
        }
        static void Main(string[] args)
        {
            LoadLibrary();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Библиотека:");
                ShowLibrary();

                DisplayMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        SaveLibrary();
                        Console.WriteLine("Нажмите Enter чтобы продолжить.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine("Нажмите Enter для выхода.");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}