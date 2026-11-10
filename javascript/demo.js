// A demo: the quote board for zone 3, with NightOwl's carrier written outside the tracker. You don't need to change it.
import { quote, quoteFor, standardCarriers } from "./rates.js";

// NightOwl's carrier: $9 plus $1 a zone, zones 1 to 3 only.
class NightOwl {
  name = "NightOwl";
  canDeliver(zone) { return zone <= 3; }
  rate(zone) { return 900 + 100 * zone; }
}

console.log("By name: " + quote("QuickShip", 2));
for (const carrier of [...standardCarriers(), new NightOwl()]) {
  console.log(quoteFor(carrier, 3));
}
