import java.util.ArrayList;
import java.util.List;

// A demo: the quote board for zone 3, with NightOwl's carrier written outside the tracker. You don't need to change it.
// Run it with: java src/main/java/Demo.java
public class Demo {
    // NightOwl's carrier: $9 plus $1 a zone, zones 1 to 3 only.
    static class NightOwl implements Carrier {
        public String name() { return "NightOwl"; }
        public boolean canDeliver(int zone) { return zone <= 3; }
        public int rate(int zone) { return 900 + 100 * zone; }
    }

    public static void main(String[] args) {
        System.out.println("By name: " + RateDesk.quote("QuickShip", 2));
        List<Carrier> board = new ArrayList<>(RateDesk.standardCarriers());
        board.add(new NightOwl());
        for (Carrier carrier : board) {
            System.out.println(RateDesk.quoteFor(carrier, 3));
        }
    }
}
