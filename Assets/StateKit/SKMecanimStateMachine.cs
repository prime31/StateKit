using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Prime31.StateKit
{
	/// <summary>
	/// Mecanim specific StateKit state machine. Note that there are some differences between a normal StateKit state machine:
	/// - the SKMecanimStateMachine will not call a states update method until Mecanim fully transitions into that state
	/// </summary>
	public sealed class SKMecanimStateMachine<T>
	{
		private T _context;
		#pragma warning disable
		public event Action onStateChanged;
		#pragma warning restore

		private SKMecanimState<T> _currentState;
		public SKMecanimState<T> currentState { get { return _currentState; } }
		public Animator animator;

		private Dictionary<System.Type, SKMecanimState<T>> _states = new Dictionary<System.Type, SKMecanimState<T>>();


		/// <summary>
		/// creates a Mecanim state machine and sets it's current state
		/// </summary>
		public SKMecanimStateMachine( Animator animator, T context, SKMecanimState<T> initialState )
		{
			this.animator = animator;
			_context = context;

			// setup our initial state
			addState( initialState );
			_currentState = initialState;
			_currentState.begin();
		}


		/// <summary>
		/// adds the state to the machine
		/// </summary>
		public void addState( SKMecanimState<T> state )
		{
			state.setMachineAndContext( this, _context );
			_states[state.GetType()] = state;
		}


		/// <summary>
		/// ticks the state machine with the provided delta time
		/// </summary>
		public void update( float deltaTime )
		{
			var currentStateInfo = animator.GetCurrentAnimatorStateInfo( 0 );

			// only call the states update method if we are in that state or if it's mecanimStateHash is 0 meaning it doesn't want us to limit the calls
			if( _currentState.mecanimStateHash == 0 || currentStateInfo.fullPathHash == _currentState.mecanimStateHash )
			{
				var tempState = _currentState;
				_currentState.reason();

				// we might have changed state in reason so we make sure we are still on the same state here
				if( tempState == _currentState )
					_currentState.update( deltaTime, currentStateInfo );
			}
		}


		/// <summary>
		/// changes the current state
		/// </summary>
		public R changeState<R>() where R : SKMecanimState<T>
		{
			// avoid changing to the same state
			var newType = typeof( R );
			if( _currentState.GetType() == newType )
				return _currentState as R;

#if UNITY_EDITOR
			// do a sanity check while in the editor to ensure we have the given state in our state list
			if( !_states.ContainsKey( newType ) )
			{
				var error = GetType() + ": state " + newType + " does not exist. Did you forget to add it by calling addState?";
				Debug.LogError( error );
				throw new Exception( error );
			}
#endif

			// end the previous state
			_currentState.end();

			// swap states and call begin
			_currentState = _states[newType];
			_currentState.begin();

			// fire the changed event if we have a listener
			if( onStateChanged != null )
				onStateChanged();

			return _currentState as R;
		}

	}
}