using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ApplicationStateManager : IApplicationStateManager
{
    private readonly IMvvmRegistry m_mvvmRegistry;
    private readonly IMvvmFactory m_mvvmFactory;
    private readonly IApplicationStateDatabase m_applicationStateDatabase;

    public ApplicationStateManager(IMvvmRegistry mvvmRegistry, IMvvmFactory mvvmFactory, IApplicationStateDatabase applicationStateDatabase)
    {
        m_mvvmRegistry = mvvmRegistry;
        m_mvvmFactory = mvvmFactory;
        m_applicationStateDatabase = applicationStateDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public async void SetState(ApplicationStateKey newStateKey)
    {
        Debug.LogWarning($"Set State: {newStateKey}");
        var newState = m_applicationStateDatabase.GetStateByKey(newStateKey);
        DestructCurrentState(newState);
        await Awaitable.NextFrameAsync(); // We need to await one frame here to make sure Unity does destroy the ui document properly
        ConstructNextState(newState);
    }

    public async void AddMvvm(MvvmKey mvvmKey)
    {
        await Awaitable.NextFrameAsync();
        Debug.LogWarning($"Add Mvvm: {mvvmKey}");
        var mvvm = m_mvvmFactory.Create(mvvmKey);
        m_mvvmRegistry.Register(mvvm);
    }

    public async void RemoveMvvm(IViewModel viewModel)
    {
        await Awaitable.NextFrameAsync(); // We need to await one frame here to make sure Unity does destroy the ui document properly
        for (int i = m_mvvmRegistry.ActiveMvvmList.Count - 1; i >= 0; i--)
        {
            var mvvm = m_mvvmRegistry.ActiveMvvmList[i];
            if (mvvm.ViewModel == viewModel)
            {
                m_mvvmRegistry.Unregister(mvvm);
                GameObject.Destroy(mvvm.GameObject);
            }
        }
    }

    private void DestructCurrentState(ApplicationState newState)
    {
        // destroy not required mvvms
        //var mvvmDeletionTasks = new List<Awaitable<Mvvm>>();
        for (int i = m_mvvmRegistry.ActiveMvvmList.Count - 1; i >= 0; i--)
        {
            var mvvm = m_mvvmRegistry.ActiveMvvmList[i];
            if (newState.MvvmKeys.Contains(mvvm.Key) == false)
            {
                m_mvvmRegistry.Unregister(mvvm);
                GameObject.Destroy(mvvm.GameObject);
            }
        }
    }

    private void ConstructNextState(ApplicationState newState)
    {
        // spawn required mvvms
        foreach (var mvvmKey in newState.MvvmKeys)
        {
            if (m_mvvmRegistry.IsRegistered(mvvmKey) == false)
            {
                var mvvm = m_mvvmFactory.Create(mvvmKey);
                m_mvvmRegistry.Register(mvvm);
            }
        }

        // PROPOSAL FOR TRANSITIONHANDLING
        // SEE ALSO TRANSITIONBEHAVIOUR & TRANSITIONSERVICE

        //var mvvmCreationTasks = new List<Awaitable<Mvvm>>();
        //foreach (var mvvmKey in newState.MvvmKeys)
        //{
        //    if (m_mvvmRegistry.IsRegistered(mvvmKey) == false)
        //    {
        //        mvvmCreationTasks.Add(m_mvvmFactory.Create(mvvmKey));
        //    }
        //}

        //foreach(var creationTask in mvvmCreationTasks)
        //{
        //    var mvvm = await creationTask;
        //    m_mvvmRegistry.Register(mvvm);
        //}
    }
}