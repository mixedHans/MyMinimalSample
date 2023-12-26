using UnityEngine;

public class ExampleModel : IModel
{
    public ExampleModel()
    {
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void Dispose()
    {
        Debug.Log($"[{GetType()}] Disposed ExampleModel");
    }
}
