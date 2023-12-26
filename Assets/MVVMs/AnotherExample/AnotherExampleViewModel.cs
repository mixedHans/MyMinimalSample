using Unity.Properties;
using UnityEngine;

public class AnotherExampleViewModel : IViewModel
{
    private readonly IModel m_exampleModel;

    [CreateProperty]
    public string Title { get; set; } = "AnotherExampleView";

    public AnotherExampleViewModel(IModel exampleModel) // ApplicationStateApplier
    {
        m_exampleModel = exampleModel;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void Dispose()
    {
        Debug.Log($"[{GetType()}] Disposed ExampleViewModel");
    }
}