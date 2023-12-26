public interface IApplicationStateDatabase
{
    ApplicationState GetStateByKey(ApplicationStateKey key);
}