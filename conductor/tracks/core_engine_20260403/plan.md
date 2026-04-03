# Implementation Plan: Setup core Space Invaders engine and player movement

## Phase 1: Project Scaffolding
- [ ] Task: Initialize .NET 8 console project and test projects
    - [ ] Create the main console app and xUnit project
    - [ ] Install Spectre.Console, FluentAssertions, and Moq
- [ ] Task: Conductor - User Manual Verification 'Phase 1: Project Scaffolding' (Protocol in workflow.md)

## Phase 2: Game Engine Foundation
- [ ] Task: Implement the core game loop and state management
    - [ ] Write unit tests for loop timing and state updates
    - [ ] Implement the high-performance main loop
- [ ] Task: Setup Spectre.Console rendering system
    - [ ] Initialize the Canvas or Layout-based render target
- [ ] Task: Conductor - User Manual Verification 'Phase 2: Game Engine Foundation' (Protocol in workflow.md)

## Phase 3: Player Controls
- [ ] Task: Implement lateral player movement
    - [ ] Write tests for position clamping and input handling
    - [ ] Implement movement logic (A/D keys)
- [ ] Task: Implement projectile firing
    - [ ] Write tests for bullet spawning and upward motion
    - [ ] Implement fire logic (Space key)
- [ ] Task: Conductor - User Manual Verification 'Phase 3: Player Controls' (Protocol in workflow.md)
