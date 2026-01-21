## DualiTD

A tower defence game.

### Built with

- Godot_v4.5.1-stable_mono_linux.x86_64
- Dotnet 10.0.101

### Exporting to Android

Required software:

1. Download Android Studio version `Android Studio Otter 2 Feature Drop | 2025.2.2 Patch 1` and run the setup wizard to get the Android SDK and other tools installed.
2. Get Java 17: `winget install EclipseAdoptium.Temurin.17.JDK`
3. In Godot, go to Project -> Install Android Build Template.

Connect an Android device via USB and enable USB debugging, make sure the computer can access the phone's storage and check that `adb devices` shows the connected device.

Then, run the following command in the terminal:

```bash
godot --headless --export-debug "Android" DualiTD.apk && adb install DualiTD.apk
```
