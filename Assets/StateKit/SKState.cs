using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace Prime31.StateKit
{
	public abstract class SKState<T>
	{
		protected int _mecanimStateHash;
		protected SKStateMachine<T> _machine;
		protected T _context;
		
		
		public SKState()
		{}


		/// <summary>
		/// constructor that takes the mecanim state name as a string
		/// </SKMecanimState>
		public SKState( string mecanimStateName ) : this( Animator.StringToHash( mecanimStateName ) )
		{}
		
		
		/// <summary>
		/// constructor that takes the mecanim state hash
		/// </summary>
		public SKState( int mecanimStateHash )
		{
			_mecanimStateHash = mecanimStateHash;
		}

		
		internal void setMachineAndContext( SKStateMachine<T> machine, T context )
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
}