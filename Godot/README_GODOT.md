# Quickly — Godot .NET (C#)

Proyecto Godot 4.7.2 Mono conectado al repo Unity `Quickly`.
No toca los archivos de Unity de la raíz (`Assets/`, `Packages/`...). Todo Godot vive en `Godot/`.

## Abrir

1. Abre **Godot Mono 4.7.2**:
   `C:\Users\BFW\GodotMono\Godot_v4.7.2-stable_mono_win64.exe`
   (NO uses el .exe suelto del Escritorio: le falta su carpeta `GodotSharp/` y por eso se cierra con `Unable to find the .NET assemblies directory`.)
2. **Importar →** selecciona `C:\Users\BFW\Documents\GitHub\Quickly\Godot\project.godot`
3. Pulsa **Build** (martillo arriba a la derecha) tras editar C#.

## .NET sin admin (portable)

No tienes SDK instalado a nivel sistema (requiere admin). Se usa portable:

- `C:\Users\BFW\dotnet\dotnet.exe` → `8.0.425`
- Variables de usuario configuradas: `DOTNET_ROOT=C:\Users\BFW\dotnet`, `PATH+=C:\Users\BFW\dotnet`
- Reinicia la terminal / VS Code para que apliquen.

Compilar manual:
```
%USERPROFILE%\dotnet\dotnet.exe build "C:\Users\BFW\Documents\GitHub\Quickly\Godot\Quickly.sln"
```

## Contenido

- `Main.tscn` — escena principal (nave + cámara + luz).
- `Scripts/ShipController.cs` — porteado desde Unity `Assets/Materials/Scenes/Scripts/ShipController.cs`.
- `Scripts/CameraFollow.cs` — porteado desde Unity `Assets/Materials/Scenes/Scripts/CameraFollow.cs`.
- `Quickly.sln` / `Quickly.csproj` — solución C# (`Godot.NET.Sdk/4.7.2`, `net8.0`). Compilación verificada: 0 errores.
- `.vscode/` — tarea `build` + config `Play en Godot` (usa dotnet portable).

## Notas Unity → Godot

- Forward en Unity `+Z`, en Godot `-Z` (ya ajustado con `Translate(0,0,-1)`).
- `Input.GetAxis("Horizontal")` → `Input.GetAxis("ui_left","ui_right")`.
- `TransformDirection(offset)` → `GlobalTransform * Offset`.
- Cámara: asigna `Target` en el editor a `../Ship` (ya viene en `Main.tscn`).
