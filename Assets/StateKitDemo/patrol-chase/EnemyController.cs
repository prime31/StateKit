using UnityEngine;
using System.Collections.Generic;
using Prime31.StateKit;


public class EnemyController : MonoBehaviour
{
	public Transform playerTransform;
	public List<Vector3> waypoints = new List<Vector3>();
	private SKStateMachine<EnemyController> _machine;


	void Start()
	{
		// fetch our waypoint positions so we have a purpose in life
		var waypointRoot = GameObject.Find( "Waypoints" );
		var rawWaypoints = waypointRoot.GetComponentsInChildren<Transform>();
		foreach( var t in rawWaypoints )
		{
			// filter out the root objects position
			if( !t.Equals( waypointRoot.transform ) )
				waypoints.Add( t.position );
		}

		// the initial state has to be passed to the constructor
		_machine = new SKStateMachine<EnemyController>( this, new EnemyPatrol() );

		// we can now add any additional states
		_machine.addState( new EnemyChase() );
	}
	
	
	void Update()
	{
		// update the state machine
		_machine.update( Time.deltaTime );
	}

}
