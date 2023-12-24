using UnityEngine;

public class ApplicationStateManager
{
    private readonly MvvmRegistry m_mvvmRegistry;
    private readonly MvvmFactory m_mvvmFactory;
    private readonly ApplicationStateDatabase m_applicationStateDatabase;

    public ApplicationStateManager(MvvmRegistry mvvmRegistry, MvvmFactory mvvmFactory, ApplicationStateDatabase applicationStateDatabase)
    {
        m_mvvmRegistry = mvvmRegistry;
        m_mvvmFactory = mvvmFactory;
        m_applicationStateDatabase = applicationStateDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public void SetState(ApplicationStateKey newStateKey)
    {
        Debug.LogWarning($"Set State: {newStateKey}");

        // get MVVMs of new state
        var newState = m_applicationStateDatabase.GetStateByKey(newStateKey);

        // destroy not needed mvvms
        for (int i = m_mvvmRegistry.activeMvvmList.Count - 1; i >= 0; i--)
        {
            var mvvm = m_mvvmRegistry.activeMvvmList[i];
            if (newState.MvvmKeys.Contains(mvvm.Key))
                continue;

            m_mvvmRegistry.Unregister(mvvm);
            GameObject.Destroy(mvvm.LifetimeScope.gameObject);
        }

        // spawn required states
        foreach (var mvvmKey in newState.MvvmKeys)
        {
            if (!m_mvvmRegistry.IsRegistered(mvvmKey))
            {
                var mvvm = m_mvvmFactory.Create(mvvmKey);
                m_mvvmRegistry.Register(mvvm);
            }
        }
    }
}