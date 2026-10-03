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
