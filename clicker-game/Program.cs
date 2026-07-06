using System;
using System.Threading.Tasks;

int coins = 0;
int coinsPerClick = 1;
int ACUpgradePrice = 250;
int ClickUpgradePrice = 10;
int ACclick = 2;
bool isACBought = false;

Console.WriteLine("Clicker has been started");
Console.WriteLine("Istruction:");
Console.WriteLine("Press [Space] to get coins(1 per click)");
Console.WriteLine("Press [U] to upgrade your clicks, it costs 10 coins initially, and the price doubles with each purchase until it reaches 5,120 ");
Console.WriteLine("Press [I] to view the balance");
Console.WriteLine("Press [O] to buy AutoClicker that give 2 coins per second, it costs 500 coins");
Console.WriteLine("Press [P] to buy AutoClicker upgrade, it costs 250 coins, and increases autoclicker revenue by 2");
Console.WriteLine($"Current balace: {coins}");

Task.Run(() => StartAC());

while (true)
{
    ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Spacebar)
    {
        coins += coinsPerClick;
        Console.WriteLine("Click!");
    }

    if (key == ConsoleKey.U)
    {
        if (coins >= ClickUpgradePrice)
        {
            coinsPerClick++;
            coins -= ClickUpgradePrice;
            if (ClickUpgradePrice * 2 <= 5120)
            {
                ClickUpgradePrice *= 2;
                Console.WriteLine($"Current click upgrade price: {ClickUpgradePrice}");
            }
            Console.WriteLine("Upgrade purchase successful");
        }
        else
        {
            Console.WriteLine("Not enough money");
        }
    }
    if (key == ConsoleKey.I)
    {
        Console.WriteLine($"Your Balance: {coins}");
    }
    if (key == ConsoleKey.O)
    {
        if (coins >= 500 && isACBought == false)
        {
            isACBought = true;
            coins -= 500;
            Console.WriteLine("Autoclicker purchase successful");
        }
        else
        {
            Console.WriteLine("Not enough money or an auto-clicker that has been purchased");
        }
    }
    if (key == ConsoleKey.P)
    {
        if (coins >= ACUpgradePrice && isACBought == true)
        {
            coins -= ACUpgradePrice;
            ACclick += 2;
            Console.WriteLine($"Autoclicker upgrade purchase successful, current coins per click: {ACclick}");
        }
        else
        {
            Console.WriteLine("Not enough money or an auto-clicker that hasn't been purchased");
        }
    }

}

async Task StartAC()
{
    while (true)
    {
        await Task.Delay(1000);
        if (isACBought)
        {
            coins += ACclick;
            Console.WriteLine("AC click!");
        }

    }
}