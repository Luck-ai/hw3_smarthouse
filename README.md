# Smart House — AR Homework 3

An interactive Unity XR smart-house scene featuring a 3D mansion environment and an animated main gate controlled by an in-scene UI button.

## Features

- 3D mansion and house-interior assets
- Interactive main gate that opens and closes with animated rotation
- XR-ready project using Unity's Input System, OpenXR, AR Foundation, and XR Interaction Toolkit
- Universal Render Pipeline (URP) rendering

## Requirements

- Unity `6000.3.19f1`
- Unity Hub with the XR/OpenXR modules required by the target device

The main XR packages include:

- XR Interaction Toolkit `3.4.1`
- AR Foundation `6.4.1`
- OpenXR `1.16.1`
- XR Hands `1.7.3`
- Universal Render Pipeline `17.3.0`

## Getting Started

1. Clone this repository.
2. Open the project in Unity `6000.3.19f1`.
3. Open `Assets/Cliff Mansion/Scene/Scene.unity` for the interactive mansion and gate scene.
4. Press **Play** and use the scene's gate button to open or close the main gate.

The gate behaviour is implemented in `Assets/Scripts/LuckyScript.cs`.

## Available Scenes

- `Assets/Cliff Mansion/Scene/Scene.unity` — primary mansion scene with the interactive gate.
- `Assets/Scenes/SmartHouse.unity` — smart-house showcase scene.
- `Assets/Scenes/BasicScene.unity` — basic XR interaction scene.
- `Assets/Scenes/SampleScene.unity` — scene currently listed in Unity Build Settings.

## Project Structure

```text
Assets/
├── Cliff Mansion/       Mansion environment and interactive scene
├── HousePack/           House models and materials
├── Scenes/              Project scenes
├── Scripts/             Custom project scripts
└── VRTemplateAssets/    XR interaction and template assets
Packages/                Unity package manifest and lock file
ProjectSettings/         Unity project and build settings
```

## Version Control

Unity-generated cache folders such as `Library`, `Temp`, `Logs`, and `UserSettings` are excluded through `.gitignore`. Source assets, scenes, scripts, packages, and project settings are tracked in Git.
