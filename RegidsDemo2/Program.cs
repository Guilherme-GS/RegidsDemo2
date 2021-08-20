using System;
using StackExchange.Redis;

namespace RegidsDemo2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Redis");

            string connectionstring = "40.65.216.220";
            var redis = ConnectionMultiplexer.Connect(connectionstring);
            var db= redis.GetDatabase();

            //var respostas = db.HashGet("P1","Grupo01");

            //db.StringSet("A", 1);
            //string a = db.StringGet("A");

            //db.StringIncrement("A");

            //a = db.StringGet("A");

            //db.StringSet("B", 2);
            //string b = db.StringGet("B");

            //subcribe canal
            //publish canal valor

            var sub = redis.GetSubscriber();
            sub.Subscribe("perguntas").OnMessage(x =>
            {
                var dados = x.Message.ToString().Split(':');
                var chavePergunta = dados[0];
                var pergunta = dados[1];

                var value = Convert.ToInt32(chavePergunta.Substring(1));
                var resp = value * 2;
                Console.WriteLine(x.Message);
                Console.WriteLine(resp);
                db.HashSet(chavePergunta, "Grupo01", resp);
            });

            Console.ReadKey();
            
        }
    }
}
