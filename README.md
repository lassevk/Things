# Things

This class library adds a simple class, ```Name``` with a single method with overloads, ```Name.Of``` which
can be used to obtain the name of types, methods, properties, events, constructors.

The benefit of using this vs. the built-in ```Type.FullName``` property of ```Type``` is that the ```Name.Of```
methods understand generics, nested types, types that have equivalent C# alias keywords, nullable operator, etc.

Example:

    string name = typeof(Dictionary<int?, string>).FullName;
	// System.Collections.Generic.Dictionary`2[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]

	string name = Name.Of(typeof(Dictionary<int, string>));
	// System.Collections.Generic.Dictionary<int?, string>

	string name = Name.Of(typeof(Dictionary<int?, string>), TypeNameOptions.UseCSharpKeywords | TypeNameOptions.UseNullableOperator);
	// returns Dictionary<int?, string>