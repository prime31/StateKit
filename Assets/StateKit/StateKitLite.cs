// loosely based on a Unity Gems article

using UnityEngine;
using System;
using System.Collections.Generic;



namespace Prime31.StateKitLite
{
	/// <summary>
	/// Simple state machine with an enum constraint. There are some rules you must follow when using this:
	/// - if you implement Awake in your subclass you must call base.Awake()
	/// - in either Awake or Start the initialState must be set
	/// - if you implement Update in your subclass you must call base.Update()
	/// </summary>
	public class StateKitLite<TEnum> : MonoBehaviour where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		private class StateMethodCache
		{
			public Action enterState;
			public Action tick;
			public Action exitState;
		}

		StateMethodCache _stateMethods;
		protected float elapsedTimeInState = 0f;
		protected TEnum previousState;
		Dictionary<TEnum,StateMethodCache> _stateCache = new Dictionary<TEnum,StateMethodCache>();

		TEnum _currentState;
		protected TEnum currentState
		{
			get
			{
				return _currentState;
			}
			set
			{
				if( _currentState.Equals( value ) )
					return;

				// swap previous/current
				previousState = _currentState;
				_currentState = value;

				// exit the state, fetch the next cached state methods then enter that state
				if( _stateMethods.exitState != null )
					_stateMethods.exitState();

				elapsedTimeInState = 0f;
				_stateMethods = _stateCache[_currentState];

				if( _stateMethods.enterState != null )
					_stateMethods.enterState();
			}
		}

		protected TEnum initialState
		{
			set
			{
				_currentState = value;
				_stateMethods = _stateCache[_currentState];

				if( _stateMethods.enterState != null )
					_stateMethods.enterState();
			}
		}


		protected virtual void Awake()
		{
			if( !typeof( TEnum ).IsEnum )
			{
				Debug.LogError( "[StateKitLite] TEnum generic contsraint failed! You must use an enum when declaring your subclass!" );
				enabled = false;
			}

			// cache all of our state methods
			var enumValues = (TEnum[])Enum.GetValues( typeof( TEnum ) );
			foreach( var e in enumValues )
				configureAndCacheState( e );
		}


		protected virtual void Update()
		{
			elapsedTimeInState += Time.deltaTime;

			if( _stateMethods.tick != null )
				_stateMethods.tick();
		}


		void configureAndCacheState( TEnum stateEnum )
		{
			var stateName = stateEnum.ToString();

			var state = new StateMethodCache();
			state.enterState = getDelegateForMethod( stateName + "_Enter" );
			state.tick = getDelegateForMethod( stateName + "_Tick" );
			state.exitState = getDelegateForMethod( stateName + "_Exit" );

			_stateCache[stateEnum] = state;
		}


		Action getDelegateForMethod( string methodName )
		{
			var methodInfo = GetType().GetMethod( methodName,
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic );

			if( methodInfo != null )
				return Delegate.CreateDelegate( typeof( Action ), this, methodInfo ) as Action;

			return null;
		}
	}
}