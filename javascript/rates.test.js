// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { quote, quoteFor, standardCarriers } from "./rates.js";

// NightOwl's own carrier, written by their integration team: $9 plus $1 a zone, zones 1 to 3 only.
class NightOwl {
  name = "NightOwl";
  canDeliver(zone) { return zone <= 3; }
  rate(zone) { return 900 + 100 * zone; }
}

// What the rate desk does today. These pass before you change anything: keep them passing.

test("QuickShip charges $5 plus $2 a zone", () => {
  assert.equal(quote("QuickShip", 1), "QuickShip: $7.00", "Keep this working: QuickShip to zone 1 is $5 + $2 = $7.00.");
  assert.equal(quote("QuickShip", 2), "QuickShip: $9.00", "Keep this working: QuickShip to zone 2 is $5 + $4 = $9.00.");
  assert.equal(quote("QuickShip", 4), "QuickShip: $13.00", "Keep this working: QuickShip to zone 4 is $5 + $8 = $13.00.");
});

test("EcoFreight adds its fuel charge", () => {
  assert.equal(quote("EcoFreight", 2), "EcoFreight: $7.70", "Keep this working: EcoFreight is $7 plus 10% fuel = $7.70.");
  assert.equal(quote("EcoFreight", 4), "EcoFreight: $7.70", "Keep this working: EcoFreight is the same to every zone: $7.70.");
});

test("SameDay goes to zones 1 and 2 only", () => {
  assert.equal(quote("SameDay", 1), "SameDay: $15.00", "Keep this working: SameDay to zone 1 is $15.00.");
  assert.equal(quote("SameDay", 2), "SameDay: $15.00", "Keep this working: SameDay to zone 2 is $15.00.");
  assert.equal(quote("SameDay", 3), "SameDay: doesn't deliver to zone 3", "Keep this working: SameDay doesn't go to zone 3.");
});

test("an unknown carrier is named", () => {
  assert.equal(quote("FastFox", 2), "No carrier called FastFox", "Keep this working: a name the desk doesn't know says so.");
});

// The ticket's new requirements. These fail until the work is done.

test("a carrier written outside the tracker can be quoted", () => {
  assert.equal(quoteFor(new NightOwl(), 2), "NightOwl: $11.00", "Not yet: NightOwl to zone 2 is $9 + $2 = $11.00.");
  assert.equal(quoteFor(new NightOwl(), 4), "NightOwl: doesn't deliver to zone 4", "Not yet: NightOwl's own rule says it doesn't go to zone 4.");
});

test("the standard carriers are the three we offer", () => {
  const carriers = standardCarriers();
  assert.deepEqual(carriers.map((c) => c.name), ["QuickShip", "EcoFreight", "SameDay"], "Not yet: the standard carriers are QuickShip, EcoFreight and SameDay, in that order.");
  assert.deepEqual(carriers.map((c) => quoteFor(c, 3)), ["QuickShip: $11.00", "EcoFreight: $7.70", "SameDay: doesn't deliver to zone 3"], "Not yet: each standard carrier quotes zone 3 by its own rules.");
});

test("the quote board lists a new carrier beside the others", () => {
  const board = [...standardCarriers(), new NightOwl()];
  assert.deepEqual(board.map((c) => quoteFor(c, 2)), ["QuickShip: $9.00", "EcoFreight: $7.70", "SameDay: $15.00", "NightOwl: $11.00"], "Not yet: the zone 2 board shows all four carriers, NightOwl last.");
});
