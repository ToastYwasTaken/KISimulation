KISimulation showcases a basic AI behaviour with states IDLE, PATROL, GROUP, ATTACK and EVADE
These are coded with help of Unity's animator and the StateMachineBehaviour.

Functionality:
IDLE: The AI idles as long as the player is not in reach.
PATROL: The AI patrols between each time randomly generated waypoints.
GROUP: The Agent groups up with other agents if any in reach.
ATTACK: The AI attacks the player as soon as it spots him.
EVADE: The agent evades when his health gets low (condition trigger - player attack - not coded yet).

Also i additionaly implemented a random spawning system, that considers all objects on the plane!
Same algorithm is used to generate random WayPoints for the agents.
I had trouble configuring the grouping mechanism, but now it finally works (no bugs found so far)


TODO: I still want to add the player's ability to attack... Might not implement it due to time saving issues.
The evade mechanism itself is already implemented!
