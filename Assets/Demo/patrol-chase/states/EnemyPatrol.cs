using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class EnemyPatrol : SKState<EnemyController>
{
	private int _currentWaypoint = 0;
	private float _closeEnoughToWaypoint = 1f;
	private float _speed = 5f;


	public override void begin()
	{
		Debug.Log( "started patrolling" );
	}


	public override void reason()
	{
		// can we see the player? if so, we gotta chase after him!
		RaycastHit hit;
		if( Physics.Raycast( _context.transform.position, _context.playerTransform.position - _context.transform.position, out hit ) )
		{
			if( hit.collider.name == "Player" )
				_machine.changeState<EnemyChase>();
		}
	}


	public override void update( float deltaTime )
	{
		// really simple waypoint navigation. Just finds a waypoint to run to
		if( Vector3.Distance( _context.transform.position, _context.waypoints[_currentWaypoint] ) < _closeEnoughToWaypoint )
		{
			_currentWaypoint++;
			if( _currentWaypoint >= _context.waypoints.Count )
				_currentWaypoint = 0;
		}


		var directionToWaypoint = _context.waypoints[_currentWaypoint] - _context.transform.position;
		_context.transform.rotation = Quaternion.LookRotation( directionToWaypoint );
		_context.transform.position += ( directionToWaypoint.normalized * _speed * deltaTime );
	}
}
