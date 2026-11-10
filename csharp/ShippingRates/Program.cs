using ShippingRates;

// A demo: the quote board for zone 3, with NightOwl's carrier written outside the tracker. You don't need to change it.
Console.WriteLine("By name: " + RateDesk.Quote("QuickShip", 2));
List<ICarrier> board = RateDesk.StandardCarriers();
board.Add(new NightOwl());
foreach (ICarrier carrier in board)
{
    Console.WriteLine(RateDesk.QuoteFor(carrier, 3));
}

// NightOwl's carrier: $9 plus $1 a zone, zones 1 to 3 only.
class NightOwl : ICarrier
{
    public string Name => "NightOwl";
    public bool CanDeliver(int zone) => zone <= 3;
    public int Rate(int zone) => 900 + 100 * zone;
}
