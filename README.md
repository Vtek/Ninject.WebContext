Ninject.WebContext
=======

Smart IoC library for your WebApp.


## Installation

```
PM> Install-Package Ninject.WebContext
```


## Features

	* Simple, straightforward and fluent
	* Start all IoC features in one line of code
	* Automatic injection on properties
	* Controller activator & factory On-Demand
	* Http scoping


## Getting started

This library is a complet implementation of IoC with Ninject. It ease you the implementation in your web project.
First you need to create an automatic injection module to define your Binding :

```csharp
public class MyModule : AutoInjectionModule
{
	public override void Load()
	{
		Bind<IMyInterface>().To<IMyImplementation>();
	}
}
```

 In Ninject, Bindings are in transient scope by default. The library allow you to set the scope in the Http request scope :

 ```csharp
 Bind<IMyInterface>().To<IMyImplementation>().InHttpScope();
 ```

 When your module is ready, you have done 95% of the code ! Start the NinjectContext in your Global.asax application start method with one line :

 ```csharp
NinjectContext
	.Instance
	.AddModule<MyModule>()
	.UseMvc()
	.UseWebApi()
	.Initialize();
 ```

 And now in your controller, see the magic :

  ```csharp
public class MyController : Controller
{
	IMyInterace MyInterface { get; set; }

	public ActionResult Index()
	{
		var myValue = MyInterface.GetValue();
		return View(myValue);
	}
}
```


## Mono

Currently, the library does not work with Mono due to an injection problem in the Ninject source code. A specific library for Mono is coming !


## Licence

The MIT License (MIT)

Copyright (c) 2015 Sylvain PONTOREAU (pontoreau.sylvain@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.