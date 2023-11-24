using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankQueue bankQueue = new BankQueue();
            Random random = new Random();

            while (true)
            {
                Console.WriteLine("Выберите роль (1 - Клиент, 2 - Администратор, 3 - Просмотр среднего времени ожидания, 4 - Просмотр истории обслуженных клиентов, 5 - Выход):");
                int roleChoice;
                if (!int.TryParse(Console.ReadLine(), out roleChoice))
                {
                    Console.WriteLine("Некорректный выбор. Повторите попытку.");
                    continue;
                }

                switch (roleChoice)
                {
                    case 1:
                        // Роль Клиента
                        Console.WriteLine("Введите ваше имя:");
                        string clientName = Console.ReadLine();

                        Console.WriteLine("Выберите тип обслуживания (1 - Открытие вклада, 2 - Кредитование, 3 - Консультация):");
                        int serviceChoice;
                        if (!int.TryParse(Console.ReadLine(), out serviceChoice) || serviceChoice < 1 || serviceChoice > 3)
                        {
                            Console.WriteLine("Некорректный выбор. Вас зарегистрировано на обслуживание по умолчанию (Открытие вклада).");
                            serviceChoice = 1; // По умолчанию - открытие вклада
                        }
                        string serviceType = "";
                        switch (serviceChoice)
                        {
                            case 1:
                                serviceType = "Открытие вклада";
                                break;
                            case 2:
                                serviceType = "Кредитование";
                                break;
                            case 3:
                                serviceType = "Консультация";
                                break;
                        }

                        Console.WriteLine("Введите код подтверждения VIP-статуса:");
                        string enteredCode = Console.ReadLine();

                        //административный код (например, "admin123")
                        string adminCode = "admin123";

                        bool isVip = false;

                        if (enteredCode == adminCode)
                        {
                            Console.WriteLine("Код верный. Вы VIP-клиент. Добро пожаловать!");
                            isVip = true;
                        }
                        else
                        {
                            Console.WriteLine("Введен неверный код. Вы не являетесь VIP-клиентом.");
                            isVip = false;
                        }

                        int priority = isVip ? 1 : 0; // Присваиваем приоритет 1 для VIP-клиентов, 0 - для обычных
                        Client client = new Client(random.Next(1000, 9999), clientName, serviceType, isVip, priority);
                        bankQueue.EnqueueClient(client);

                        break;


                    case 2:
                        // Роль Администратора
                        Console.WriteLine("Выберите действие для администратора (1 - Обслужить клиента, 2 - Изменить время обслуживания, 3 - Выйти):");
                        int adminChoice;
                        if (!int.TryParse(Console.ReadLine(), out adminChoice))
                        {
                            Console.WriteLine("Некорректный выбор. Повторите попытку.");
                            continue;
                        }

                        switch (adminChoice)
                        {
                            case 1:
                                bankQueue.ServeClient();
                                break;

                            case 2:
                                Console.WriteLine("Введите тип услуги для изменения времени обслуживания:");
                                string serviceTypeToChange = Console.ReadLine();

                                Console.WriteLine("Введите новое время обслуживания (в минутах):");
                                int newServiceTime;
                                if (!int.TryParse(Console.ReadLine(), out newServiceTime))
                                {
                                    Console.WriteLine("Некорректный ввод. Используется стандартное время.");
                                    newServiceTime = 10;
                                }

                                bankQueue.AddServiceTime(serviceTypeToChange, newServiceTime);
                                break;

                            case 3:
                                // Выйти из режима администратора
                                break;

                            default:
                                Console.WriteLine("Некорректный выбор. Повторите попытку.");
                                break;
                        }
                        break;

                    case 3:
                        // Просмотр среднего времени ожидания
                        bankQueue.ShowAverageWaitTime();
                        break;

                    case 4:
                        // Просмотр истории обслуженных клиентов
                        bankQueue.ShowServedClientsHistory();
                        break;

                    case 5:
                        // Выход из программы
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break;
                }
            }
        }
    }
}
