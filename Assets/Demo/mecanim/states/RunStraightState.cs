using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class RunStraightState : SKMecanimState<MecanimPlayerController>
{
	public RunStraightState() : base( "Base Layer.Run" )
	{}
	
	
	public override void reason()
	{
		// simple reasoning for demonstrations purposes. pressing right swaps to the RunRightState, pressing left goes to the RunLeftState
		if( Input.GetKey( KeyCode.RightArrow ) )
			_machine.changeState<RunRightState>();
		else if( Input.GetKey( KeyCode.LeftArrow ) )
			_machine.changeState<RunLeftState>();
	}

	
	public override void update( float deltaTime, AnimatorStateInfo stateInfo )
	{
		// do interesting stuff with your player here
	}

}
