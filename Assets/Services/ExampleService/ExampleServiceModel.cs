using UnityEngine;

[CreateAssetMenu(fileName = "ExampleServiceModel", menuName = "Services/ExampleService/Create ExampleServiceModel")]
public class ExampleServiceModel : ScriptableObject
{
    // Properties
    public ExampleServiceValueObject ServiceValueObject 
    { 
        get => m_serviceValueObject;
        set
        {
            m_serviceValueObject = value;
            ExampleServiceModelChanged?.Invoke(m_serviceValueObject);
        }
    }

    // Events
    public event ExampleServiceModelChangedEvent ExampleServiceModelChanged;

    // Members
    private ExampleServiceValueObject m_serviceValueObject;
}

public delegate void ExampleServiceModelChangedEvent(ExampleServiceValueObject eventArgs);

public class ExampleServiceValueObject
{
    public string ExampleValue { get; set; }
}