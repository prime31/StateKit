using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChasingState : SKState<SomeClass>
{
	// all states must implement a constructor that takes in the state machine that will be running the show
	public ChasingState( SKStateMachine<SomeClass> machine ) : base( machine )
	{}

	
	public override void begin()
	{
		Debug.Log( "started chasing" );
	}
	
	
	public override void update( float deltaTime )
	{
		// do the actual chasing and once complete either pop to the previous state or set it directly
	}

	
	public override void end()
	{
		Debug.Log( "done chasing" );
	}

}
