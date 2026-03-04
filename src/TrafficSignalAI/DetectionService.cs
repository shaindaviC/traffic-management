using OpenCvSharp;

namespace TrafficSignalAI;

public interface IDetectionService
{
    Task<DetectionFrame> DetectAsync(CancellationToken cancellationToken);
}

/// <summary>
/// College-level prototype for road state detection.
/// Replace random simulation with YOLO model output parsing when your model is available.
/// </summary>
public class YoloDetectionService : IDetectionService
{
    private readonly Random _random = new();

    public Task<DetectionFrame> DetectAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Placeholder frame grab to show where OpenCV camera integration belongs.
        using var frame = new Mat();

        var roads = Enum.GetValues<RoadDirection>()
            .Select(direction =>
            {
                var vehicleCount = _random.Next(0, 18);
                var hasEmergency = _random.NextDouble() < 0.08;
                var isEmpty = vehicleCount == 0;

                // Congestion score can combine queue length, waiting time, and lane occupancy from YOLO.
                var congestionScore = hasEmergency
                    ? 999
                    : vehicleCount * 1.0 + (_random.NextDouble() * 2);

                return new RoadState(direction, vehicleCount, hasEmergency, isEmpty, congestionScore);
            })
            .ToList();

        return Task.FromResult(new DetectionFrame(DateTime.UtcNow, roads));
    }
}
