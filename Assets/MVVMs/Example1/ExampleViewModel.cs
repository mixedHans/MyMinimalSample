using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;


public interface IViewModel : IDisposable { }
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

public class ApplicationStateManager
{
    private readonly StateTransitionManager m_stateTransitionManager;

    public ApplicationStateManager(StateTransitionManager stateTransitionManager)
    {
        m_stateTransitionManager = stateTransitionManager;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void SetState(ApplicationStateKey applicationState)
    {
        Debug.LogError($"[{GetType()}] SetState: {applicationState}");
        m_stateTransitionManager.TransitionToState(applicationState);
    }
}

public class StateFactory
{

}

public class StateTransitionManager
{
    private readonly MvvmRegistry m_mvvmRegistry;
    private readonly MvvmFactory m_mvvmFactory;
    private readonly ApplicationStateDatabase m_applicationStateDatabase;

    public StateTransitionManager(MvvmRegistry mvvmRegistry, MvvmFactory mvvmFactory, ApplicationStateDatabase applicationStateDatabase)
    {
        m_mvvmRegistry = mvvmRegistry;
        m_mvvmFactory = mvvmFactory;
        m_applicationStateDatabase = applicationStateDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void TransitionToState(ApplicationStateKey newState)
    {
        // get MVVMs of new state
        var mvvmsRequiredInNewState = m_applicationStateDatabase.keyValuePairs[newState];

        // destroy not needed mvvms
        for(int i = m_mvvmRegistry.activeMvvmList.Count - 1; i >= 0; i--)
        {
            var mvvm = m_mvvmRegistry.activeMvvmList[i];
            if (mvvmsRequiredInNewState.Contains(mvvm.Key))
                continue;

            m_mvvmRegistry.Unregister(mvvm);
            GameObject.Destroy(mvvm.LifetimeScope.gameObject);
        }

        // spawn required states
        foreach (var mvvmKey in mvvmsRequiredInNewState)
        {
            if (!m_mvvmRegistry.IsRegistered(mvvmKey))
            {
                var mvvm = m_mvvmFactory.Create(mvvmKey);
                m_mvvmRegistry.Register(mvvm);
            }
        }
    }
}

public enum ApplicationStateKey
{
    Example,
    AnotherExample, 
    YetAnotherExample,
}

public class ApplicationStateDatabase
{
    public Dictionary<ApplicationStateKey, List<MvvmKey>> keyValuePairs = new()
    {
        { ApplicationStateKey.Example, new List<MvvmKey>() { MvvmKey.ExampleMvvm } },
        { ApplicationStateKey.AnotherExample, new List<MvvmKey>() { MvvmKey.AnotherExampleMvvm } },
        { ApplicationStateKey.YetAnotherExample, new List<MvvmKey>() { MvvmKey.ExampleMvvm, MvvmKey.AnotherExampleMvvm } },
    };
}

public class MvvmRegistry
{
    public List<MVVM> activeMvvmList = new();

    public bool IsRegistered(MVVM mvvm)
    {
        return activeMvvmList.Contains(mvvm); 
    }

    public bool IsRegistered(MvvmKey mvvmKey)
    {
        foreach(var mvvm in activeMvvmList)
        {
            if(mvvm.Key == mvvmKey)
                return true;
        }

        return false;
    }

    public void Register(MVVM mvvm)
    {
        activeMvvmList.Add(mvvm);
    }

    public void Unregister(MVVM mvvm)
    {
        activeMvvmList.Remove(mvvm);
    }
}

public class MvvmFactory
{
    private readonly MvvmDatabase m_mvvmDatabase;

    public MvvmFactory(MvvmDatabase mvvmDatabase)
    {
        this.m_mvvmDatabase = mvvmDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public MVVM Create(MvvmKey mvvmKey) 
    {
        Debug.Log($"[{GetType()}] Spawn MVVM: {mvvmKey}");
        m_mvvmDatabase.mvvmKeyValuePairs.TryGetValue(mvvmKey, out var mvvmResourceName);
        var lifetimeScopePrefab = Resources.Load(mvvmResourceName).GetComponent<LifetimeScope>();
        lifetimeScopePrefab.autoRun = false;
        var lifetimeScope = GameObject.Instantiate(lifetimeScopePrefab);
        lifetimeScope.Build();
        var viewModel = lifetimeScope.Container.Resolve<IViewModel>();
        var model = lifetimeScope.Container.Resolve<IModel>();
        return new MVVM(mvvmKey, lifetimeScope, viewModel, model); 
    }
}

public class MVVM
{
    public MVVM(MvvmKey key, LifetimeScope lifetimeScope, IViewModel viewModel, IModel model)
    {
        Key = key;
        LifetimeScope = lifetimeScope;
        ViewModel = viewModel;
        Model = model;
        Debug.Log($"[{key}] Constructor called");
    }

    public MvvmKey Key { get; }
    public LifetimeScope LifetimeScope { get; }
    public IViewModel ViewModel { get; }
    public IModel Model { get; }
}

public class MvvmDatabase
{
    public Dictionary<MvvmKey, string> mvvmKeyValuePairs = new()
    {
        { MvvmKey.ExampleMvvm, "ExampleLifetimeScope" },
        { MvvmKey.AnotherExampleMvvm, "AnotherExampleLifetimeScope" },
    };
}

public enum MvvmKey
{
    ExampleMvvm,
    AnotherExampleMvvm,
}