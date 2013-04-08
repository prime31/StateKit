using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class SKStateMachine<T>
{
	protected T _context;
	#pragma warning disable
	public event Action onStateChanged;
	#pragma warning restore
	
	public SKState<T> currentState { get { return _currentState; } }
	
	
	private Dictionary<System.Type, SKState<T>> _states = new Dictionary<System.Type, SKState<T>>();
	private SKState<T> _currentState;
	private Animator _animator;
	
	
	public SKStateMachine( T context, SKState<T> initialState )
	{
		this._context = context;
		
		// setup our initial state
		initialState.setMachineAndContext( this, context );
		_states[initialState.GetType()] = initialState;
		_currentState = initialState;
		_currentState.begin();
	}
	

	private SKState<T> getOrCreate( System.Type type )
	{
		if( _states.ContainsKey( type ) )
			return _states[type];
		
		var state = Activator.CreateInstance( type ) as SKState<T>;
		state.setMachineAndContext( this, _context );
		_states.Add( type, state );
		
		return state;
	}
	
	
	/// <summary>
	/// adds the state to the machine
	/// </summary>
	public void addState( SKState<T> state )
	{
		_states[state.GetType()] = state;
	}

	
	public void update( float deltaTime )
	{
		_currentState.reason();
		_currentState.update( deltaTime );
	}
	
	
	public virtual void changeState<R>() where R : SKState<T>
	{
		// avoid changing to the same state
		var newType = typeof( R );
		if( _currentState.GetType() == newType )
			return;
		
		// only call end if we have a currentState
		if( _currentState != null )
			_currentState.end();
		
		// swap states and call begin
		_currentState = getOrCreate( newType );
		_currentState.begin();
		
		// fire the changed event if we have a listener
		if( onStateChanged != null )
			onStateChanged();
	}

}
