namespace TrafficSignalAI;

public enum RoadDirection
{
    North,
    East,
    South,
    West
}

public record RoadState(
    RoadDirection Direction,
    int VehicleCount,
    bool HasEmergencyVehicle,
    bool IsEmpty,
    double CongestionScore
);

public record DetectionFrame(DateTime Timestamp, IReadOnlyList<RoadState> Roads);
