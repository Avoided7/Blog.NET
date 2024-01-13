namespace Blog.Domain.Primitives;

public class Result
{
  public Error Error { get; }
  public bool IsSucceeded { get; }

  protected Result(Error error, bool isSucceeded)
  {
    ArgumentNullException.ThrowIfNull(error);

    if (error != Error.Empty && isSucceeded)
    {
      throw new ArgumentException("Result cannot be succeeded with error.");
    }

    if (error == Error.Empty && !isSucceeded)
    {
      throw new ArgumentException("Result cannot be failure without error.");
    }

    Error = error;
    IsSucceeded = isSucceeded;
  }

  public static Result Succeeded()
  {
    return new Result(Error.Empty, true);
  }

  public static Result<T> Succeeded<T>(T data)
  {
    return new Result<T>(data, Error.Empty, true);
  }

  public static Result Failure(Error error)
  {
    return new Result(error, false);
  }

  public static Result<T> Failure<T>(Error error)
  {
    return new Result<T>(error, false);
  }
}

public class Result<T> : Result
{
  private readonly T _data = default!;
  public T Data => IsSucceeded ? _data : throw new InvalidOperationException("Cannot access data in failure result.");

  internal Result(T data, Error error, bool isSucceeded) : base(error, isSucceeded)
  {
    ArgumentNullException.ThrowIfNull(data);

    _data = data;
  }

  internal Result(Error error, bool isSucceeded) : base(error, isSucceeded)
  { }
}