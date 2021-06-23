using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBotUI
{
    public class DiscordBotModel
    {
        private DiscordSocketClient _client;
        private string _token;
        private MapTable _mapData;

        public static CommandService _commands;
        public static IServiceProvider _services;

        public DiscordBotModel(string t, MapTable m)
        {
            _token = t;
            _mapData = m;
        }

        public void Connect()
        {
            Task t = MainAsync();
        }

        public bool IsConnected()
        {
            if (_client!=null && _client.ConnectionState == ConnectionState.Connected)
                return true;
            else
                return false;
        }
        

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info

            });
            _client.Log += Log;
            _commands = new CommandService();
            _services = new ServiceCollection().BuildServiceProvider();
            _client.MessageReceived += CommandRecieved;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
        }

        //接收發言 回覆
        private async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;

            //Debug用
            Console.WriteLine("{0} {1}:{2}", message.Channel.Name, message.Author.Username, message);
            if (message == null)
                return;

            //無視機器人發言
            if (message.Author.IsBot)
                return;


            var context = new CommandContext(_client, message);
            var CommandContext = message.Content;
            string reply = _mapData.Reply(message.Channel.Name, CommandContext);

            if (reply != null)
                await message.Channel.SendMessageAsync(reply);


        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

    }
}
