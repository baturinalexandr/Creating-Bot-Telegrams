using System;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        // Токен, полученный от @BotFather
        string botToken = "";
        

        var botClient = new TelegramBotClient(botToken);

        try
        {
            // Получаем основную информацию о боте
            var me = await botClient.GetMeAsync();

            Console.WriteLine($"UserName: {me.Username}");
            Console.WriteLine($"First Name: {me.First_Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка подключения к API Telegram:");
            Console.WriteLine(ex.Message);
        }
    }
}
