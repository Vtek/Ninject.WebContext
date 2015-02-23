using System;

namespace Ninject.WebContext
{
	/// <summary>
	/// Release.
	/// </summary>
	class Release : IDisposable
	{
		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		Action Action { get; set; }

		/// <summary>
		/// Initializes a new instance of the Release class.
		/// </summary>
		/// <param name="release">Release.</param>
		public Release(Action release) { Action = release; }

		/// <summary>
		/// Releases all resource used by the <see cref="Ninject.WebContext.Release"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Ninject.WebContext.Release"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Ninject.WebContext.Release"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Ninject.WebContext.Release"/> so
		/// the garbage collector can reclaim the memory that the <see cref="Ninject.WebContext.Release"/> was occupying.</remarks>
		public void Dispose()
		{
			Action();
		}
	}
}

