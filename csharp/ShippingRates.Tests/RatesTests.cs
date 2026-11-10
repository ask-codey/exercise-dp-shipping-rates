using ShippingRates;

namespace ShippingRates.Tests;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

// NightOwl's own carrier, written by their integration team: $9 plus $1 a zone, zones 1 to 3 only.
public class NightOwl : ICarrier
{
    public string Name => "NightOwl";
    public bool CanDeliver(int zone) => zone <= 3;
    public int Rate(int zone) => 900 + 100 * zone;
}

[TestClass]
public class RatesTests
{
    // What the rate desk does today. These pass before you change anything: keep them passing.

    [TestMethod]
    public void QuickShipChargesFivePlusTwoAZone()
    {
        Assert.AreEqual("QuickShip: $7.00", RateDesk.Quote("QuickShip", 1), "Keep this working: QuickShip to zone 1 is $5 + $2 = $7.00.");
        Assert.AreEqual("QuickShip: $9.00", RateDesk.Quote("QuickShip", 2), "Keep this working: QuickShip to zone 2 is $5 + $4 = $9.00.");
        Assert.AreEqual("QuickShip: $13.00", RateDesk.Quote("QuickShip", 4), "Keep this working: QuickShip to zone 4 is $5 + $8 = $13.00.");
    }

    [TestMethod]
    public void EcoFreightAddsItsFuelCharge()
    {
        Assert.AreEqual("EcoFreight: $7.70", RateDesk.Quote("EcoFreight", 2), "Keep this working: EcoFreight is $7 plus 10% fuel = $7.70.");
        Assert.AreEqual("EcoFreight: $7.70", RateDesk.Quote("EcoFreight", 4), "Keep this working: EcoFreight is the same to every zone: $7.70.");
    }

    [TestMethod]
    public void SameDayGoesToZonesOneAndTwoOnly()
    {
        Assert.AreEqual("SameDay: $15.00", RateDesk.Quote("SameDay", 1), "Keep this working: SameDay to zone 1 is $15.00.");
        Assert.AreEqual("SameDay: $15.00", RateDesk.Quote("SameDay", 2), "Keep this working: SameDay to zone 2 is $15.00.");
        Assert.AreEqual("SameDay: doesn't deliver to zone 3", RateDesk.Quote("SameDay", 3), "Keep this working: SameDay doesn't go to zone 3.");
    }

    [TestMethod]
    public void AnUnknownCarrierIsNamed()
    {
        Assert.AreEqual("No carrier called FastFox", RateDesk.Quote("FastFox", 2), "Keep this working: a name the desk doesn't know says so.");
    }

    // The ticket's new requirements. These fail until the work is done.

    [TestMethod]
    public void ACarrierWrittenOutsideTheTrackerCanBeQuoted()
    {
        Assert.AreEqual("NightOwl: $11.00", RateDesk.QuoteFor(new NightOwl(), 2), "Not yet: NightOwl to zone 2 is $9 + $2 = $11.00.");
        Assert.AreEqual("NightOwl: doesn't deliver to zone 4", RateDesk.QuoteFor(new NightOwl(), 4), "Not yet: NightOwl's own rule says it doesn't go to zone 4.");
    }

    [TestMethod]
    public void TheStandardCarriersAreTheThreeWeOffer()
    {
        List<ICarrier> carriers = RateDesk.StandardCarriers();
        CollectionAssert.AreEqual(new List<string> { "QuickShip", "EcoFreight", "SameDay" }, carriers.Select(c => c.Name).ToList(), "Not yet: the standard carriers are QuickShip, EcoFreight and SameDay, in that order.");
        CollectionAssert.AreEqual(
            new List<string> { "QuickShip: $11.00", "EcoFreight: $7.70", "SameDay: doesn't deliver to zone 3" },
            carriers.Select(c => RateDesk.QuoteFor(c, 3)).ToList(),
            "Not yet: each standard carrier quotes zone 3 by its own rules.");
    }

    [TestMethod]
    public void TheQuoteBoardListsANewCarrierBesideTheOthers()
    {
        List<ICarrier> board = RateDesk.StandardCarriers();
        board.Add(new NightOwl());
        CollectionAssert.AreEqual(
            new List<string> { "QuickShip: $9.00", "EcoFreight: $7.70", "SameDay: $15.00", "NightOwl: $11.00" },
            board.Select(c => RateDesk.QuoteFor(c, 2)).ToList(),
            "Not yet: the zone 2 board shows all four carriers, NightOwl last.");
    }
}
