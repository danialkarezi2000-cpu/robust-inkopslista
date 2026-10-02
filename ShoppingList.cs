// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;
    private int budgetLimit;

    public ShoppingList(string path, int budgetLimit)
    {
        this.path = path;
        this.budgetLimit = budgetLimit;
    }

    public void Add(Item item)
    {
        if (Total() + item.Price > budgetLimit) // Checks if the item would exceed the budget.
        {
            throw new InvalidOperationException("Budgeten skulle överskridas.");
        }
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {

        if (number < 1 || number > items.Count)// Checks that the item number exists.
        {
            Console.WriteLine("Det finns ingen vara med det numret. ");
            return;
        }
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++) // Add all item prices, starting from index 0
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name.ToLower() == name.ToLower())
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try // Tries to save the list to the file.
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Det gick inte att spara: du saknar behörighet att skriva till filen.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Det gick inte att spara listan:{ex.Message}");
        }

    }

    // Reads the file back into the list.
    public void Load()
    {
        if (!File.Exists(path)) // Stops loading if the file doesn't exist.
        {
            Console.WriteLine(" Det finns ingen sparad lista. ");
            return;
        }
        String[] lines = File.ReadAllLines(path); // Reads the file on line at a time.
        foreach (string line in lines)
        {
            //Skips empty lines
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            string[] parts = line.Split(';');
            // Checks that the line is in the correct format and that the price is a valid number. If not, skip it.

            if (parts.Length != 2 || !int.TryParse(parts[0], out int price))
            {
                Console.WriteLine($"Hoppar över en trasig rad i filen: {line}");
                continue;
            }
            try
            {
                items.Add(new Item(parts[1], price));
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Hoppar över en trasig rad i filen: {line}");
            }
        }
    }
}
