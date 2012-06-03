using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PatrollingState : SKState<SomeClass>
{
	// all states must implement a constructor that takes in the state machine that will be running the show
	public PatrollingState( SKStateMachine<SomeClass> machine ) : base( machine )
	{}
	
	
	public override void begin()
	{
		Debug.Log( "started patrolling" );
	}
	
	
	public override void update( float deltaTime )
	{
		// do the actual patrolling and once something interesting is near change to chasing state
	}
	

	public override void end()
	{
		Debug.Log( "done patrolling" );
	}

}
