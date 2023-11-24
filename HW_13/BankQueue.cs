using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13
{
    public class BankQueue
    {

        private Queue<Client> clientQueue = new Queue<Client>();
        private Dictionary<string, int> serviceTimes = new Dictionary<string, int>
    {
        { "Открытие вклада", 10 },
        { "Кредитование", 15 },
        { "Консультация", 5 }
    };

        private List<Client> servedClients = new List<Client>();

        /// <summary>
        ///Добавление клиента в очередь
        /// </summary>
        public void EnqueueClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");
            }

            clientQueue.Enqueue(client);
            Console.WriteLine($"Клиент {client.Name} добавлен в очередь. Текущее количество клиентов в очереди: {clientQueue.Count}");

            ShowQueueStatus();
        }

        /// <summary>
        /// Обслуживание клиента
        /// </summary>
        public void ServeClient()
        {
            if (clientQueue.Count > 0)
            {
                // Сортируем очередь по приоритету (по убыванию)
                var sortedQueue = clientQueue.OrderByDescending(client => client.Priority);

                Client servedClient = sortedQueue.First();
                clientQueue = new Queue<Client>(sortedQueue.Skip(1));

                int serviceTime = GetServiceTime(servedClient.ServiceType);
                Console.WriteLine($"Клиент {servedClient.Name} (Приоритет: {servedClient.Priority}) обслужен за {serviceTime} минут. " +
                                  $"Текущее количество клиентов в очереди: {clientQueue.Count}");

                servedClients.Add(servedClient);
                ShowQueueStatus();
            }
            else
            {
                Console.WriteLine("Очередь пуста. Нет клиентов для обслуживания.");
            }
        }


        /// <summary>
        /// Изменение времени обслуживания для определенного типа услуги
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="time"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddServiceTime(string serviceType, int time)
        {
            if (string.IsNullOrWhiteSpace(serviceType))
            {
                throw new ArgumentException("Тип услуги не может быть пустым или содержать только пробелы.", nameof(serviceType));
            }

            if (time <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(time), "Время обслуживания должно быть положительным числом.");
            }

            if (serviceTimes.ContainsKey(serviceType))
            {
                serviceTimes[serviceType] = time;
                Console.WriteLine($"Время обслуживания для типа услуги '{serviceType}' изменено на {time} минут.");
            }
            else
            {
                Console.WriteLine($"Тип услуги '{serviceType}' не найден. Время обслуживания не изменено.");
            }
        }

        /// <summary>
        /// Показ среднего времени ожидания в очереди
        /// </summary>
        public void ShowAverageWaitTime()
        {
            if (servedClients.Any())
            {
                double averageWaitTime = servedClients.Average(client => GetServiceTime(client.ServiceType));
                Console.WriteLine($"Среднее время ожидания в очереди: {averageWaitTime} минут");
            }
            else
            {
                Console.WriteLine("Очередь пуста. Нет данных для расчета среднего времени ожидания.");
            }
        }

        /// <summary>
        /// Показ истории обслуженных клиентов
        /// </summary>
        public void ShowServedClientsHistory()
        {
            if (servedClients.Any())
            {
                Console.WriteLine("История обслуженных клиентов:");

                foreach (var client in servedClients)
                {
                    Console.WriteLine($"Имя: {client.Name}, Тип обслуживания: {client.ServiceType}, VIP: {client.IsVip}");
                }
            }
            else
            {
                Console.WriteLine("Нет данных об обслуженных клиентах.");
            }
        }

        /// <summary>
        /// Получение времени обслуживания для определенного типа услуги
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private int GetServiceTime(string serviceType)
        {
            if (serviceTimes.ContainsKey(serviceType))
            {
                return serviceTimes[serviceType];
            }
            else
            {
                Console.WriteLine($"Время обслуживания для типа услуги '{serviceType}' не определено. Используется стандартное время.");
                return 10; // Значение по умолчанию
            }
        }

        /// <summary>
        /// Показ текущего состояния очереди
        /// </summary>
        private void ShowQueueStatus()
        {
            Console.WriteLine("Текущее состояние очереди:");

            foreach (var client in clientQueue)
            {
                Console.WriteLine($"Имя: {client.Name}, Тип обслуживания: {client.ServiceType}, VIP: {client.IsVip}");
            }

            Console.WriteLine();
        }


    }
}
