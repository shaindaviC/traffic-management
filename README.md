# AI-Based Four-Road Traffic Signal (College Project Starter)

Yes — in a 4-way junction you typically model **four incoming roads** (North, East, South, West). Your idea is solid for a college project:

1. Detect traffic density per road using camera + YOLO.
2. If one road is empty, avoid giving it unnecessary green time.
3. Dynamically give green to the most congested path.
4. If an emergency vehicle is detected (visual class + optional siren cue), immediately prioritize that path.

This repo contains a C# starter implementation for that logic.

## Project structure

- `src/TrafficSignalAI/Program.cs` — app loop.
- `src/TrafficSignalAI/DetectionService.cs` — YOLO/OpenCV integration point (currently simulated values).
- `src/TrafficSignalAI/TrafficController.cs` — signal decision logic (rush road + emergency override).
- `src/TrafficSignalAI/Models.cs` — shared models.

## How to run

1. Install .NET 8 SDK.
2. Run:

```bash
cd src/TrafficSignalAI
dotnet restore
dotnet run
```

## How to connect a real YOLO model

- Export YOLO model to ONNX.
- In `YoloDetectionService`, replace random values with:
  - camera capture using OpenCV,
  - ONNX inference output parsing,
  - counting vehicles by lane/road,
  - emergency vehicle class detection (ambulance, fire truck, police).

## Suggested college-level enhancements

- Add per-road waiting-time score.
- Add yellow and all-red safety phases.
- Add a small dashboard (ASP.NET or WinForms/WPF).
- Persist logs to CSV/SQLite for report graphs.
- Add manual override mode for authorities.

## Git push steps

```bash
git add .
git commit -m "Add C# YOLO-ready smart traffic signal prototype"
git push origin <your-branch-name>
```

