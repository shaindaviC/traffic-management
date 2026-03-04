using TrafficSignalAI;

var detectionService = new YoloDetectionService();
var controller = new TrafficController();

Console.WriteLine("AI Traffic Signal Prototype (C# + YOLO-ready)");
Console.WriteLine("Press Ctrl+C to stop.\n");

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    eventArgs.Cancel = true;
    cts.Cancel();
};

while (!cts.Token.IsCancellationRequested)
{
    var frame = await detectionService.DetectAsync(cts.Token);
    var selectedRoad = controller.SelectGreenRoad(frame);
    var greenDuration = controller.ComputeGreenDuration(selectedRoad);

    Console.WriteLine($"[{frame.Timestamp:HH:mm:ss}] GREEN => {selectedRoad.Direction} | vehicles={selectedRoad.VehicleCount} | emergency={selectedRoad.HasEmergencyVehicle} | duration={greenDuration.TotalSeconds}s");

    await Task.Delay(TimeSpan.FromSeconds(2), cts.Token);
}
