// Lakeside Parcel's rate desk. Read the ticket in README.md first.

// The promise a carrier keeps: its name, whether it delivers to a zone, and its rate there, in cents.
// NightOwl's integration team has already written their carrier against it.
export interface Carrier {
  name: string;
  canDeliver(zone: number): boolean;
  rate(zone: number): number;
}

// The quote screen today: a carrier picked by its name.
export function quote(carrierName: string, zone: number): string {
  let cents: number;
  switch (carrierName) {
    case "QuickShip":
      cents = 500 + 200 * zone;
      break;
    case "EcoFreight":
      cents = 700 + Math.floor(700 * 10 / 100);
      break;
    case "SameDay":
      if (zone > 2) { return `SameDay: doesn't deliver to zone ${zone}`; }
      cents = 1500;
      break;
    default:
      return `No carrier called ${carrierName}`;
  }
  return `${carrierName}: ${dollars(cents)}`;
}

// NightOwl's code calls this with its own carrier. It doesn't use the carrier yet.
export function quoteFor(carrier: Carrier, zone: number): string {
  return "Not supported yet";
}

// The quote board calls this for the carriers Lakeside Parcel offers. It's empty for now.
export function standardCarriers(): Carrier[] {
  return [];
}

export function dollars(cents: number): string {
  return `$${Math.floor(cents / 100)}.${String(cents % 100).padStart(2, "0")}`;
}
