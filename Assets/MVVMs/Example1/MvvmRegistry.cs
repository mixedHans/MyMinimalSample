using System.Collections.Generic;

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
