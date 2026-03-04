namespace TrafficSignalAI;

public class TrafficController
{
    private readonly TimeSpan _minimumGreen = TimeSpan.FromSeconds(8);
    private readonly TimeSpan _maximumGreen = TimeSpan.FromSeconds(30);

    public RoadState SelectGreenRoad(DetectionFrame frame)
    {
        var emergencyRoad = frame.Roads.FirstOrDefault(r => r.HasEmergencyVehicle);
        if (emergencyRoad is not null)
        {
            return emergencyRoad;
        }

        // Skip empty roads and pick the highest congestion path.
        var rushRoad = frame.Roads
            .Where(r => !r.IsEmpty)
            .OrderByDescending(r => r.CongestionScore)
            .ThenByDescending(r => r.VehicleCount)
            .FirstOrDefault();

        // If every road is empty, default to North.
        return rushRoad ?? new RoadState(RoadDirection.North, 0, false, true, 0);
    }

    public TimeSpan ComputeGreenDuration(RoadState selectedRoad)
    {
        if (selectedRoad.HasEmergencyVehicle)
        {
            return _maximumGreen;
        }

        var scaledSeconds = Math.Clamp(6 + selectedRoad.VehicleCount * 2, _minimumGreen.TotalSeconds, _maximumGreen.TotalSeconds);
        return TimeSpan.FromSeconds(scaledSeconds);
    }
}
