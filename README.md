# Redundant-Space-Militia  
Repository for Team Redundant Space Militia's project in Dr. Shinar's CS179N course.


## Overview:  
< Insert Game Title Here > is a first person shooter with some light puzzle and RPG elements. We were tasked with seeing the prototype through to completion in ten weeks.

#### Data Box:  
**Engineers:** Ashley McDaniel, Deven Fafard, Jacques Fracchia, Jerry Kuei, Michael Rojas  
**Platform:** PC  
**Lines of Code:** 3,518 (written by us) + 139,287 (plugins + assets) = 142,805 lines  


## Game Description:  
#### Levels  
The game features 2 levels. The first level takes place in a rural setting, where the player’s main objective is to kill all of the invading enemies in order to board their ship. The second level takes place aboard the alien ship, where the player discovers the reason why the aliens invaded in the first place.

#### Gameplay  
The game is a traditional first person shooter. The player has access to a small arsenal of weapons in order to shoot (or melee strike) enemies. Additionally, the player can pick up some objects in order to solve small puzzles to proceed through the levels. 

The controller scheme is as follows:  
**Shoot Weapon:** left click  
**Drop Item:** right click  
**Look:**  move mouse  
**Move:** [W], [A], [S] [D]  
**Sprint:** [Shift]   
**Pick Up Object / Interact:** [E]  
**Select Weapon:** [1], [2], [3], [4]  


## Implementation:  
#### Game Engine  
We opted to use Unity (version 2019.1.14f) as our game engine. We chose it because of its robust documentation and community support. It is also free to use for students and professionals who profit less than $100,000 annually, which made it an obvious choice.

#### Scripting  
Overall, we strived to follow best practices for game development - mainly event driven programming and separation of concerns. This means that the core game logic was facilitated by a centralized event manager (in our case GameManager). Relevant objects such as the enemies, checkpoint objectives, and the player notify GameManager of changes to their states in an effort to keep core functionality out of Update(); polling for changes on every frame has the potential to be very expensive. GameManager would in turn pass these notifications to relevant subsystem controllers. This was accomplished with a simple observer pattern implementation. Relevant events were declared in the NotificationType enum so multiple controllers could respond to the same event. In general, we followed a structure as shown in the below diagram:

![Class Diagram](https://i.imgur.com/i3og5VX.png)

#### Tools  
In addition to Unity, we used the Game development with Unity workload in order to use Visual Studio as our IDE. To track effort in a more modular fashion, we used Trello in addition to the spreadsheet that was provided.


## Post Mortem:  
#### What tasks were accomplished?  
Most of the core gameplay mechanics were finished before Week 5. This included gameplay through Level 1. Various demo / internal ‘scratch work’ was repurposed to create Level 2. A major project refactor was completed around Week 9 in order to facilitate an easier merge of the two levels in the final weeks.

#### What planned tasks were not done?  
Gameplay improvements and bug fixes were not accomplished. Major game-breaking (and by extension demo-breaking) bugs were fixed, but many quality of life improvements were not made. The team felt that this was out of scope of the project given the fact that the TA encouraged us to prioritize demonstrable content.

#### How did scrum work for us?  
The team did not strictly follow the scrum process. Scrum was used to generate tasks and loosely prescribe meeting times, but daily standups / text updates were not often adhered to. We also used Trello, which is functionally a kanban board, for more granular task updates.

For generating tasks, prescribing point values were mostly meaningless - they ended up reflecting overall importance to completing a user story rather than the amount of effort it would take to complete a task.

Formal task validation was not conducted for this project. User stories were evaluated according to how well something would appear at the weekly demo rather than how well it truly met the acceptance criteria. This led to duplicated work and tasks that were not assigned being completed by others instead.

#### What would we do differently?  
Given a do-over, we would like to focus less on incorporating art assets early in development, as this led to an incoherent art style and impeded engineering efforts. We would also pay more attention to how we set up the repository before any work began so as not to “stomp” previously completed work or lose object references.
