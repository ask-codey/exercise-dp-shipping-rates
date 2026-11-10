namespace ShippingRates;

// Lakeside Parcel's rate desk. Read the ticket in README.md first.

// The promise a carrier keeps: its name, whether it delivers to a zone, and its rate there, in cents.
// NightOwl's integration team has already written their carrier against it.
public interface ICarrier
{
    string Name { get; }
    bool CanDeliver(int zone);
    int Rate(int zone);
}

public static class RateDesk
{
    // The quote screen today: a carrier picked by its name.
    public static string Quote(string carrierName, int zone)
    {
        int cents;
        switch (carrierName)
        {
            case "QuickShip":
                cents = 500 + 200 * zone;
                break;
            case "EcoFreight":
                cents = 700 + 700 * 10 / 100;
                break;
            case "SameDay":
                if (zone > 2) { return $"SameDay: doesn't deliver to zone {zone}"; }
                cents = 1500;
                break;
            default:
                return $"No carrier called {carrierName}";
        }
        return $"{carrierName}: {Dollars(cents)}";
    }

    // NightOwl's code calls this with its own carrier. It doesn't use the carrier yet.
    public static string QuoteFor(ICarrier carrier, int zone) => "Not supported yet";

    // The quote board calls this for the carriers Lakeside Parcel offers. It's empty for now.
    public static List<ICarrier> StandardCarriers() => new List<ICarrier>();

    public static string Dollars(int cents) => $"${cents / 100}.{cents % 100:D2}";
}
