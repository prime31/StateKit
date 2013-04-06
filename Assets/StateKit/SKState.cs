using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class SKState<T>
{
	protected SKStateMachine<T> _machine;
	protected T _context;
	
	
	public void setMachineAndContext( SKStateMachine<T> machine, T context )
	{
		_machine = machine;
		_context = context;
	}


	public virtual void begin()
	{}
	
	
	public virtual void reason()
	{}
	
	
	public abstract void update( float deltaTime );
	
	
	public virtual void end()
	{}

}
