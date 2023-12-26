using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

public class ExampleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IModel, ExampleModel>(Lifetime.Scoped);
        builder.Register<IViewModel, ExampleViewModel>(Lifetime.Scoped);
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
