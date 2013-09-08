using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StandardUITester : MonoBehaviour
{
	private SKStateMachine<SomeClass> _machine;
	
	
	void Start()
	{
		// our context can be any type at all
		var someClass = new SomeClass();
		
		// the initial state has to be passed to the constructor
		_machine = new SKStateMachine<SomeClass>( someClass, new PatrollingState() );
		_machine.addState( new ChasingState() );
	}
	
	
	void Update()
	{
		_machine.update( Time.deltaTime );
	}
	
	
	void OnGUI()
	{
		if( GUILayout.Button( "Patrolling State" ) )
			_machine.changeState<PatrollingState>();
		
		if( GUILayout.Button( "Chasing State" ) )
			_machine.changeState<ChasingState>();
	}
}
