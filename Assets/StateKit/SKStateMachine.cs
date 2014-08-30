using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Prime31.StateKit
{
	public sealed class SKStateMachine<T>
	{
		protected T _context;
		#pragma warning disable
		public event Action onStateChanged;
		#pragma warning restore

		public SKState<T> currentState { get { return _currentState; } }
		public SKState<T> previousState;
		public float elapsedTimeInState = 0f;


		private Dictionary<System.Type, SKState<T>> _states = new Dictionary<System.Type, SKState<T>>();
		private SKState<T> _currentState;


		public SKStateMachine( T context, SKState<T> initialState )
		{
			this._context = context;

			// setup our initial state
			addState( initialState );
			_currentState = initialState;
			_currentState.begin();
		}


		/// <summary>
		/// adds the state to the machine
		/// </summary>
		public void addState( SKState<T> state )
		{
			state.setMachineAndContext( this, _context );
			_states[state.GetType()] = state;
		}


		/// <summary>
		/// ticks the state machine with the provided delta time
		/// </summary>
		public void update( float deltaTime )
		{
			elapsedTimeInState += deltaTime;
			_currentState.reason();
			_currentState.update( deltaTime );
		}


		/// <summary>
		/// changes the current state
		/// </summary>
		public R changeState<R>() where R : SKState<T>
		{
			// avoid changing to the same state
			var newType = typeof( R );
			if( _currentState.GetType() == newType )
				return _currentState as R;

			// only call end if we have a currentState
			if( _currentState != null )
				_currentState.end();

			#if UNITY_EDITOR
			// do a sanity check while in the editor to ensure we have the given state in our state list
			if( !_states.ContainsKey( newType ) )
			{
				var error = GetType() + ": state " + newType + " does not exist. Did you forget to add it by calling addState?";
				Debug.LogError( error );
				throw new Exception( error );
			}
			#endif

			// swap states and call begin
			previousState = _currentState;
			_currentState = _states[newType];
			_currentState.begin();
			elapsedTimeInState = 0f;

			// fire the changed event if we have a listener
			if( onStateChanged != null )
				onStateChanged();

			return _currentState as R;
		}

	}
}