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

### Individual Devlog 1

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

### Individual Devlog 2



### Individual Devlog 3




## Final Submission
### Group Devlog
Put your group Devlog here.


### Team Member Name 1
Put your individual final Devlog here.
### Team Member Name 2
Put your individual final Devlog here.
### Allen Gu
#### Dialogue framework built.
1. DialogueSet scriptable object added, including DialogueOption class, Dialogue class and DialogueSet class
2. DialogueController script added, which manages the logic of dialogues.
3. DialogueUIController script added, which makes the dialogue interactable through UI.
4. GameController script modified, making it able to call out dialogue function.
#### Break-down
I ended up using most of the parts written in my break-down. The break-down helps our group to coordinate smoother and reduces the probability of cross-working. The break-down also helps me design the hierachy structure of dialogue data and can be edited and used more conveniently. There are also aspects that can improve, which is making the break-down more specific or creating sub-break-down since i still had trouble structuring the code.
## Open-Source Assets
[Skybox Series Free - Avionx](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

[Ice Cream Parlor - BrimanFunkman](https://sketchfab.com/3d-models/ice-cream-parlor-e508c4a4e3864aedbfe1e6a7f0f8d6ec)



