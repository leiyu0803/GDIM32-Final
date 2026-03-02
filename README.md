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
### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
[Skybox Series Free - Avionx](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

[Ice Cream Parlor - BrimanFunkman](https://sketchfab.com/3d-models/ice-cream-parlor-e508c4a4e3864aedbfe1e6a7f0f8d6ec)



