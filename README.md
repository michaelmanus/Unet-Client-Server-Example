# Unet-Based Client/Server Approach: The Ghost Technique
Using Unity's High Level Network API (HLAPI) in a testable, easy to understand fashion with minimal magic.

The purpose of this project is to show a working example that has gotten around many pitfalls of working with Unet.

## Benefits 
* **Rapid Development** - This technique does not require a build to see both client and server running in a reasonable manner allowing you to test network functionality without running a build.
* **Testing** - You can see both the client and the server co-existing in your unity scene. The server objects can be replaced by translucent, non-colliding entities to track how the server 'sees' as well as the clients.
* **Distinguished Client and Server Functionality** - Sometimes, you don't want to replicate all the server's components or vice versa. Unet's built for this functionality but the separation can be muddy with conditionals all over the place. This technique makes it clear who does what.
* **Dynamic Client Prefabs** - This is something a lot of people have been asking about. Unity's network manager doesn't make it easy to spawn dynamically created objects on the client. This approach provides a path.

## Caveats
* **No "Host" Concept** - This is for client/server type games. The single player version of the game will always go through the network (even if it's just localhost). Not great for making a mostly single player game with network capabilities.
* **No Network Transforms** - Though this technique does not rule them out, I find the network transforms inconsistent and ambiguous - magic - so this example avoids them.

## Running the Example
In the example scene, we have **client** and **server** top level game objects. These both run together for testing purposes but don't need to be. 
To take a look at them running individually, simply the disable respective top level game object and build. Make sure you have exactly one server while running multiple builds. :)

The example contains a red capsule running around randomly. This is an npc example completely controlled by the server. The green capsule is a player. The objects movement is sync'd with the server but there is no movement prediction - this was for simplicity.