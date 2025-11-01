Senior Design - Intermediate Planning


Minimum viable project: Our minimal viable project is a simple, three-tiered tower that the user can play through.  Each of the tiers of the tower will be able to rotate, and when rotation is happening, there will be a short shaking camera animation that will play, signaling the player that the tower is rotating, which will keep them in place so as to not make the physics more complicated than they need to be.  Our game will have circuit puzzles, combination puzzles, water puzzles (involves water physics with ray casting), and Hades puzzles, which involves the character “dying” and being able to use bridges that they were unable to use before.  There will be a story to our game that the user will gain more insight to as they play along, as there will be voice acting and hints which will guide the user as they go about the tower.  The user will have an inventory in order to carry objects to solve puzzles, as well as a mini map which will aid them in seeing how the tower rotates and how it affects the environment around them.

Descriptions and diagrams of our system, sub-systems/components, and expected user interactions:



Specific tasks (and dependencies between tasks) that need to be completed in order to implement the systems and subsystems listed above:

Player system (all requirements/resources are simply Unity):  
Character movement:
Constraints: The only constraints are how Unity allows us to implement this task
Dependencies: No known dependencies
Minimap:
Constraints: No known constraints
Dependencies: Tower, tower movement, and surrounding environment
Item grabbing:
Constraints: No known constraints
Dependencies: Assets
Inventory:
Constraints: No known constraints
Dependencies: Assets
Physics:
Constraints: Unity physics
Dependencies: Need to have character model implemented

Environment system:
Assets:
Constraints: Unity
Dependencies: No known dependencies
Requirements/resources: Blender/Substance Painter
Puzzles:
Constraints: Unity
Dependencies: Assets/story
Requirements/resources: No known requirements/resources
Tower movement:
Constraints: No known constraints
Dependencies: Tower asset
Requirements/resources: No known requirements/resources
Voice/caption implementation:
Constraints: No known constraints
Dependencies: Unity triggers
Requirements/resources: UW recording studio/editing software
Hint implementation:
Constraints: No known constraints
Dependencies: Unity triggers
Requirements/resources: No known requirements/resources
Story:
Constraints: No known constraints
Dependencies: No known dependencies
Requirements/resources: No known requirements/resources
Map:
Constraints: Potential learning curves with designing a fully functional map
Dependencies: No known dependencies
Requirements/resources: Blender/Substance Painter
Render texture upscaling implementation:
Constraints: Learning curve
Dependencies: No known dependencies
Requirements/resources: Done in Unity by changing the resolution of textures created in Substance Painter
Color design:
Constraints: No known constraints
Dependencies: No known dependencies
Requirements/resources: No known requirements/resources
