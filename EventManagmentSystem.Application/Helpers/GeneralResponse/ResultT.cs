using System.Text.Json.Serialization;

namespace EventManagmentSystem.Application.Helpers;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    [JsonConstructor]

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => IsSuccess
        ? _value! : default;
    //: throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}
