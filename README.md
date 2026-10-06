# Robust inköpslista

## Del 1 - Sex fel

### Fel 1: Programmet kraschade vid start.

**Vad hände?**
Programmet kraschade när det läste filen.

**Varför?**
I `Load()` delades varje rad med `Split(';')`. En tom rad gav bara `parts[0]`. När programmet sedan försökte läsa `parts[1]` fanns den inte, och därför fick programmet `IndexOutOfRangeException`.

**Hur fixade jag det?**
Jag lade till en kontroll med `IsNullOrWhiteSpace` och hoppade över tomma rader.

### Fel 2: Filen saknades.

**Vad hände?**
Programmet kraschade om `items.txt` inte fanns.

**Varför?**
`Load()` försökte läsa filen direkt med `File.ReadAllText` utan att först kontrollera om filen fanns. Då fick programmet `FileNotFoundException`.

**Hur fixade jag det?**
Jag lade till en kontroll med `File.Exists(path)`. Om filen inte finns visar programmet ett meddelande och fortsätter utan att krasha.

### Fel 3: Bokstäver i stället för tal.

**Vad hände?**
Programmet kraschade om användaren skrev bokstäver där programmet förväntade sig ett tal.

**Varför?**
Programmet använde `int.Parse()`. Om texten inte kunde göras om till ett heltal fick programmet `FormatException`.

**Hur fixade jag det?**
Jag bytte till `int.TryParse()` så att programmet kan kontrollera inmatningen och fråga igen i stället för att krascha.

### Fel 4: Fel nummer vid borttagning.

**Vad hände?**
Programmet kraschade om användaren försökte ta bort en vara med ett nummer som inte fanns i listan.

**Varför?**
`RemoveAt()` använde `items.RemoveAt(number - 1)` utan att först kontrollera om numret var giltigt. Om numret var för stort eller mindre än 1 blev indexet fel och programmet fick `ArgumentOutOfRangeException`.

**Hur fixade jag det?**
Jag lade till en kontroll som ser till att numret är mellan 1 och antalet varor i listan innan varan tas bort. Om numret är fel visar programmet meddelandet " Det finns ingen vara med det numret." i stället för att krascha.

### Fel 5: Fel totalsumma.

**Vad hände?**
Totalsumman blev för låg. Med Mjölk (15), Bröd (32)och Ost (89) visade programmet 121 kr i stället för 136 kr.

**Varför?**
I `Total()` började loopen på `i = 1`. Då hoppade programmet över den första varan i listan.

**Hur fixade jag det?**
Jag ändrade startvärdet från `i = 1` till `i = 0` så att alla varor räknas med i summan.

### Fel 6: Fel vid sparning doldes.

**Vad hände?**
Programmet kunde säga att listan var sparad även om något gick fel vid sparningen.

**Varför?**
`Save()` hade en tom `catch`. Om `File.WriteAllText()` misslyckades fångades felet, men programmet gjorde inget med det och skrev ändå att listan var sparad.

**Hur fixade jag det?**
Jag ersatte den tomma `catch` med specifika undantag, till exempel `UnauthorizedAccessException` och `IOException`. Meddelandet "Listan är sparad." visas nu bara när sparningen lyckas.

## Klassdiagram

![Klassdiagram](Klassdiagram.png)

## Designval - Budgettak

Jag valde att skicka in budgettaket till `ShoppingList` genom konstruktorn.
I `Program.cs` skapas listan med budgeten `200`:
`new ShoppingList("items.txt", 200)`
Budgeten sparas sedan i variablen `budgetLimit` i `ShoppingList`.
Jag valde den lösning eftersom budgeten kan ändras i `Program.cs` utan att ändra inne i `ShoppingList`.
