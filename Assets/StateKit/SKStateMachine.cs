using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class SKStateMachine<T>
{
	public T context;
	
	private SKState<T> _currentState;
	private SKState<T> _previousState;
	private Dictionary<System.Type, SKState<T>> _states = new Dictionary<System.Type, SKState<T>>();
	
	
	public SKStateMachine( T context, System.Type initialState )
	{
		this.context = context;
		
		// setup our initial state
		_currentState = getOrCreate( initialState );
		_currentState.begin();
	}
	
	
	private SKState<T> getOrCreate( System.Type type )
	{
		if( _states.ContainsKey( type ) )
			return _states[type];
		
		var state = Activator.CreateInstance( type ) as SKState<T>;
		_states.Add( type, state );
		
		return state;
	}

	
	public void update( float deltaTime )
	{
		_currentState.update( deltaTime );
	}
	
	
	public void changeState<R>() where R : SKState<T>
	{
		// avoid changing to the same state
		var newType = typeof( R );
		if( _currentState.GetType() == newType )
			return;
		
		// only call end if we have a currentState
		if( _currentState != null )
			_currentState.end();
		
		// swap states and call begin
		_previousState = _currentState;
		_currentState = getOrCreate( newType );
		_currentState.begin();
	}
	
	
	public void popState()
	{
		_currentState.end();
		_currentState = _previousState;
		_currentState.begin();
	}

}
