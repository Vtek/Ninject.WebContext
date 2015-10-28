Ninject.WebContext
=======

Smart IoC library for your WebApp.


## Installation

**/!\ Take care**
Version 1.0 is now obsolete. You need to use the alpha version 1.1 to use all the improvement from the Ninject.CoreContext.

```
PM> Install-Package Ninject.WebContext -Pre
```


## Getting started

This library is a complet implementation of IoC with Ninject for Asp.Net MVC & WebApi (Version 5.0.0). It ease you the implementation in your web project.
First you need to create an automatic injection module to define your bindings :

```csharp
public class MyModule : AutoInjectionModule
{
	public override void Load()
	{
		Bind<IMyInterface>().To<IMyImplementation>();
	}
}
```

In Ninject, bindings are in transient scope by default. The library allow you to set the Http request as scope :

```csharp
Bind<IMyInterface>().To<IMyImplementation>().InHttpScope();
```

When your modules are ready, you have done 95% of the work ! Start the NinjectContext in your Global.asax application start method with one line :

```csharp
NinjectContext
	.Get()
	.AddModule<MyModule>()
	.UseMvc()
	.UseWebApi()
	.WithAutoInjection()
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


## About AutoInjection

AutoInjection is an out of the box feature in the library but you must activate it with WithAutoInjection method. If you want to use a classic IoC implementation, don't call this method and define constructor with dependecies in parameter :

```csharp
public class MyController : Controller
{
	IMyInterace MyInterface { get; set; }
	
	public MyController(IMyInterace myInterface)
	{
		MyInterface = myInterface;
	}
	
	public ActionResult Index()
	{
		var myValue = MyInterface.GetValue();
		return View(myValue);
	}
}
```

If you do that, you can use standard NinjectModule to define your Bind but without the use of AutoInjection, Filter can't be inject (You can inject dependencies into a constructor of an attribute).


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