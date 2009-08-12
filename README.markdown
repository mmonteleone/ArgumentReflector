ArgumentReflector
=================
http://github.com/mmonteleone/ArgumentReflector  

Spoonful of syntactic sugar for processing anonymous object arguments in C# 3.0 to simulate named, optional, method parameters.

What?
-----

You may have seen such C# (ab)usage rampant throughout [ASP.NET MVC][4]:

    routes.MapRoute(
        "Default",
        "{controller}/{action}/{id}",
        new { controller = "Home", action = "Index", id = "" }
    );

    <%= Html.RouteLink("Search Results Page 2", "Search", new { query = ViewData["searchQuery"], page = 2 })%>

ArgumentReflector is designed to live on the other end of these kinds of calls, making it easy to process anonymous object arguments.  Almost *too* easy.

Contrived example, wherein ArgumentReflector is admittedly not the ideal tool for the job:

    // method calls with named, optional, parameters via anonymous object
    SetUpUser(new { isCitizen = false, email = "email@email.com" });
    SetUpUser(new { LastName = "smith" });
    SetUpUser(new { firstName = "John", email = "john@john.com", iscitizen = true });

    // method accepting anonymous object
    public void SetUpUser(object options)
    {
        // set up some default option values

        string firstName = "some first name";
        string lastName = "some last name";
        string email = "some email address";
        bool isCitizen = false;

        // use ArgumentReflector's extension method
        // to process properties set on the object

        options.Reflect(new PropertyHandlerDictionary {
            { "firstname", v => firstName = (string)v },
            { "lastname", v => lastName = (string)v },
            { "email", v => email = (string)v },
            { "iscitizen", v => isCitizen = (bool)v }
        });

        // do something with the options
    }

There is also an optional strongly-typed generic api:

    options.Reflect(prop =>
        prop.Named<string>("firstname", v => firstName = v);
        prop.Named<string>("lastname", v => lastName = v);
        prop.Named<string>("email", v => email = v);
        prop.Named<bool>("iscitizen", v => isCitizen = v);
    });

So why would I (not) want this?
-------------------------------

To provide minimal-friction named, optional, arguments in C# 3.0.  That's the only reason, really.   

**Where could this ever be useful?**  
A particularly useful problem space is unit test helper utilities, such as methods to support mocking a complex environment setup before a test.  Some tests might be interested in setting many options, some only one or two.  Accepting 15 string or boolean arguments in a row will surely decrease your test's readability just as coding 15 combinations of overloads will decrease your productivity and increase potential for bugs.  If you have ever had to mock ASP.NET MVC's HttpContextBase, you might agree.

**But isn't C# 4 slated to support named parameters?**  
Yes.    

**And isn't this kind of a kludge?**  
Yes, but ASP.NET MVC started it!

**Not to mention *really* slow?**  
Yes and no.  

If you care about high performance, this might not be for you since it uses reflection invocation.  

That being said, this is the same core technique as ASP.NET MVC's *widely*-used built-in class RouteValueDictionary, employed by nearly every method that accepts anonymous object/named-parameter arguments in the framework.  

And there are scenarios where you might *not* care, such as unit test utilities.

Installation
------------

Since this is such a small piece of code, it is recommended to simply copy the source, ArgumentReflector.cs, directly into your project.  It does not warrant being a referenced, compiled, assembly.  

1. Download the source.
2. cd into the project's directory `> build release`
3. Copy build\Release\ArgumentReflector.cs into your own project.
   *  Alternatively, the assembly ArgumentReflector.dll can be added to your project as well.
4. Either modify ArgumentReflector to share your project's namespace, or state `using ArgumentReflctor` to include the extension methods.

To run ArgumentReflector's xUnit tests, use
    > build test

Usage
-----

Usage is quite simple, with a few syntax options:

1.  Dictionary-based
2.  Strongly-typed generic lambdas
3.  Weakly-typed lambdas

**Examples**

    // anonymous argument to a method call which handles it with ArgumentReflector
    options = new { propertyA = "some passed value", propertyNameB = 5 };

    // pre-set default options within the method call
    string optionA = "default";
    int optionB = 100;

    // Dictionary-based
    options.Reflect(new PropertyHandlerDictionary {
        { "propertyNameA", x => optionA = (string)x },
        { "propertyNameB", x => optionB = (int)x },
    });

    // Strongly-typed generic lambdas
    options.Reflect(prop => {
        prop.Named<string>("optionA", x => optionA = x);
        prop.Named<int>("optionB", x => optionB = x);
    });

    // Weakly-typed lambdas
    options.Reflect(prop => {
        prop.Named("optionA", x => optionA = (string)x);
        prop.Named("optionB", x => optionB = (int)x);
    });

By default, ArgumentReflector is case-insensitive with regard to property names, so `Method(new { option = "a" })` would be processed identically to `Method(new { Option = "a" })`.  This behavior can be overriden in each of the above 3 syntax options by setting True in `Reflect`'s second overload.

    options.Reflect(true, new PropertyHandlerDictionary {
        { "CaseSensitivePropertyA", x => optionA = (string)x },
        { "caseSENSITIVEpropertyB", x => optionB = (int)x },
    });

Also, if you have a strong moral objection to the extension methods, you can use the underlying classes, although this erodes the beauty of the low-friction api.

    var reflector = new ArgumentReflector(options);
    var propertyHandlers = new PropertyHandlerDictionary();
    propertyHandlers.AddTypedHandler<string>("optionA", x => optionA = x);
    propertyHandlers.AddTypedHandler<int>("optionB", x => optionB = x);
    reflector.Reflect(propertyHandlers);

Credit
------

Copyright 2009 [Michael Monteleone][3]

API influenced by [Jonathan Pryor][1]'s excellent callback-based program option parser, [NDesk.Options][2]

License
-------

The MIT License

Copyright (c) 2009 Michael Monteleone

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

[1]: http://www.jprl.com/ "Jonathan Pryor"
[2]: http://www.ndesk.org/Options "NDesk.Options"
[3]: http://michaelmonteleone.net "Michael Monteleone"
[4]: http://www.asp.net/mvc/ "ASP.NET MVC"
