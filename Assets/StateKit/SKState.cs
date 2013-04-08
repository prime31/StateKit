using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class SKState<T>
{
	internal int mecanimStateHash;
	protected SKStateMachine<T> _machine;
	protected T _context;
	
	
	public SKState()
	{}
	
	
	/// <summary>
	/// constructor that takes the mecanim state name as a string
	/// </summary>
	public SKState( string mecanimStateName ) : this( Animator.StringToHash( mecanimStateName ) )
	{}
	
	
	/// <summary>
	/// constructor that takes the mecanim state hash
	/// </summary>
	public SKState( int mecanimStateHash )
	{
		
	}

	
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
