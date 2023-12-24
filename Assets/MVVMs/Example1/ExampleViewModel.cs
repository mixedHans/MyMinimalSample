using UnityEngine;
public class ExampleViewModel : IViewModel
{
    private readonly IModel m_exampleModel;

    public ExampleViewModel(IModel exampleModel) // ApplicationStateApplier
    {
        m_exampleModel = exampleModel;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void Dispose()
    {
        Debug.Log($"[{GetType()}] Disposed ExampleViewModel");
    }   
}
