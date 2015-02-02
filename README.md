# README #

BehaveN is a functional oriented behavior tree implementation in C#. If you dislike OOP implementations then perhaps this will fit you better. If you are unfamiliar with behavior trees I would recommend the following links:

[AI Game Dev Video Tutorial](https://www.youtube.com/watch?v=n4aREFb3SsU)  
[Project Zomboid Behavior Tree Usage](http://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php)  

BehaveN differs from the "standard" OOP implementation by making the logic of the behavior tree nodes fully stateless and thus sharable between trees. Multiple actors can all use the same behavior tree "instance" across one or more threads safely. BehaveN also uses lambdas extensively so if you're not used to C# 3 lamba syntax you might want to read up on that.

BehaveN was created for use with Unity although there is nothing Unity specific in the core. A library of Unity specific nodes is available in a separate namespace.

### Nodes and state ###

There is only 1 (instantiated) class in BehaveN, the Node class which itself has just 3 delegates defining the initialize, tick, and success behaviors. Nodes are composed via parameters to these functions in a way that will be hopefully be familar to anyone who's spent some time in a functional language. For everyone else, we'll get to some examples shortly. All functionality including the behavior tree runner and the delegates that are supplied to the Node's fields are static.
The total state of a tree in BehaveN is simply a ```Dictionary<object, <Dictionary<string, object>>```. Now that might look complex and sort of ... eh? ... so let me explain.

* Root ```Dictionary<object, Dictionary<string, object>>```
  - Node specific ```Dictionary<string, object>```
  - Shared data ```Dictionary<string, object>```

The structure of this dictionary is intentionally very wide open. The "main" function, the BehaviorTree.Run function simply takes a Node and the root dictionary. You are responsible for creating this dictionary and providing it when you call Run. There is a 
Simlarily the shared data dictionary is also up to you to define. It is common to have some utility functions for accessing this dictionary via a constant key. In the Unity implementation I have ```GetSharedStateVariable<T>``` and ```SetSharedStateVariable<T>``` functions to encapsulate the details of reading and writing to this dictionary.

### Creating a tree structure ###

Given that a Node has no reference to other nodes, you may be asking how it is possible to create a tree structure? The answer lies in the fact that C# supports [closures](http://www.codethinked.com/c-closures-explained). If you open up the source to Sequence.cs or Selector.cs you can see that the nodes to be iterated over are passed in as a parameter to the Node function which then returns a new node with the appropriate functions set. Those functions however are the result of calling yet another function! (and passing along the list of nodes). The function returned from the Tick function contains a closure over the nodes and thus can "remember" them.

### Getting started ###

1. Get BehaveN
  1. Download the source and compile
  2. Copy the resulting dll(s) to your project
2. Determine how you're going to manage state
  1. If you're using Unity then feel free to build off of the example BehaviorTree component.
  2. If you're using a different system, determine what part of your system will be responsible for 

### Conventions ###

One convention established in the BehaveN (but not required by any means) is for each node to have a static class that contains a function named Node. This function is how new nodes of that type are constructed. This allows you to consistently do NodeType.Node() (optionally also having params to Node) to get a new node instance. For example, after adding some game specific nodes, you might end up with something like this:

```
public static Node EnemyAi() {
	return Sequence.Node(
		Selector.Node(
			IfHasTarget.Node(DETECTION_RANGE),
			GetTarget.Node(Faction.Player, DETECTION_RANGE),
			ClearTargetAndFail.Node() // if we get here, there is no target so clear the key
		),
		Sequence.Node(
			SetPathfindingToTargetTransform.Node()
		), 
		Sequence.Node(
			IfTargetWithinAbilityRange.Node(0), // here the 0 selects which ability to test
			ActivateAbility.Node(0) // here 0 selects which ability to activate
		)
	);
}
```

### License ###

This project is licensed under The MIT License
Copyright 2014 David Koontz, Trenton Kennedy

### Contact ###

Bugs, comments, complaints should be directed to the [BehaveN](https://github.com/dkoontz/BehaveN)

Or contact the authors directly:  
David Koontz - david@koontzfamily.org  
Trenton Kennedy - trentonkennedy@gmail.com
