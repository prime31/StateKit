using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class SKState<T>
{
	protected SKStateMachine<T> _machine;
	
	
	public SKState( SKStateMachine<T> machine )
	{
		_machine = machine;
	}
	
	
	public abstract void begin();
	
	public abstract void update( float deltaTime );
	
	public abstract void end();

}
