using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    QUIT_GAME,
    START_GAME,
    PLAYER_DEAD,
    LEVEL1_START,
    LEVEL2_START,
    INTRO_DONE,
    FIRST_CHECKPOINT_DONE,
    SECOND_CHECKPOINT_DONE,
    LEVEL1_COMPLETE,
    LEVEL2_COMPLETE,
    GAME_OVER
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
