ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    int choice;

    while (!int.TryParse(Console.ReadLine(), out choice)) //Keep asking until the user enters a vlid number.
    {
        Console.Write("Skriv ett giltigt nummer: ");
    }

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");
        int price;

        while (!int.TryParse(Console.ReadLine(), out price)) //Keeps asking until the user enters a valid price.
        {
            Console.Write("Skriv ett giltigt pris: ");
        }
        list.Add(new Item(name, price));
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        int number;

        while (!int.TryParse(Console.ReadLine(), out number)) //Keep asking until the user enter a valid number.
        {
            Console.Write("Skriv ett giltigt nummer:");
        }
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}

