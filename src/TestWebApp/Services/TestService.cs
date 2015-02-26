using System;

namespace TestWebApp
{
	public class TestService : ITestService
	{
		public string Hello()
		{
			return "Hello World !";
		}
	}
}

