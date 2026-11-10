"""Lakeside Parcel's rate desk. Read the ticket in README.md first.

A carrier is any object with a name, can_deliver(zone) and rate(zone), its rate in cents.
NightOwl's integration team has already written their carrier that way.
"""


def quote(carrier_name, zone):
    """The quote screen today: a carrier picked by its name."""
    match carrier_name:
        case "QuickShip":
            cents = 500 + 200 * zone
        case "EcoFreight":
            cents = 700 + 700 * 10 // 100
        case "SameDay":
            if zone > 2:
                return f"SameDay: doesn't deliver to zone {zone}"
            cents = 1500
        case _:
            return f"No carrier called {carrier_name}"
    return f"{carrier_name}: {dollars(cents)}"


def quote_for(carrier, zone):
    """NightOwl's code calls this with its own carrier. It doesn't use the carrier yet."""
    return "Not supported yet"


def standard_carriers():
    """The quote board calls this for the carriers Lakeside Parcel offers. It's empty for now."""
    return []


def dollars(cents):
    return f"${cents // 100}.{cents % 100:02d}"
