# Button Hunter

## Overview
**Button Hunter** is a turn-based puzzle game developed as part of my bachelor’s thesis.  
The game explores how **automated planning** can be applied in game AI by integrating the **Planning Domain Definition Language (PDDL)** and the **FastDownward planner** into a Unity-based environment.

In Button Hunter, the player manipulates the environment by placing walls to influence enemy movement. Enemies (represented as “buttons”) autonomously adapt their strategies using automated planning, always attempting to reach the goal tile via the shortest path. The challenge lies in guiding them over traps by cleverly shaping their path.

---

## Controls
- **Mouse**
  - Left-click to place walls  
  - Navigate menus and interact with UI  

---

## Gameplay
1. The game is **turn-based**. The player acts first by placing a wall.  
2. Enemies (“buttons”) then move one step along the **shortest available path** toward the goal tile.  
3. The objective is to **manipulate enemy paths** so they step onto traps before reaching the goal.  
4. Win by eliminating all enemies. Lose if any enemy reaches the goal untrapped.  

---

## Technologies Used
- **Unity** (version: 2022.3.45f1 )  
- **PDDL (Planning Domain Definition Language)**  
- **FastDownward planner**  
- **C#** for Unity scripting  
