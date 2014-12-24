using UnityEngine;
using System.Collections;
using Prime31.StateKit;


/// <summary>
/// super simple demo of wiring up Mecanim and StateKit to work together. In this demo, the Mecanim state machine was setup with bool params
/// to switch from running straight to right/left. The RunLeft/RunRightStates manage setting/unsetting the bools to keep Mecanim in check
/// </summary>
public class MecanimPlayerController : MonoBehaviour
{
	private SKMecanimStateMachine<MecanimPlayerController> _stateMachine;

	
	void Start()
	{
		var animator = GetComponent<Animator>();
		
		// create the state machine
		_stateMachine = new SKMecanimStateMachine<MecanimPlayerController>( animator, this, new RunStraightState() );
		
		// add all of our states so that we can switch to them at will
		_stateMachine.addState( new RunLeftState() );
		_stateMachine.addState( new RunRightState() );
		
		// listen for state changes and log them
		_stateMachine.onStateChanged += () =>
		{
			Debug.Log( "state changed: " + _stateMachine.currentState );
		};
	}
	
	
	void Update()
	{
		_stateMachine.update( Time.deltaTime );
	}
	
	
	void OnGUI()
	{
		GUI.skin.label.fontSize = 20;
		GUILayout.Label( "Mecanim Example. It's about 10 lines of code so open the MecanimPlayerController and read it." );
		GUILayout.Space( 20 );
		GUILayout.Label( "current state: " + _stateMachine.currentState );
	}

}
