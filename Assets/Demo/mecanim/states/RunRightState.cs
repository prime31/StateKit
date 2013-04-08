using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class RunRightState : SKMecanimState<MecanimPlayerController>
{
	private int _goRightParam;
	
	
	public RunRightState() : base( "Base Layer.RunRight" )
	{
		// fetch the hashes for any parameters we may need from mecanim state machine
		_goRightParam = Animator.StringToHash( "goRight" );
	}
	
	
	public override void begin()
	{
		// set the mecanim parameter so we start running right
		machine.animator.SetBool( _goRightParam, true );
		machine.animator.applyRootMotion = true;
	}
	
	
	public override void reason()
	{
		// decide if we should change state based on input
		if( !Input.GetKey( KeyCode.RightArrow ) )
			machine.changeState<RunStraightState>();
	}
	
	
	public override void update( float deltaTime, AnimatorStateInfo stateInfo )
	{
		// do interesting stuff with your player here
	}
	
	
	public override void end()
	{
		// clean up the mecanim state here
		machine.animator.SetBool( _goRightParam, false );
		machine.animator.applyRootMotion = false;
	}

}
