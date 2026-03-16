# GDIM32-Final

### Team Member Name 1

Haoyi Zhang

### Team Member Name 2

Pengcheng Qi

### Team Member Name 3

Allen Gu

## Check-In
### Group Devlog

During development, I find a issue that after spawning the second NPC, players were unable to interact with it. I added a breakpoint at the location where NPC interaction occurs( `NPCInteract` at `GameController.cs` ). After checking the variables in my code, I found that the reference still pointed to the first NPC. So, when spawning new NPC, I adjusted the reference to point to the new NPC instead.

### Haoyi Zhang

`CameraMovement.cs`

`GameController.cs`

`InteractableBase.cs` and all subclass

`PlayerController.cs`

`PlayerMovement.cs`

Entire `TestScense`

`Start` and `GameOver`

Proposal is detailed enough.

We use Trello to track our process.

I will add a deadline on all feature listed on Trello.

### Pengcheng Qi
#### Found Art Resources and did parts of the NPC prefab.
1. Found and adapted the ice cream parlour model into the shop scene
2. Modified the window and the ice cream container models to fit the game, added custom colliders with cubes in the scene to block the player's movement.
3. Decorated the scene
4. Imported and made the animations usable in the scene with the prefabs NPC_Test1 and NPC_Test2, made animators for the NPCs with a ShouldMove bool variable to control the movement state of the NPC.(and attempted to code the NPCs' movements with the Move Sequence Test script and failed)
5. Added A navmesh on the scene using geometry with physics colliders in the navmesh surface and added cubes as custom colliders in order to bake the correct navmesh.

### Allen Gu
#### Dialogue framework built.
1. DialogueSet scriptable object added, including DialogueOption class, Dialogue class and DialogueSet class
2. DialogueController script added, which manages the logic of dialogues.
3. DialogueUIController script added, which makes the dialogue interactable through UI.
4. GameController script modified, making it able to call out dialogue function.
#### Break-down
I ended up using most of the parts written in my break-down. The break-down helps our group to coordinate smoother and reduces the probability of cross-working. The break-down also helps me design the hierachy structure of dialogue data and can be edited and used more conveniently. There are also aspects that can improve, which is making the break-down more specific or creating sub-break-down since i still had trouble structuring the code.



## Final Submission
### Group Devlog

#### MVC

##### Model

DialogueSet.cs is a scriptable object that stores the dialogue data, including all the branchings and if changing the order.
##### View
DialogueUIController.cs is a UI controller that controls all the UIs related to the dialgoue system. It converts the dialogue data into the form viewable form to player and displays it. It also controls the option UI.
##### Control
DialogueController.cs is the code that controls the dialgoue logic and handles the dialogue process.

### Singleton

We use singleton in GameController, PlayerController and DialogueController.

#### Finite State Machine

We use Finite State Machine in PlayerController, when player preparing ice cream. Player can go across three state, Empty, Container, and Finished. The state will go back to Empty if player interact with trashcan.

#### Inheritance & Polymorphism

All interactable object, include cup, cone, trashcan, ice-cream, and NPC are Inherited from a IntercatableBase class, contains if player is looking at them.  Each interactable object has different function, so we rewrite the Interact function.

### Haoyi Zhang
I created three NPC prefab, add them the function that can follow the waypoint in the map. I changed GameController so NPC can spawn and make order. I changed UI and DialogueUIController so that dialogue can be displayed on the screen. I changed PlayerMovement and collision in the sense so player now can jump. I added most of the SFX and make them function. I fixed some spelling mistake in ScriptableObject with Pengcheng.
### Pengcheng Qi
I looked for and arranged the assets for the scene, like the ice-cream shop, the npcs, the environment, and the props inside the ice-cream shop, added lights, and added collisions responsible for the movement of NPCs and the Player. I also made a rough first version of the NPCs' script and baked the Navmesh for the ice-cream scene by adding a lot of custom collisions. I fixed some minor problems like spelling mistakes in the dialogs and changed some minor things in scripts such as the GameController so that the player's hand can hold the correct item.
### Allen Gu
I wrote DialogueSet.cs, DialogueController.cs and DialogueUIController.cs, edited PlayerController.cs, GameController.cs, added and edited DialogueController GameObject, created and filled DisruptiveCustomer scriptable object, IndecisiveCustomer scriptable object and RegularCustomer scriptable object.



## Assets
[Skybox Series Free - Avionx](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

[Ice Cream Parlor - BrimanFunkman](https://sketchfab.com/3d-models/ice-cream-parlor-e508c4a4e3864aedbfe1e6a7f0f8d6ec)

[NPC models and animations](https://www.mixamo.com/#/)

[Simple Low Poly Nature Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552)

[Lights](https://free3d.com/3d-model/ceiling-light-41651.html)

[Low Poly Wind](https://assetstore.unity.com/packages/vfx/shaders/low-poly-wind-182586)

[Get Out SFX](https://www.myinstants.com/en/instant/tuco-get-out-30566/)

[BGM and other SFX](https://pixabay.com/)

