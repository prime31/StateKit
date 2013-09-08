using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace Prime31.StateKit
{
	public abstract class SKState<T>
	{
		protected SKStateMachine<T> _machine;
		protected T _context;
		
		
		public SKState()
		{}

		
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