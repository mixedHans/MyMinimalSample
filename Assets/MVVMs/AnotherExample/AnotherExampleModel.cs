using UnityEngine;

public class AnotherExampleModel : IModel
{
    public AnotherExampleModel()
    {
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void Dispose()
    {
        Debug.Log($"[{GetType()}] Disposed ExampleModel");
    }
}