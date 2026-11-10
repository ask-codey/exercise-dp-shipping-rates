import java.util.List;

// Lakeside Parcel's rate desk. Read the ticket in README.md first.
class RateDesk {
    // The quote screen today: a carrier picked by its name.
    static String quote(String carrierName, int zone) {
        int cents;
        switch (carrierName) {
            case "QuickShip":
                cents = 500 + 200 * zone;
                break;
            case "EcoFreight":
                cents = 700 + 700 * 10 / 100;
                break;
            case "SameDay":
                if (zone > 2) { return "SameDay: doesn't deliver to zone " + zone; }
                cents = 1500;
                break;
            default:
                return "No carrier called " + carrierName;
        }
        return carrierName + ": " + dollars(cents);
    }

    // NightOwl's code calls this with its own carrier. It doesn't use the carrier yet.
    static String quoteFor(Carrier carrier, int zone) {
        return "Not supported yet";
    }

    // The quote board calls this for the carriers Lakeside Parcel offers. It's empty for now.
    static List<Carrier> standardCarriers() {
        return List.of();
    }

    static String dollars(int cents) {
        return "$" + cents / 100 + "." + String.format("%02d", cents % 100);
    }
}
