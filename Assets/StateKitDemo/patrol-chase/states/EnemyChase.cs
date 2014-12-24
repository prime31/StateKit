using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class EnemyChase : SKState<EnemyController>
{
	private float _speed = 8f;


	public override void begin()
	{
		Debug.Log( "started chasing" );
	}


	public override void reason()
	{
		// can we see the player? If not, we get out of here
		RaycastHit hit;
		var canSeePlayer = false;
		if( Physics.Raycast( _context.transform.position, _context.playerTransform.position - _context.transform.position, out hit ) )
		{
			if( hit.collider.name == "Player" )
				canSeePlayer = true;
		}

		if( !canSeePlayer )
			_machine.changeState<EnemyPatrol>();
	}


	public override void update( float deltaTime )
	{
		// run after the player!
		var directionToPlayer = _context.playerTransform.position - _context.transform.position;
		_context.transform.rotation = Quaternion.LookRotation( directionToPlayer );
		_context.transform.position += ( directionToPlayer.normalized * _speed * deltaTime );
	}

}
