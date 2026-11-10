"""A demo: the quote board for zone 3, with NightOwl's carrier written outside the tracker. You don't need to change it."""
from rates import quote, quote_for, standard_carriers


class NightOwl:
    """NightOwl's carrier: $9 plus $1 a zone, zones 1 to 3 only."""
    name = "NightOwl"

    def can_deliver(self, zone):
        return zone <= 3

    def rate(self, zone):
        return 900 + 100 * zone


print("By name: " + quote("QuickShip", 2))
for carrier in standard_carriers() + [NightOwl()]:
    print(quote_for(carrier, 3))
