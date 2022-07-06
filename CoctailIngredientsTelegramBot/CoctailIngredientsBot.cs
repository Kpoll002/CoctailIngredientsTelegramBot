using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using CoctailsIngredient.Client;
using CoctailsIngredient.Models;

namespace CoctailBot
{
    public class CoctailIngredientsBot
    {
        string LastText;
        string Call;
        TelegramBotClient botClient = new TelegramBotClient("5571847171:AAF-BvsiI4zetb0lf5WSOYs85bnFplz3FfQ");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} Работает");
            Console.ReadKey();

        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Ошибка в телеграм бот АПИ:\n {apiRequestException.ErrorCode}" + $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                



                ReplyKeyboardMarkup replyKeyboardMarkup = new
                   (
                   new[]
                   {
                     new KeyboardButton [] {"Поиск по ингредиенту", "Поиск по названию" },
                     new KeyboardButton [] { "Список ингредиентов", "Популярные коктейли" },
                     new KeyboardButton [] { "Случайный коктейль" }
                   }
                   )

                {
                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Добро Пожаловать!Выберете запрос", replyMarkup: replyKeyboardMarkup);
                return;
            }

            if (message.Text == "Поиск по ингредиенту")
            {
                LastText = "Добро Пожаловать!Выберете запрос:";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ингредиент(Eng)", replyMarkup: new ForceReplyMarkup() { Selective = true });

            }
            else
            if (LastText == "Добро Пожаловать!Выберете запрос:")
            {
                Call = message.Text;
                CoctailClient client = new CoctailClient();
                var result = client.GetSearchByIngredientAsync(Call).Result;
                foreach (var item in result.drinks)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $" {item.strDrinkThumb}\nНазвание:{item.strDrink}");
                }

            }
            else
            



            if (message.Text == "Поиск по названию")
            {
                LastText = "Добро Пожаловать!Выберете запрос";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название(Eng):", replyMarkup: new ForceReplyMarkup() { Selective=true });
            }
            else
            if (LastText == "Добро Пожаловать!Выберете запрос:")
            {
                Call = message.Text;
                CoctailClient client = new CoctailClient();
                var result = client.GetSearchByNameAsync(Call).Result;
                foreach (var item in result.ingredients)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Ингредиенты: {item.strIngredient}\nОписание:{item.strDescription}\nАлкоголь: {item.strAlcohol}\nОбороты: {item.strABV}\nТип: {item.strType}");

                }
            }

            else

            if (message.Text == "Список ингредиентов")
            {

                CoctailClient coctail = new CoctailClient();
                var result = coctail.GetListOfIngredientsAsync().Result;
                foreach (var item in result.drinks)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{item.strIngredient1}");
                }
            }

            if (message.Text == "Популярные коктейли")
            {

                CoctailClient coctail = new CoctailClient();
                var result = coctail.GetListOfPopularCoctailsAsync().Result;
                for (int i = 0; i < result.drinks.Count; i++)
                {

                    var item = result.drinks[i];

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{item.strImageSource}\nНазвание:{item.strDrink}\nТип:{item.strCategory}\nАлкоголь:{item.strAlcoholic}\nКак приготовить:{item.strInstructions}");
                }

            }
            
            if (message.Text == "Случайный коктейль")
            {
                CoctailClient client = new CoctailClient();
                var result = client.GetRandomCoctailsAsync().Result;
                for (var i = 0; i < result.drinks.Count; i++)

                {
                    var item = result.drinks [i];
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{item.strDrinkThumb})\nНазвание: { item.strDrink}\nТип: { item.strCategory}\nАлкоголь: { item.strAlcoholic}\nКак приготовить:{ item.strInstructions}");

                }
            }
            


          
        }


    }
}
