using System;
using ForSolar;
using taskForSolar;

namespace taskForSolar
{
    class Program
    {
        static void Main(string[] args)
        {
            bool process = true;
            while (process)
            {
                Console.Clear();
                Console.Write(" Меню:\n  1: Создать событие.\n" +
                    "  2: Просмотр задач.\n" +
                    "  3: Удаление задачи\n" +
                    "  4: Редактирование задачи\n\n" +
                    "  5: -> Выход  \n");
                var choice = Console.ReadKey().Key;
                switch (choice)
                {
                    // Создание задачи
                    case ConsoleKey.D1:
                        using (taskDB db = new taskDB())
                        {
                            db.Elements.Add(db.CreateAndFill());
                            db.SaveChanges();
                        }

                        Console.WriteLine("\n\n\t\tТы справился");
                        break;

                    // Просмотр заметок
                    case ConsoleKey.D2:
                        bool sortProcess = true;
                        Console.Clear();
                        using (taskDB db = new taskDB())
                        {
                            db.PrintCase();
                            while (sortProcess)
                            {
                                string menu = "\n\nShift: по возрастанию; Alt: по убыванию\n" +
                                    " 1: Название;\t 2: Описание;\n 3: Дата\n\n" +
                                    " Alt + C: Предстоит;\t Alt + E: Закончился срок выполнения\n" +
                                    " 5: Выход.";
                                Console.WriteLine(menu);

                                var keypress = Console.ReadKey();


                                if (keypress.Key == ConsoleKey.Backspace)
                                {
                                    break;
                                }

                                else if ((ConsoleModifiers.Shift & keypress.Modifiers) != 0)
                                {
                                    switch (keypress.Key)
                                    {
                                        case ConsoleKey.D1: // shift 1
                                            Console.Clear();
                                            db.PrintCaseOrderByNameAsc();
                                            break;

                                        case ConsoleKey.D2: // shift 2
                                            Console.Clear();
                                            db.PrintCaseOrderByDateAsc();
                                            break;

                                        case ConsoleKey.D3: // shift 3
                                            Console.Clear();
                                            db.PrintCaseOrderByDateAsc();
                                            break;

                                        default:
                                            Console.Clear();
                                            db.PrintCase();
                                            break;
                                    }
                                }
                                else if ((ConsoleModifiers.Alt & keypress.Modifiers) != 0)
                                {
                                    switch (keypress.Key)
                                    {
                                        case ConsoleKey.D1: // alt 1
                                            Console.Clear();
                                            db.PrintCaseOrderByNameDesc();
                                            break;

                                        case ConsoleKey.D2: // alt 2
                                            Console.Clear();
                                            db.PrintCaseOrderByDateDesc();
                                            break;

                                        case ConsoleKey.D3: // alt 3
                                            Console.Clear();
                                            db.PrintCaseOrderByDateDesc();
                                            break;

                                        default:
                                            Console.Clear();
                                            db.PrintCase();
                                            break;
                                    }
                                }

                            }

                        }
                        break;

                    // Удаление заметки
                    case ConsoleKey.D3:

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tУдаление");

                            // Да, только для того чтобы сделать вывод, да фигня. Но по другому у меня не вышло, соре.
                            using (taskDB db = new taskDB())
                            {
                                db.PrintCase();
                            }
                            Console.Write("\nВведите * для выхода\nID элемента: ");
                            string inputDel = Console.ReadLine();
                            if (inputDel != "*")
                            {
                                int delId = Convert.ToInt32(inputDel);
                                descriptionTask toDelete = new descriptionTask() { Id = delId };
                                using (taskDB db = new taskDB())
                                {
                                    db.Elements.Attach(toDelete);
                                    db.Elements.Remove(toDelete);
                                    db.SaveChanges();
                                }

                            }
                            else
                            {
                                break;
                            }

                        }
                        break;

                    // Редактирование заметки
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.WriteLine(" Редактирование:");
                        using (taskDB db = new taskDB())
                        {
                            db.PrintCase();
                        }
                        Console.Write("\nВведите * для выхода\nID элемента: ");
                        string inputEdit = Console.ReadLine();
                        if (inputEdit != "*")
                        {
                            int edit = Convert.ToInt32(inputEdit);
                            using (taskDB db = new taskDB())
                            {
                                db.Elements.Update(db.CreateAndFill(db.GetCaseById(edit)));
                                db.SaveChanges();
                            }

                            Console.WriteLine("\n\n\t\tЗадача обновлена");
                        }

                        break;

                    
                    case ConsoleKey.D0:
                        process = false;
                        break;
                }

            }

        }
    }
}