// The promise a carrier keeps: its name, whether it delivers to a zone, and its rate there, in cents.
// NightOwl's integration team has already written their carrier against it.
interface Carrier {
    String name();
    boolean canDeliver(int zone);
    int rate(int zone);
}
