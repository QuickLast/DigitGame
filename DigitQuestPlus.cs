using System;
using System.Collections.Generic;
using System.Timers;

namespace DungeonGame
{
    class Player
    {
        public int Health { get; set; } = 100;
        public int Potions { get; set; } = 1;
        public int Gold { get; set; } = 0;
        public int Arrows { get; set; } = 5;
    }

    class Monster
    {
        public int Health { get; set; }

        public Monster()
        {
            Random rand = new Random();
            Health = rand.Next(20, 51);
        }

        public int Attack()
        {
            Random rand = new Random();
            return rand.Next(5, 16);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            List<string> dungeonMap = new List<string>{ "преподаватель", "дисциплинарка", "покушац", "завотделением", "пара" };
            Random rand = new Random();
            List<string> randomRooms = RandomRooms(dungeonMap);

            Console.WriteLine("Добро пожаловать в приемную комиссию МЦК КТИТС, дорогой абитуриент! Если ты поступишь - твоей основной задачей будет выжить и победить загадочного босса ДЕМО в конце своего обучения здесь.");
            Console.WriteLine("Тебя ждет тернистый путь, полный злых поварих, завотделений и преподавателей с их огромным количеством практик. Ты точно уверен, что оно тебе надо?\n");
            Console.ReadLine();
            Console.WriteLine("\nДа уже в целом и все равно, что ты там говоришь. ТЫ УЖЕ ПОСТУПИЛ! УАХАХАХАХАХАХХАХАХАХ");
            Console.WriteLine("ВПЕРЕД К ПРИКЛЮЧЕНИЯМ!");

            Console.WriteLine("\nНажмите Enter, чтобы продолжить.");
            Console.ReadLine();


            for (int i = 0; i < randomRooms.Count; i++)
            {
                Console.WriteLine($"\n==========\n\nВы вошли в комнату {i + 1}.\n\n==========\n");

                if (player.Potions > 0)
                {
                    Console.WriteLine($"У вас есть {player.Potions} милых видео с котиками. Хотите восстановить свои нервные клетки? Напишите 'д', если Да и 'н' если Нет");
                    string action = Console.ReadLine().ToLower();
                    if (action == "д")
                    {
                        player.Health += 10;
                        player.Potions--;
                        Console.WriteLine($"Видео с котиками закончилось. Вы восстановили 10 нервных клеток (теперь их у вас {player.Health}), и теперь радостный(ая) двигаетесь дальше!\n");
                      
                    }
                    else if (action == "н")
                    {

                    }
                    else
                    {
                        Console.WriteLine("Сочту это за 'нет'. Идем дальше");
                    }
                    Console.WriteLine();
                }

                switch (randomRooms[i])
                {
                    case "преподаватель":
                        Monster monster = new Monster();
                        Console.WriteLine("В этой комнате вас ждет схватка с преподавателем! Надо сдать все практики и победить монстра!");
                        if (!Battle(player, monster)) return;
                        break;

                    case "дисциплинарка":
                        int trapDamage = rand.Next(10, 21);
                        player.Health -= trapDamage;
                        Console.WriteLine($"Вас отправили на дисциплинарку и после нее вы потеряли {trapDamage} нервных клеток. Ваше здоровье: {player.Health} НК.");
                        break;

                    case "покушац":
                        Console.WriteLine("Вы забрели в столовую!");
                        OpenChest(player);
                        break;

                    case "завотделением":
                        Console.WriteLine("Вы встретили завотделением.");
                        VisitMerchant(player);
                        break;

                    case "пара":
                        Console.WriteLine("Вы просто пришли на пару. Скукота.");
                        break;

                    case "демо":
                        Console.WriteLine("Вы вошли в комнату босса ДЕМО!");
                        Monster boss = new Monster();
                        if (!Battle(player, boss)) return;
                        break;
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("Вы погибли!");
                    return;
                }

                Console.WriteLine("\nНажмите Enter, чтобы продолжить.");
                Console.ReadLine();
            }

            Console.WriteLine("Поздравляем! Вы победили!");
        }

        static List<string> RandomRooms(List<string> currentPoints)
        {
            List<string> listOfRooms = new List<string>();
            Random rand = new Random();
            for (int i=0; i < 9; i++)
            {
                listOfRooms.Add(currentPoints[rand.Next(0, 5)]);
            }
            listOfRooms.Add("демо");
            return listOfRooms;
        }

        static bool Battle(Player player, Monster monster)
        {
            while (player.Health > 0 && monster.Health > 0)
            {
                Console.Write("Выберите действие: 'сдать практику' или 'попытаться получить автомат': ");
                string action = Console.ReadLine().ToLower();

                if (action == "сдать практику")
                {
                    Random rand = new Random();
                    int damage = rand.Next(10, 21);
                    monster.Health -= damage;
                    Console.WriteLine($"\nВы попытались сдать практику и смогли сдать {damage} практик за раз (жесть). У преподавателя осталось {monster.Health} практик для сдачи.\n");

                    if (monster.Health > 0)
                    {
                        int monsterDamage = monster.Attack();
                        player.Health -= monsterDamage;
                        Console.WriteLine($"Монстр атаковал вас по вашему самолюбию и вы получили {monsterDamage} психического урона. Ваше здоровье: {player.Health} нервных клеток.\n");
                    }
                }
                else if (action == "попытаться получить автомат")
                {
                    if (player.Arrows > 0)
                    {
                        Random rand = new Random();
                        int damage = rand.Next(5, 16);
                        monster.Health -= damage;
                        Console.WriteLine($"Вы попытались получить автомат по практикам, задарив преподавателя, и смогли закрыть {damage} практик автоматом (ура). У преподавателя осталось {monster.Health} практик для сдачи.\n");
                        player.Arrows--;
                    }
                    else if (player.Arrows == 0)
                    {
                        Console.WriteLine("У вас кончились подарки, эх. Придется сдавать все самому.\n");
                    }
                }
                else
                {
                    Console.WriteLine("А вот ни-ни. Только эти варианты. Попробуйте снова.\n");
                }
            }

            if (monster.Health <= 0)
            {
                Console.WriteLine("Вы закрыли предмет!");
                player.Gold += 10; // Получаем объяснительные за победу
                return true;
            }

            return false;
        }

        static void OpenChest(Player player)
        {
            Random rand = new Random();
            int answer = rand.Next(1, 11) + rand.Next(1, 11);
            int randomNum = rand.Next(1, 11);

            Console.WriteLine($"Злая повариха решила вас проучить: сколько будет {answer - randomNum} + {randomNum}?");

            while (true)
            {
                Console.Write("Ваш ответ: ");
                if (int.TryParse(Console.ReadLine(), out int guess) && guess == answer)
                {
                    string loot = GetLoot();
                    if (loot == "potion")
                    {
                        player.Potions++;
                        Console.WriteLine("Вы нашли милое видео с котиками! Оно поможет вылечить ваши нервные клетки!");
                    }
                    else if (loot == "gold")
                    {
                        player.Gold += 20;
                        Console.WriteLine("Вы нашли объяснительные! Теперь можно еще немного почиллить дома или спастись от завотделения!");
                    }
                    else if (loot == "arrows")
                    {
                        player.Arrows += 5;
                        Console.WriteLine("Вы нашли подарки для того, чтобы получить автомат!");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Повариха так и знала, что вы неуч! Попробуйте снова.");
                }
            }
        }

        static string GetLoot()
        {
            Random rand = new Random();
            int lootChance = rand.Next(1, 4); // 1-3
            return lootChance switch
            {
                1 => "potion",
                2 => "gold",
                _ => "arrows",
            };
        }

        static void VisitMerchant(Player player)
        {
            if (player.Gold >= 30)
            {
                player.Gold -= 30;
                player.Potions++;
                Console.WriteLine("Вы отдали 30 объяснительных, чтобы вам дали возможность посмотреть милое видео с котиками!");
            }
            else
            {
                Console.WriteLine("У вас недостаточно объяснительных для покупки видео с котиками.");
            }
        }
    }
}
