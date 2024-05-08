//// Принцип единственной ответственности (SRP)
//// Класс Task должен отвечать только за хранение информации о задаче.
public class Task
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

// Принцип открытости/закрытости (OCP)
// Добавим интерфейс для хранилища задач, чтобы была возможность расширения функциональности.
public interface ITaskRepository
{
    void AddTask(Task task);
    List<Task> GetAllTasks();
}

// Принцип подстановки Барбары Лисков (LSP)
// Создадим специализированный класс для хранения задач в памяти.
public class InMemoryTaskRepository : ITaskRepository
{
    private List<Task> tasks = new List<Task>();

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public List<Task> GetAllTasks()
    {
        return tasks;
    }
}

// Принцип разделения интерфейса (ISP)
// Создадим интерфейс для сервиса управления задачами, который не будет зависеть от конкретной реализации хранилища.
public interface ITaskService
{
    void AddTask(string description);
    List<Task> GetAllTasks();
}

// Принцип инверсии зависимостей (DIP)
// Реализуем сервис управления задачами, зависящий только от абстракции ITaskRepository.
public class TaskService : ITaskService
{
    private readonly ITaskRepository taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        this.taskRepository = taskRepository;
    }

    public void AddTask(string description)
    {
        Task task = new Task { Description = description, IsCompleted = false };
        taskRepository.AddTask(task);
    }

    public List<Task> GetAllTasks()
    {
        return taskRepository.GetAllTasks();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем сервис управления задачами с использованием InMemoryTaskRepository.
        ITaskRepository taskRepository = new InMemoryTaskRepository();
        ITaskService taskService = new TaskService(taskRepository);

        // Добавляем задачи
        taskService.AddTask("Погулять с собакой");
        taskService.AddTask("Сделать уроки");
        taskService.AddTask("Приготовить ужин");

        // Получаем список задач и выводим его на экран
        List<Task> tasks = taskService.GetAllTasks();
        Console.WriteLine("Список задач:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"- {task.Description} {(task.IsCompleted ? "(Выполнено)" : "")}");
        }
    }
}
#region
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//// Принцип единственной ответственности (SRP)
//// Каждый класс отвечает только за свою задачу.

//// Класс для представления клиента
//public class Customer
//{
//    public string Name { get; set; }
//    public string Email { get; set; }
//}

//// Класс для представления товара
//public class Product
//{
//    public string Name { get; set; }
//    public double Price { get; set; }

//    public override string? ToString()
//    {
//        return $"Name:{Name}, Price:{Price}";
//    }
//}

//// Класс для представления заказа
//public class Order
//{
//    [Required]
//    static int NextNumber= 1;
//    public int Id { get; set; }
//    public Customer Customer { get; set; }
//    public List<Product> Products { get; set; }

//    public Order(Customer customer)
//    {
//        Id = NextNumber++;
//        Customer = customer;
//        Products = new List<Product>();
//        Console.WriteLine($"создан заказ {Id} для покупателя: {customer.Name}");
//    }

//    public void AddProduct(Product product)
//    {
//        Products.Add(product);
//        Console.WriteLine($"для покупателя{Customer.Name} добавлен товар {product}");
//    }
//}

//// Принцип открытости/закрытости (OCP)
//// Классы открыты для расширения, но закрыты для модификации.

//// Интерфейс для службы доставки
//public interface IShippingService
//{
//    void ShipOrder(Order order);
//}

//// Конкретная реализация службы доставки через почту
//public class MailShippingService : IShippingService
//{
//    public void ShipOrder(Order order)
//    {
//        //Console.WriteLine("состав заказа:");
//        //foreach (var item in order.Products)
//        //{
//        //    Console.WriteLine(item);
//        //}
//        Console.WriteLine($"Заказ №{order.Id} отправлен покупателю {order.Customer.Name} почтой {order.Customer.Email}.");

//    }
//}

//// Принцип подстановки Барбары Лисков (LSP)
//// Подклассы должны быть взаимозаменяемы с базовым классом.

//// Класс для представления заказа с оплатой при доставке
//public class CODOrder : Order
//{
//    public CODOrder(Customer customer) : base(customer) { }

//    // Добавляем оплату при доставке
//    public void PayOnDelivery(double amount)
//    {
//        Console.WriteLine($"Payment of {amount} received on delivery.");
//    }
//}

//// Принцип разделения интерфейса (ISP)
//// Клиенты не должны зависеть от интерфейсов, которые они не используют.

//// Класс для управления заказами
//public class OrderManager
//{
//    private readonly IShippingService shippingService;

//    public OrderManager(IShippingService shippingService)
//    {
//        this.shippingService = shippingService;
//    }

//    public void ProcessOrder(Order order)
//    {
//        // Обработка заказа
//        Console.WriteLine($"Обрабатывается заказ для: {order.Customer.Name}...");
//        shippingService.ShipOrder(order);
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Создаем клиента
//        Customer customer = new Customer { Name = "Иван Иванов", Email = "ivanIvanov@example.com" };

//        // Создаем товары
//        Product product1 = new Product { Name = "Book", Price = 10.99 };
//        Product product2 = new Product { Name = "Pen", Price = 1.99 };

//        // Создаем заказ
//        Order order = new Order(customer);
//        order.AddProduct(product1);
//        order.AddProduct(product2);

//        // Создаем менеджер заказов и передаем ему конкретную реализацию службы доставки
//        IShippingService shippingService = new MailShippingService();
//        OrderManager orderManager = new OrderManager(shippingService);

//        // Обрабатываем заказ
//        orderManager.ProcessOrder(order);

//        Console.ReadLine();
//    }
//}
#endregion