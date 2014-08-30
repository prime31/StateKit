using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Prime31.StateKit
{
	public abstract class SKMecanimState<T>
	{
		internal int mecanimStateHash;
		protected SKMecanimStateMachine<T> _machine;
		protected T _context;


		public SKMecanimState()
		{}


		/// <summary>
		/// constructor that takes the mecanim state name as a string. Note that if a mecanimStateName is passed into the constructor
		/// the reason and update methods will not be called until Mecanim finishes any transitions and is completely in the mecanim state.
		/// Do not pass in a mecanim state name if you do not want that behaviour.
		/// </summary>
		public SKMecanimState( string mecanimStateName ) : this( Animator.StringToHash( mecanimStateName ) )
		{}


		/// <summary>
		/// constructor that takes the mecanim state hash
		/// </summary>
		public SKMecanimState( int mecanimStateHash )
		{
			this.mecanimStateHash = mecanimStateHash;
		}


		internal void setMachineAndContext( SKMecanimStateMachine<T> machine, T context )
		{
			_machine = machine;
			_context = context;
			onInitialized();
		}


		/// <summary>
		/// called directly after the machine and context are set allowing the state to do any required setup
		/// </summary>
		public virtual void onInitialized()
		{}


		public virtual void begin()
		{}


		public virtual void reason()
		{}


		public abstract void update( float deltaTime, AnimatorStateInfo stateInfo );


		public virtual void end()
		{}

	}
}