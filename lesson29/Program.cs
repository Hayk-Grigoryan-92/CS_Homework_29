using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose you game");
            
            Casino game = new Casino();
            bool flag = true;

            while (flag)
            {
                Bet bet = null;
                Console.WriteLine("Press 1: Number bet | Press 2: Odd bet | Press 3: Even bet | Press 4: Exit");
                if (!int.TryParse(Console.ReadLine(), out int option)) continue;
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Choose you lucky number");
                        if (!int.TryParse(Console.ReadLine(), out int chooseDise) || chooseDise < 1 || chooseDise > 6)
                        {
                            Console.WriteLine("Invalid number! Choose between 1 and 6.");
                            continue;
                        }
                        Console.WriteLine("Please insert bet amount");
                        if (!int.TryParse(Console.ReadLine(), out int amount)) continue;
                        bet = new NumberBet(amount, chooseDise);
                        game.PlaceBet(bet);

                        break;
                    case 2:
                        Console.WriteLine("Please insert bet amount");
                        if (!int.TryParse(Console.ReadLine(), out int amount2) || amount2 <= 0)
                        {
                            continue;
                        }
                        bet = new OddBet(amount2);
                        game.PlaceBet(bet);
                        break;
                    case 3:
                        Console.WriteLine("Please insert bet amount");
                        if (!int.TryParse(Console.ReadLine(), out int amount3) || amount3 <= 0)
                        {
                            continue;
                        }
                        bet = new EvenBet(amount3);
                        game.PlaceBet(bet);
                        break;
                    case 4:
                        flag = false;
                        continue;
                }
            }
        }
    }

    abstract class Bet
    {
        public int Amaunt { get; set; }
        public Bet(int money)
        {
            Amaunt = money;
        }

        private int _dice;

        public int Dice
        {
            get
            {
                Random d = new Random();
                _dice = d.Next(0, 7);
                return _dice;
            }
        }
        public abstract bool IsWinningBet();

        public virtual int PayOut()
        {
            return Amaunt * 2;
        }
    }

    class NumberBet : Bet
    {
        private int _number;

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value > 0 || value < 7)
                {
                    _number = value;
                }
                else
                {
                    throw new Exception("Wrong number of dice");
                }
            }
        }
        public NumberBet(int money, int num) : base(money)
        {
            Number = num;
        }

        public override int PayOut()
        {
            return Amaunt * 6;
        }

        public override bool IsWinningBet()
        {
            Console.WriteLine($"Dice : {Dice}");
            if (Dice == Number)
            {
                return true;
            }
            return false;
        }
    }

    class OddBet : Bet
    {
        public OddBet(int money) : base(money)
        {

        }
        public override bool IsWinningBet()
        {
            if (Dice % 2 == 1)
            {
                return true;
            }
            return false;
        }
    }

    class EvenBet : Bet
    {
        public EvenBet(int money) : base(money)
        {

        }
        public override bool IsWinningBet()
        {
            if (Dice % 2 == 0)
            {
                return true;
            }
            return false;
        }
    }


    class Casino
    {
        public int PlayerBalance { get; set; } = 100;
        public void PlaceBet(Bet bet)
        {
            if (PlayerBalance > bet.Amaunt)
            {
                PlayerBalance -= bet.Amaunt;
                if (bet.IsWinningBet())
                {
                    PlayerBalance += bet.PayOut();
                    Console.WriteLine("Congrats!!! You WIN.)");
                    Console.WriteLine($"Your balance is: {PlayerBalance}");
                }
                else
                {
                    Console.WriteLine("You lose, try again");
                    Console.WriteLine($"Your balance is: {PlayerBalance}");
                }
            }
            else
            {
                throw new Exception("Your ballance is too low");
            }
        }

    }
}
