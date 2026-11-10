# Exercise: The Shipping Rate Calculator

Exercise code for the lesson **Exercise: The Shipping Rate Calculator** in the course *Design Patterns, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the operations manager, Lakeside Parcel
>
> **A fourth carrier, NightOwl, signs on next month.** NightOwl charges $9 plus $1 a zone, and only delivers to zones 1 to 3. Their integration team has already written NightOwl's rules as a carrier of their own, against the carrier promise in our code (a name, `canDeliver(zone)` and `rate(zone)` in cents), and their code calls two things we have to finish:
>
> 1. **`quoteFor(carrier, zone)`** must quote any carrier it's handed, by that carrier's own rules: `NightOwl: $11.00` for zone 2, and `NightOwl: doesn't deliver to zone 4`.
> 2. **`standardCarriers()`** must hand back the three carriers we offer today, QuickShip, EcoFreight and SameDay, in that order, so the quote board can list NightOwl beside them.
>
> Nobody should have to edit QuickShip's, EcoFreight's or SameDay's rules to add NightOwl, or the next carrier after it. The counter's quote by name, `quote("QuickShip", 2)`, has to keep working exactly as it does today.

`quoteFor` and `standardCarriers` are already there, because NightOwl's code calls them, but they don't do anything yet.

## Today's rates

| Carrier | Rate | Zones |
|---|---|---|
| QuickShip | $5 plus $2 a zone | 1 to 4 |
| EcoFreight | $7, plus a 10% fuel charge ($7.70) | 1 to 4 |
| SameDay | $15 | 1 and 2 only |
| NightOwl (the ticket) | $9 plus $1 a zone | 1 to 3 only |

Rates are kept in cents, so the fuel charge never rounds wrong, and shown with two decimals: `$7.70`.

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | The rate desk's code | The tests |
|---|---|---|
| `csharp/` | `ShippingRates/Rates.cs` | `ShippingRates.Tests/RatesTests.cs` |
| `java/` | `src/main/java/` (`RateDesk.java`, `Carrier.java`) | `src/test/java/RatesTest.java` |
| `python/` | `rates.py` | `test_rates.py` |
| `javascript/` | `rates.js` | `rates.test.js` |
| `typescript/` | `rates.ts` | `rates.test.ts` |

The names follow each language's style: `Quote`, `QuoteFor` and `StandardCarriers` in C#, `quote_for` and `standard_carriers` in Python, `quote`, `quoteFor` and `standardCarriers` in Java, JavaScript and TypeScript. In C#, Java and TypeScript the carrier promise is an interface (`ICarrier`, `Carrier`); in Python and JavaScript a carrier is any object with a name and those two methods.

Every folder also has a small demo program: the quote board for zone 3, with NightOwl's carrier written outside the tracker's code, the way their team wrote it. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project ShippingRates
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first, 4 tests pass and 3 fail. The 4 that pass are today's rates: keep them green the whole time, and run the tests after every small change. The 3 that fail are the ticket. A failing test's message starts with **Not yet:** (or **Keep this working:** if you broke something that worked) and says what it expected, for example *Not yet: NightOwl to zone 2 is $9 + $2 = $11.00.*

## When you're done

All 7 tests are green, and the demo prints:

```text
By name: QuickShip: $9.00
QuickShip: $11.00
EcoFreight: $7.70
SameDay: doesn't deliver to zone 3
NightOwl: $12.00
```

<details>
<summary>Stuck?</summary>

The quote screen has every carrier's numbers and rules inside one `switch` on the carrier's name, so a carrier it's never heard of, like NightOwl, can't be quoted at all. NightOwl's team has already shown the way out: a carrier can be an object that keeps the carrier promise and holds its own numbers. Make each of today's three carriers one of those (the **Strategy** pattern, as a class, from *Strategy: When the Rule Needs a Class of Its Own*): QuickShip holds its $5 and $2, EcoFreight its $7 and 10%, SameDay its $15 and its farthest zone. Then `quoteFor` only has to ask the carrier it's handed, `standardCarriers` hands back the three objects, and the quote by name can find a carrier in that list and ask it the same way.

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
