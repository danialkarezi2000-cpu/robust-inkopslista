# Robust inköpslista

## Del 1 - Sex fel

### Fel 1: Programmet kraschade vid start.

**Vad hände?**
Programmet kraschade när det läste filen.

**Varför?**
I `Load()` delades varje rad med `Split(';')`. En tom rad gav bara `parts[0]`. När programmet sedan försökte läsa `parts[1]` fanns den inte, och därför fick programmet `IndexOutOfRangeException`.

**Hur fixade jag det?**
Jag lade till en kontroll med `IsNullOrWhiteSpace` och hoppade över tomma rader.
