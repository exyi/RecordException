# C# `record`s inheriting from `Exception`

Install [from NuGet](https://www.nuget.org/packages/RecordException): `dotnet add package RecordException`

Then define your exceptions like this:

```csharp
using RecordExceptions;

public record MissingIdException(int id): RecordException;

```

The exception message will automatically be `MissingIdException: id = {id}`.
Probably fine for debugging, but if you want to customize that, you can:

```csharp
public record MissingIdException(int id): RecordException
{
    public override string Message => $"Id {id} is missing";
}
```

The message may also be specified in the construtor:

```csharp
public record OperationCancelledException: RecordException("User has cancelled the operation.")
```

You can also wrap exception into InnerException, it might be useful for adding relevant information:

```csharp
public record SomethingBad(Exception InnerException): RecordException("Something really bad has happened", InnerException);
```

Notes:

* If you are using some parameters in your message, prefer to override the Message property if you want the `x with { Prop = newValue }` syntax to work.
* Even when you provide your own message, all properties will be shown bellow the stacktrace. If you don't like this, `public override string ToString() => base.ToString();`.
* Don't be too surprised if this stops working with the next C# version 😅. Yes, records are not supposed to inherit from System.Exception. But they forgot to check base type of a base type ;)



License is MIT, so you can use it however you want to. If you need a record inheriting from another class, feel free to fork this repo.
