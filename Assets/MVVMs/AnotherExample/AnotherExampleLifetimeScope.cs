using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

// Todo: Rename LifetimeScope to a more mvvm confirm naming
public class AnotherExampleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IModel, AnotherExampleModel>(Lifetime.Scoped);
        builder.Register<IViewModel, AnotherExampleViewModel>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<UIDocument>();

        builder.RegisterBuildCallback(container =>
        {
            Debug.Log($"[{GetType()}] Registered Example LTS");
        });
    }

    protected override void OnDestroy()
    {
        Debug.Log($"[{GetType()}] Destroyed");
        base.OnDestroy();
    }
}