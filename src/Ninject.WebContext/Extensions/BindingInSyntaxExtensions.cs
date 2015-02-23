using Ninject.WebContext;
using Ninject.Syntax;
using System.Web;

namespace Ninject.Modules
{
	/// <summary>
	/// Binding in syntax extensions.
	/// </summary>
	public static class BindingInSyntaxExtensions
	{
		/// <summary>
		/// Define the auto injection on type T.
		/// </summary>
		/// <returns>The auto injection.</returns>
		/// <param name="syntax">Syntax.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IBindingWhenInNamedWithOrOnSyntax<T> WithAutoInjection<T>(this IBindingWhenInNamedWithOrOnSyntax<T> syntax)
		{
			AutoInjection.AddTypeToInject(typeof(T));
			return syntax;
		}

		/// <summary>
		/// Set the scope of binding in HttpContext.
		/// </summary>
		/// <returns>The http scope.</returns>
		/// <param name="syntax">Syntax.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IBindingNamedWithOrOnSyntax<T> InHttpScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> syntax)
		{
			return syntax.InScope(x => HttpContext.Current);
		}
	}
}

