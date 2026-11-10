import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.ArrayList;
import java.util.List;
import org.junit.jupiter.api.Test;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class RatesTest {
    // NightOwl's own carrier, written by their integration team: $9 plus $1 a zone, zones 1 to 3 only.
    static class NightOwl implements Carrier {
        public String name() { return "NightOwl"; }
        public boolean canDeliver(int zone) { return zone <= 3; }
        public int rate(int zone) { return 900 + 100 * zone; }
    }

    // What the rate desk does today. These pass before you change anything: keep them passing.

    @Test
    void quickShipChargesFivePlusTwoAZone() {
        assertEquals("QuickShip: $7.00", RateDesk.quote("QuickShip", 1), "Keep this working: QuickShip to zone 1 is $5 + $2 = $7.00.");
        assertEquals("QuickShip: $9.00", RateDesk.quote("QuickShip", 2), "Keep this working: QuickShip to zone 2 is $5 + $4 = $9.00.");
        assertEquals("QuickShip: $13.00", RateDesk.quote("QuickShip", 4), "Keep this working: QuickShip to zone 4 is $5 + $8 = $13.00.");
    }

    @Test
    void ecoFreightAddsItsFuelCharge() {
        assertEquals("EcoFreight: $7.70", RateDesk.quote("EcoFreight", 2), "Keep this working: EcoFreight is $7 plus 10% fuel = $7.70.");
        assertEquals("EcoFreight: $7.70", RateDesk.quote("EcoFreight", 4), "Keep this working: EcoFreight is the same to every zone: $7.70.");
    }

    @Test
    void sameDayGoesToZonesOneAndTwoOnly() {
        assertEquals("SameDay: $15.00", RateDesk.quote("SameDay", 1), "Keep this working: SameDay to zone 1 is $15.00.");
        assertEquals("SameDay: $15.00", RateDesk.quote("SameDay", 2), "Keep this working: SameDay to zone 2 is $15.00.");
        assertEquals("SameDay: doesn't deliver to zone 3", RateDesk.quote("SameDay", 3), "Keep this working: SameDay doesn't go to zone 3.");
    }

    @Test
    void anUnknownCarrierIsNamed() {
        assertEquals("No carrier called FastFox", RateDesk.quote("FastFox", 2), "Keep this working: a name the desk doesn't know says so.");
    }

    // The ticket's new requirements. These fail until the work is done.

    @Test
    void aCarrierWrittenOutsideTheTrackerCanBeQuoted() {
        assertEquals("NightOwl: $11.00", RateDesk.quoteFor(new NightOwl(), 2), "Not yet: NightOwl to zone 2 is $9 + $2 = $11.00.");
        assertEquals("NightOwl: doesn't deliver to zone 4", RateDesk.quoteFor(new NightOwl(), 4), "Not yet: NightOwl's own rule says it doesn't go to zone 4.");
    }

    @Test
    void theStandardCarriersAreTheThreeWeOffer() {
        List<Carrier> carriers = RateDesk.standardCarriers();
        assertEquals(List.of("QuickShip", "EcoFreight", "SameDay"), carriers.stream().map(Carrier::name).toList(),
            "Not yet: the standard carriers are QuickShip, EcoFreight and SameDay, in that order.");
        assertEquals(List.of("QuickShip: $11.00", "EcoFreight: $7.70", "SameDay: doesn't deliver to zone 3"),
            carriers.stream().map(c -> RateDesk.quoteFor(c, 3)).toList(), "Not yet: each standard carrier quotes zone 3 by its own rules.");
    }

    @Test
    void theQuoteBoardListsANewCarrierBesideTheOthers() {
        List<Carrier> board = new ArrayList<>(RateDesk.standardCarriers());
        board.add(new NightOwl());
        assertEquals(List.of("QuickShip: $9.00", "EcoFreight: $7.70", "SameDay: $15.00", "NightOwl: $11.00"),
            board.stream().map(c -> RateDesk.quoteFor(c, 2)).toList(), "Not yet: the zone 2 board shows all four carriers, NightOwl last.");
    }
}
