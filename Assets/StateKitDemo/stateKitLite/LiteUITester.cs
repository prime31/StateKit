using UnityEngine;
using System.Collections;
using Prime31.StateKitLite;



public enum LiteDemoStates
{
	Patrolling,
	Chasing
}


public class LiteUITester : StateKitLite<LiteDemoStates>
{
	void Start()
	{
		initialState = LiteDemoStates.Patrolling;
	}


	void OnGUI()
	{
		if( GUILayout.Button( "Patrolling State" ) )
			currentState = LiteDemoStates.Patrolling;

		if( GUILayout.Button( "Chasing State" ) )
			currentState = LiteDemoStates.Chasing;
	}


	#region Optional State Methods

	void Patrolling_Enter()
	{
		Debug.Log( "Patrolling_Enter" );
	}


	void Patrolling_Tick()
	{
		// too many logs
		//Debug.Log( "Patrolling_Tick" );
	}


	void Patrolling_Exit()
	{
		Debug.Log( "Patrolling_Exit" );
	}


	void Chasing_Enter()
	{
		Debug.LogWarning( "Chasing_Enter" );
	}


	void Chasing_Tick()
	{
		// too many logs
		//Debug.LogWarning( "Chasing_Tick" );
	}


	void Chasing_Exit()
	{
		Debug.LogWarning( "Chasing_Exit" );
	}

	#endregion
}
