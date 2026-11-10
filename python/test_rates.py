"""The tests for your code. You don't need to change this file.
Each message says what was expected, so a failing test tells you what to look at."""
from rates import quote, quote_for, standard_carriers


class NightOwl:
    """NightOwl's own carrier, written by their integration team: $9 plus $1 a zone, zones 1 to 3 only."""
    name = "NightOwl"

    def can_deliver(self, zone):
        return zone <= 3

    def rate(self, zone):
        return 900 + 100 * zone


# What the rate desk does today. These pass before you change anything: keep them passing.

def test_quickship_charges_five_plus_two_a_zone():
    assert quote("QuickShip", 1) == "QuickShip: $7.00", "Keep this working: QuickShip to zone 1 is $5 + $2 = $7.00."
    assert quote("QuickShip", 2) == "QuickShip: $9.00", "Keep this working: QuickShip to zone 2 is $5 + $4 = $9.00."
    assert quote("QuickShip", 4) == "QuickShip: $13.00", "Keep this working: QuickShip to zone 4 is $5 + $8 = $13.00."


def test_ecofreight_adds_its_fuel_charge():
    assert quote("EcoFreight", 2) == "EcoFreight: $7.70", "Keep this working: EcoFreight is $7 plus 10% fuel = $7.70."
    assert quote("EcoFreight", 4) == "EcoFreight: $7.70", "Keep this working: EcoFreight is the same to every zone: $7.70."


def test_sameday_goes_to_zones_one_and_two_only():
    assert quote("SameDay", 1) == "SameDay: $15.00", "Keep this working: SameDay to zone 1 is $15.00."
    assert quote("SameDay", 2) == "SameDay: $15.00", "Keep this working: SameDay to zone 2 is $15.00."
    assert quote("SameDay", 3) == "SameDay: doesn't deliver to zone 3", "Keep this working: SameDay doesn't go to zone 3."


def test_an_unknown_carrier_is_named():
    assert quote("FastFox", 2) == "No carrier called FastFox", "Keep this working: a name the desk doesn't know says so."


# The ticket's new requirements. These fail until the work is done.

def test_a_carrier_written_outside_the_tracker_can_be_quoted():
    assert quote_for(NightOwl(), 2) == "NightOwl: $11.00", "Not yet: NightOwl to zone 2 is $9 + $2 = $11.00."
    assert quote_for(NightOwl(), 4) == "NightOwl: doesn't deliver to zone 4", "Not yet: NightOwl's own rule says it doesn't go to zone 4."


def test_the_standard_carriers_are_the_three_we_offer():
    carriers = standard_carriers()
    assert [c.name for c in carriers] == ["QuickShip", "EcoFreight", "SameDay"], \
        "Not yet: the standard carriers are QuickShip, EcoFreight and SameDay, in that order."
    assert [quote_for(c, 3) for c in carriers] == ["QuickShip: $11.00", "EcoFreight: $7.70", "SameDay: doesn't deliver to zone 3"], \
        "Not yet: each standard carrier quotes zone 3 by its own rules."


def test_the_quote_board_lists_a_new_carrier_beside_the_others():
    board = standard_carriers() + [NightOwl()]
    assert [quote_for(c, 2) for c in board] == ["QuickShip: $9.00", "EcoFreight: $7.70", "SameDay: $15.00", "NightOwl: $11.00"], \
        "Not yet: the zone 2 board shows all four carriers, NightOwl last."
