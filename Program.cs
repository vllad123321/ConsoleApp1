using System;

// 1. Общий интерфейс компонента
interface INotifier
{
    void Send(string message);
}

// 2. Конкретный компонент - отправка email оповещений
class EmailNotifier : INotifier
{
    private string _email;

    public EmailNotifier(string email)
    {
        _email = email;
    }

    public void Send(string message)
    {
        Console.WriteLine($"Email sent to {_email}: {message}");
    }
}

// 3. Базовый декоратор
abstract class NotifierDecorator : INotifier
{
    protected INotifier _notifier;

    public NotifierDecorator(INotifier notifier)
    {
        _notifier = notifier;
    }

    public virtual void Send(string message)
    {
        _notifier.Send(message);
    }
}

// 4. Конкретный декоратор для отправки SMS
class SmsNotifier : NotifierDecorator
{
    private string _phoneNumber;

    public SmsNotifier(INotifier notifier, string phoneNumber) : base(notifier)
    {
        _phoneNumber = phoneNumber;
    }

    public override void Send(string message)
    {
        base.Send(message);  // Отправляем email уведомление
        Console.WriteLine($"SMS sent to {_phoneNumber}: {message}");
    }
}

// 4. Конкретный декоратор для отправки сообщений в Facebook
class FacebookNotifier : NotifierDecorator
{
    private string _facebookId;

    public FacebookNotifier(INotifier notifier, string facebookId) : base(notifier)
    {
        _facebookId = facebookId;
    }

    public override void Send(string message)
    {
        base.Send(message);  // Отправляем email и SMS уведомления
        Console.WriteLine($"Facebook message sent to {_facebookId}: {message}");
    }
}

// 5. Клиент
class Program
{
    static void Main(string[] args)
    {
        // Создаем базовый объект отправки оповещений по email
        INotifier notifier = new EmailNotifier("user@example.com");

        // Оборачиваем его в SMS-оповещения
        notifier = new SmsNotifier(notifier, "+1234567890");

        // Дополнительно оборачиваем в Facebook-оповещения
        notifier = new FacebookNotifier(notifier, "facebook-user-321");

        // Отправляем уведомление
        notifier.Send("System maintenance scheduled for tonight!");
    }
}
