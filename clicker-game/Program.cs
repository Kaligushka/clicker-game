using System;
using System.Threading.Tasks;

string lastNotification = "";
int notificationId = 0; 

// Player economy and stats
int coins = 0;

int coinsPerClick = 1;
int autoClickPower = 2;

int autoClickerUpgradePrice = 250;
int ClickUpgradePrice = 10;
int autoClickerPrice = 500;

const int maxClickUpgradePrice = 81920;
const int maxACUpgradePrice = 64000;

int ACAmount = 0;


DrawUI();

Task.Run(() => StartAC());

// Main input handling loop(Dispatcher)
while (true)
{
    ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Spacebar) TryClick();
    if (key == ConsoleKey.U) TryUpgradeClick();
    if (key == ConsoleKey.O) TryBuyAC();
    if (key == ConsoleKey.P) TryBuyACUpgrade();

}

// --- CORE METHODS ---

// Updates statistics and clears consoles to prevent clutter
void DrawUI()
{
    Console.Clear();

    Console.WriteLine("=== 🎮 CONSOLE CLICKER 1.1 ===");
    Console.WriteLine("Instruction:");
    Console.WriteLine($"  [Space] - Get coins (+{coinsPerClick} per click)");

    
    Console.WriteLine($"  [U]     - Upgrade your clicks (Cost: {ClickUpgradePrice} coins, price doubles)");

    
    Console.WriteLine($"  [O]     - Buy AutoClicker (Cost: {autoClickerPrice} coins, gives {autoClickPower} coins/sec)");

    
    Console.WriteLine($"  [P]     - Upgrade AutoClicker (Cost: {autoClickerUpgradePrice} coins)");
    Console.WriteLine("--------------------------------------------");

    
    Console.WriteLine($"🤖 AutoClickers amount: {ACAmount} (Generating: {autoClickPower * ACAmount} coins/sec)");
    Console.WriteLine($"💰 Current balance: {coins} coins");
    Console.WriteLine("--------------------------------------------");

    if (!string.IsNullOrEmpty(lastNotification))
    {
        Console.WriteLine(lastNotification);
    }
}

// Handles manual player clicks
void TryClick()
{
    coins += coinsPerClick;
    _ = ShowNotification($"Click!(+ {coinsPerClick})");
}

// Upgrades manual click power and doubles the price
void TryUpgradeClick()
{
    if (coins >= ClickUpgradePrice)
    {
        coinsPerClick++;
        coins -= ClickUpgradePrice;
        if (ClickUpgradePrice * 2 <= maxClickUpgradePrice)
        {
            ClickUpgradePrice *= 2;
        }
        _ = ShowNotification("Upgrade purchase successful");
    }
    else
    {
        _ = ShowNotification("Not enough money");
    }
}

// Purchases a new AutoClicker and scales the price exponentially
void TryBuyAC()
{
    if (coins >= autoClickerPrice)
    {
        ACAmount++;
        coins -= autoClickerPrice;

        autoClickerPrice = (int)(autoClickerPrice * 1.5);

        _ = ShowNotification("Autoclicker purchase successful");
    }
    else
    {
        _ = ShowNotification("Not enough money");
    }
}

// Upgrades the base efficiency of all owned AutoClickers
void TryBuyACUpgrade()
{
    if (coins >= autoClickerUpgradePrice && ACAmount > 0)
    {
        coins -= autoClickerUpgradePrice;
        
        if(autoClickerUpgradePrice * 2 <= maxACUpgradePrice) 
        {
            autoClickerUpgradePrice *= 2;
        }

        autoClickPower += 2;
        _ = ShowNotification($"Autoclicker upgrade successful! New power: {autoClickPower} coins/sec");
    }
    else
    {
        _ = ShowNotification("Not enough money or an auto-clicker that hasn't been purchased");
    }
}

// Manages temporary on-screen messages using thread-safe ID filtering
async Task ShowNotification(string message)
{
    notificationId++;
    int currentId = notificationId;
    
    lastNotification = message;
    DrawUI();

    await Task.Delay(2000);

    if(currentId == notificationId) 
    {
        lastNotification = "";
        DrawUI();
    }
}

// Background loop for passive income generation
async Task StartAC()
{
    while (true)
    {
        await Task.Delay(1000);
        if (ACAmount > 0)
        {
            coins += autoClickPower * ACAmount;
            DrawUI();
        }

    }
}