using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class RunLeftState : SKMecanimState<MecanimPlayerController>
{
	private int _goLeftParam;

	
	public RunLeftState() : base( "Base Layer.RunLeft" )
	{
		// fetch the hashes for any parameters we may need from mecanim state machine
		_goLeftParam = Animator.StringToHash( "goLeft" );
	}
	
	
	public override void begin()
	{
		// set the mecanim parameter so we start running right
		_machine.animator.SetBool( _goLeftParam, true );
		_machine.animator.applyRootMotion = true;
	}
	
	
	public override void reason()
	{
		// decide if we should change state based on input
		if( !Input.GetKey( KeyCode.LeftArrow ) )
			_machine.changeState<RunStraightState>();
	}
	
	
	public override void update( float deltaTime, AnimatorStateInfo stateInfo )
	{
		// do interesting stuff with your player here
	}
	
	
	public override void end()
	{
		// clean up the mecanim state here
		_machine.animator.SetBool( _goLeftParam, false );
		_machine.animator.applyRootMotion = false;
	}

}
