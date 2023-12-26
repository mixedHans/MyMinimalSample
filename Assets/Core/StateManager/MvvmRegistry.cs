using System.Collections.Generic;

public class MvvmRegistry : IMvvmRegistry
{
    private List<Mvvm> activeMvvmList = new();

    public List<Mvvm> ActiveMvvmList { get => activeMvvmList; private set => activeMvvmList = value; }

    public bool IsRegistered(MvvmKey mvvmKey)
    {
        foreach (var mvvm in ActiveMvvmList)
        {
            if (mvvm.Key == mvvmKey)
                return true;
        }

        return false;
    }

    public void Register(Mvvm mvvm)
    {
        ActiveMvvmList.Add(mvvm);
    }

    public void Unregister(Mvvm mvvm)
    {
        ActiveMvvmList.Remove(mvvm);
    }
}
