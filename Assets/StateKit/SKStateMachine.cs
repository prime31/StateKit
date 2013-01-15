using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class SKStateMachine<T>
{
	public T context;
	#pragma warning disable
	public event Action onStateChanged;
	#pragma warning restore
	
	private Dictionary<System.Type, SKState<T>> _states = new Dictionary<System.Type, SKState<T>>();
	private SKState<T> _currentState;
	public SKState<T> currentState { get { return _currentState; } }
	private SKState<T> _previousState;
	public SKState<T> previousState { get { return _previousState; } }

	
	public SKStateMachine( T context, SKState<T> initialState )
	{
		this.context = context;
		
		// setup our initial state
		initialState.setMachine( this );
		_states[initialState.GetType()] = initialState;
		_currentState = initialState;
		_currentState.begin();
	}
	
	
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
		state.setMachine( this );
		_states.Add( type, state );
		
		return state;
	}

	
	public void update( float deltaTime )
	{
		_currentState.reason();
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
		
		// fire the changed event if we have a listener
		if( onStateChanged != null )
			onStateChanged();
	}
	
	
	public void popState()
	{
		_currentState.end();
		_currentState = _previousState;
		_currentState.begin();
	}

}
