StateKit
====

Lightweight and simple to use state machine implementing the "states as objects" system. The idea is to create a state machine for some target object (could be a player, NPC,
overall game, menu system, etc) that allows you to separate all the states into separate classes while retaining access to the original target object.  Setup is made as simple as
possible. Assuming the target object is of class SomeClass any states made should inherit from SKState&lt;SomeClass>. Changing state is just a matter of calling the changeState
method and providing the class name of the state to change to. All states must implement 3 methods: begin, update and end. That's all there is to it. The rest is left up to the implementor.

Simple usage example:

    // create a state machine that will work with an object of type SomeClass as the focus with an initial state of PatrollingState
    var machine = new SKStateMachine<SomeClass>( someClass, typeof( PatrollingState ) );
	
	// another option for the state machine constructor is to pass an instance of the initial state
	var machine = new SKStateMachine<SomeClass>( someClass, new PatrollingState() );
	
	// updates the state machine
	machine.update( Time.deltaTime );
	
	// change states
	machine.changeState<ChasingState>();
	