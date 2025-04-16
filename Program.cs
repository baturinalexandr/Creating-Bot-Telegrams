namespace YourProjectName
{
    class Program
    {
        private const string V = "/start";

        public static object BotMethods { get; private set; }

        static async Task Main()
        {
            // Мой токен, который вы получил от @BotFather
            string token = "7515320786:AAG_7q8H4TeDlR1_S7H_MCVxiBqG8X2UFYg";

            var botClient = new TelegramBotClient(token);

            // Получаем информацию о боте
            var botInfo = await botClient.GetMeAsync();
            Console.WriteLine($"Бот запущен: {botInfo.FirstName} ({botInfo.Username})");

            // Подключаемся к обновлениям через поллинг
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Получаем все типы обновлений
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            Console.ReadKey(); // Ждем нажатия клавиши для завершения программы
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, int v, CancellationToken cancellationToken)
        {
            if (update.Message is not null && update.Message.Text is not null)
            {
                // Обработка команд
                switch (update.Message.Text.ToLower())
                {
                    case v:
                        await BotMethods.HandleStartCommand(update.Message, botClient);
                        break;
                    default:
                        await BotMethods.HandleUnknownCommand(update.Message, botClient);
                        break;
                }
            }
        }

        private static async Task<Task> HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            object value = await BotMethods.HandlePollingErrorAsync(exception);
            return Task.CompletedTask;
        }
    }
}
