namespace Core.Exceptions;

public class FruitNotFoundException : Exception
{
    public FruitNotFoundException(string name)
        : base($"Fruit with name '{name}' was not found.")
    {
    }
}
