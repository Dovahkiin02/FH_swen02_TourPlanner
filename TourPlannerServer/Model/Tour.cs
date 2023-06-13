namespace TourPlannerServer.Model {
    public enum TransportType {
        FASTEST,
        SHORTEST,
        BICYCLE,
        PEDESTRIAN,
        MULTIMODAL,
    }

    public record Tour {

        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string From { get; init; }
        public string To { get; init; }
        public TransportType TransportType { get; init; }
        public float Distance { get; init; }
        public TimeSpan EstTime { get; init; }
        public string Info { get; init; }
        public List<TourLog> TourLogs { get; init; } = new List<TourLog>();

        public override string ToString() {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}, EstTime: {EstTime}, Info: {Info}";
        }
    }
}
