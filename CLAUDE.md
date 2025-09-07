# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an educational Unity 3D project for the "Animation and Interactivity" class. It provides a modular toolkit of scripts that students can use to create interactive experiences without needing to write code. The core design philosophy centers around UnityEvents, allowing students to visually wire together behaviors in the Unity Inspector to create complex interactive systems.

The project features a ball physics-based game template as the foundation, with an expanding library of reusable components for triggers, animations, spawning, collection mechanics, and more.

## Key Unity Project Structure

- **Main Scene**: `Assets/Scenes/ballPlayer.unity`
- **Educational Scripts**: `Assets/Scripts/` (organized by function)
  - `Input/` - Event source components
  - `Actions/` - Event target components
  - `Physics/` - Movement and physics systems
  - `Game/` - Game management systems
  - `UI/` - User interface components (future)
  - `Examples/` - Example combinations (future)
- **Input Configuration**: `Assets/InputSystem_Actions.inputactions`
- **Solution File**: `interactionTemplate.sln`

## Development Commands

### Building and Running
- Open the project in Unity Editor (Unity version determined by ProjectSettings)
- Build using Unity's Build Settings (Ctrl+Shift+B)
- Play in editor using Unity's Play button or Ctrl+P

### Testing
- Unity Test Framework is available (`com.unity.test-framework`: "1.4.5")
- Run tests through Unity Test Runner window (Window > General > Test Runner)

### Unity Packages Used
- Input System (`com.unity.inputsystem`: "1.11.2") - Modern input handling
- Universal Render Pipeline (`com.unity.render-pipelines.universal`: "17.0.3") - Rendering
- Cinemachine (`com.unity.cinemachine`: "3.1.2") - Camera management
- AI Navigation (`com.unity.ai.navigation`: "2.0.5") - Pathfinding
- Adobe Substance 3D (`Assets/Adobe/Substance3DForUnity/`) - Material authoring

## Core Game Architecture

### Player Controller System
- `BallController.cs` - Main physics-based ball controller with:
  - Camera-relative movement using Rigidbody forces
  - Ground detection via sphere casting
  - Jump mechanics with grounded checking
  - Input System integration via OnMove/OnJump callbacks

### Input System
- Uses Unity's new Input System with action-based bindings
- Input actions defined in `InputSystem_Actions.inputactions`
- Movement mapped to WASD/gamepad stick
- Jump mapped to Space/gamepad button

### Educational Component Library

#### Input Components (Event Sources)
Located in `Assets/Scripts/Input/`:
- `InputKeyPress.cs` - Simple key press event system
- `InputKeyCountdown.cs` - Key-based countdown system with TextMeshPro display
- `InputTriggerZone.cs` - 3D collision detection by tag
- `InputQuitGame.cs` - Application quit on Escape key

#### Action Components (Event Targets)  
Located in `Assets/Scripts/Actions/`:
- `ActionSpawnObject.cs` - Manual single object spawning
- `ActionAutoSpawner.cs` - Automatic object spawner with:
  - Random spawn timing between min/max intervals
  - Multiple prefab support with random selection
  - Positional variance using insideUnitSphere
- `ActionRestartScene.cs` - Scene restart functionality (R key)

#### Physics Components
Located in `Assets/Scripts/Physics/`:
- `PhysicsPlayerController.cs` - Ball-based player controller with camera-relative movement
- `PhysicsBumper.cs` - Advanced bumper/repulsion system with:
  - Configurable force direction (collision normal or radial)
  - Scale animation with custom curves and per-axis scaling
  - Material emission effects for visual feedback
  - Cooldown system with events
  - Comprehensive editor gizmos and debugging
- `PhysicsPlatformStick.cs` - Moving platform attachment system using physics forces

#### Game Management Components
Located in `Assets/Scripts/Game/`:
- `GameCollectionManager.cs` - Score/collection system with TextMeshPro display and threshold events

## Code Conventions

- **Naming**: Educational naming convention implemented:
  - **New Scripts**: `[Category][Purpose]` format (e.g., `InputKeyPress`, `ActionSpawnObject`)
  - **Legacy Scripts**: Mixed conventions remain in root folder for migration
  - **Categories**: Input, Action, Physics, Game, UI
- **Physics**: Uses Unity's Rigidbody system with `linearVelocity` (new Unity physics API)
- **Input**: Mixed approaches - new Input System callbacks and legacy `Input.GetKeyDown()`
- **UI**: TextMeshPro integration for text display (`TMPro` namespace)
- **Events**: Heavy use of UnityEvents for designer-friendly connections
- **Editor Tools**: Advanced scripts include custom gizmos and editor debugging features
- **Organization**: Scripts grouped by functionality in `Assets/Scripts/`

## Educational Design Philosophy

### UnityEvent-Driven Architecture
- **No-Code Approach**: Students create interactivity by connecting UnityEvents in the Inspector
- **Visual Learning**: Event connections are visible and easy to understand
- **Modular Design**: Each script serves a specific purpose and can be combined with others
- **Designer-Friendly**: Non-programmers can create complex interactions

### Script Categories for Student Use

#### Input Components (Event Sources)
- Key press events, collision detection, trigger zones
- Countdown systems, collection thresholds

#### Action Components (Event Targets)  
- Object spawning, scene management, animation triggers
- Score updates, platform movement, material effects

#### Hybrid Components
- Scripts that both receive and send events for complex chains

### Learning Outcomes
- Understanding event-driven programming concepts
- Grasping component-based architecture
- Learning physics and animation principles
- Developing systems thinking for interactive design

## Development Notes

- Project uses Universal Render Pipeline (URP) for rendering
- Ground detection implemented via Physics.CheckSphere rather than raycast
- Camera system expects a main camera for movement direction calculation
- Force-based movement with velocity clamping for responsive physics feel
- UnityEvent system is the primary interface for student interactions
- Advanced scripts feature comprehensive editor tooling with custom gizmos and scene handles
- Material instances are properly managed to avoid shared material modification
- Mixed Input System usage - newer scripts use Input System, older ones use legacy Input class
- Platform movement uses physics forces rather than direct transform manipulation
- Spawning systems support both random and manual triggering patterns

### Adding New Educational Components
When creating new scripts for student use:
1. Expose key parameters as public SerializeField fields
2. Include UnityEvents for both input and output where appropriate
3. Add helpful tooltips and headers for student understanding
4. Consider editor gizmos for visual feedback
5. Follow the underscore naming convention for utility scripts (`_scriptName`)