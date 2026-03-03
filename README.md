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
1. Found and adapted the ice cream parlour model into the scene
2. Modified the window and the ice cream container models to fit the game, added custom colliders in the scene.
3. Decorated the scene
4. Imported and made the animations usable in the scene (and attempted to code the NPCs)

### Allen Gu
#### Dialogue framework built.
1. DialogueSet scriptable object added, including DialogueOption class, Dialogue class and DialogueSet class
2. DialogueController script added, which manages the logic of dialogues.
3. DialogueUIController script added, which makes the dialogue interactable through UI.
4. GameController script modified, making it able to call out dialogue function.



## Final Submission
### Group Devlog
Put your group Devlog here.


### Haoyi Zhang
Put your individual final Devlog here.
### Pengcheng Qi
Put your individual final Devlog here.
### Allen Gu


## Open-Source Assets
[Skybox Series Free - Avionx](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

[Ice Cream Parlor - BrimanFunkman](https://sketchfab.com/3d-models/ice-cream-parlor-e508c4a4e3864aedbfe1e6a7f0f8d6ec)

[NPC models and animations](https://www.mixamo.com/#/)

[Simple Low Poly Nature Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552)

[Low Poly Wind](https://assetstore.unity.com/packages/vfx/shaders/low-poly-wind-182586)



