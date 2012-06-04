using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class SKState<T>
{
	protected SKStateMachine<T> _machine;
	
	
	public void setMachine( SKStateMachine<T> machine )
	{
		_machine = machine;
	}


	public abstract void begin();

	public virtual void reason()
	{}
	
	public abstract void update( float deltaTime );
	
	public abstract void end();

}
