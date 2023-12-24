using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExampleService", menuName = "Services/ExampleService/Create ExampleService")]
public class ExampleService : ScriptableObject
{
    public ExampleServiceModel ExampleServiceModel;

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
