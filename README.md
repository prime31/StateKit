StateKit
====

Lightweight and simple to use state machine implementing the "states as objects" system. The idea is to create a state machine for some target object (could be a player, NPC, overall game, menu system, etc) that allows you to separate all the states into separate classes while retaining access to the original target object. Setup is made as simple as
possible. Assuming the target object is of class SomeClass any states made should inherit from SKState&lt;SomeClass>. Changing state is just a matter of calling the changeState method and providing the class name of the state to change to. All states must implement 3 methods: begin, update and end. That's all there is to it. The rest is left up to the implementor.

Simple usage example:

``` csharp
// create a state machine that will work with an object of type SomeClass as the focus with an initial state of PatrollingState
var machine = new SKStateMachine<SomeClass>( someClass, new PatrollingState() );

// we can now add any additional states
_machine.addState( new AttackState() );
_machine.addState( new ChaseState() );

// reason (which is optional) allows the current state to check constraints and change state if desired then updates the state machine
// these two methods would typically be called in an Update/FixedUpdate of an object
machine.reason();
machine.update( Time.deltaTime );

// change states. the state machine will automatically create and cache an instance of the class (in this case ChasingState)
machine.changeState<ChasingState>();
```


StateKit now has big brother: SKMecanimStateKit. This is a StateKit state machine that is tailored to work with Mecanim. See the demo scene and comments for more info.



StateKitLite
====

StateKitLite is an even simpler, single class FSM. To use StateKitLite, you just subclass the StateKitLite class and provide an enum to satifsy the generic constraint (for example `class YourClass : StateKitLite<SomeEnum>`). The enum is then used to control the state machine. The naming conventions for the methods are best shown with an example. See below:

``` csharp
enum SomeEnum
{
	Walking,
	Idle
}

public class YourClass : StateKitLite<SomeEnum>()
{
	void Start()
	{
		initialState = SomeEnum.Idle;
	}

	void Walking_Enter() {}
	void Walking_Tick() {}
	void Walking_Exit() {}

	void Idle_Enter() {}
	void Idle_Tick() {}
	void Walking_Exit() {}
}
```


All state methods are optional. StateKitLite will cache the methods that you implemented at startup. You can change states at any time by setting the currentState property (for example `currentState = SomeEnum.Walking`). There are a few simple rules that you must follow in your subclass to make sure StateKitLite can function:

- if you implement Awake in your subclass you must call base.Awake()
- in either Awake or Start the initialState must be set
- if you implement Update in your subclass you must call base.Update()



License
-----

[Attribution-NonCommercial-ShareAlike 3.0 Unported](http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode) with [simple explanation](http://creativecommons.org/licenses/by-nc-sa/3.0/deed.en_US) with the attribution clause waived. You are free to use StateKit in any and all games that you make. You cannot sell StateKit directly or as part of a larger game asset.
