# Implementation Plan: Setup core Space Invaders engine and player movement

## Phase 1: Project Scaffolding
- [x] Task: Initialize .NET 10 console project and test projects
    - [x] Create the main console app and xUnit project
    - [x] Install Spectre.Console, FluentAssertions, and Moq
- [x] Task: Conductor - User Manual Verification 'Phase 1: Project Scaffolding' (Protocol in workflow.md)

## Phase 2: Game Engine Foundation
- [x] Task: Implement the core game loop and state management
    - [x] Write unit tests for loop timing and state updates
    - [x] Implement the high-performance main loop
- [x] Task: Setup Spectre.Console rendering system
    - [x] Initialize the Canvas or Layout-based render target
- [x] Task: Conductor - User Manual Verification 'Phase 2: Game Engine Foundation' (Protocol in workflow.md)

## Phase 3: Player Controls
- [ ] Task: Implement lateral player movement
    - [ ] Write tests for position clamping and input handling
    - [ ] Implement movement logic (A/D keys)
- [ ] Task: Implement projectile firing
    - [ ] Write tests for bullet spawning and upward motion
    - [ ] Implement fire logic (Space key)
- [ ] Task: Conductor - User Manual Verification 'Phase 3: Player Controls' (Protocol in workflow.md)
