using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    QUIT_GAME,
    START_GAME,
    PLAYER_DEAD,
    UI_LEVEL1_START,
    UI_LEVEL2_START,
    LEVEL1_COMPLETE,
    LEVEL2_COMPLETE,
    GLOBAL_GAME_OVER
};

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(NotificationType type);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer newObserver)
    {
        observers.Add(newObserver);
    }

    public void Notify(NotificationType type)
    {
        foreach(Observer watchdog in observers)
        {
            watchdog.OnNotify(type);
        }
    }
}
