using Ninject.Modules;

namespace Ninject.WebContext
{
	/// <summary>
	/// Auto injection module.
	/// </summary>
	public abstract class AutoInjectionModule : NinjectModule
	{
		/// <summary>
		/// Registers the specified binding.
		/// </summary>
		/// <param name="binding">The binding to add.</param>
		public override void AddBinding (Ninject.Planning.Bindings.IBinding binding)
		{
			AutoInjection.AddTypeToInject (binding.Service);
			base.AddBinding (binding);
		}
	}
}

