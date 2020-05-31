using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusic : Subject
{
    private AudioSource _GameMusic;

    [SerializeField]
    private AudioClip[] _gameMusicClip;

    [SerializeField]
    private float _MusicVolume = 0.5f;

    void Awake(){
        _GameMusic = GetComponent<AudioSource>();
    }

    public void OnNotify(NotificationType type)
    {
        switch(type)
        {
            case NotificationType.LEVEL1_START:
                PlayThingsAreLookingUp();
                break;

            case NotificationType.SECOND_CHECKPOINT_DONE:
                PlayShitGotReal();
                break;

            /*
            case NotificationType.LEVEL2_START:
                PlayIGotABadFeeling();                  Music for LEVEL02 part 01
                break;


            case NotificationType.LEVEL2_FIRST_CHECKPOINT_DONE:
                PlayThingsArePickingUp();                  Music for LEVEL02 part 02
                break;

            case NotificationType.LEVEL2_SECOND_CHECKPOINT_DONE:
                PlayBossBattle();                  Music for LEVEL02 part 03
                break;
            */

            case NotificationType.LEVEL1_COMPLETE:
                StopMusic();
                break;

            case NotificationType.GAME_OVER:
               StopMusic();
                break;
        }
    }
    
    void PlayThingsAreLookingUp(){  
        _GameMusic.volume = _MusicVolume;
        _GameMusic.loop = true;
        _GameMusic.clip = _gameMusicClip[0];
        _GameMusic.Play();
    }

    void PlayThingsArePickingUp(){  
        _GameMusic.volume = _MusicVolume;
        _GameMusic.loop = true;
        _GameMusic.clip = _gameMusicClip[1];
        _GameMusic.Play();
    }

    void PlayIGotABadFeeling(){  
        _GameMusic.volume = _MusicVolume;
        _GameMusic.loop = true;
        _GameMusic.clip = _gameMusicClip[2];
        _GameMusic.Play();
    }

    void PlayShitGotReal(){  
        _GameMusic.volume = _MusicVolume;
        _GameMusic.loop = true;
        _GameMusic.clip = _gameMusicClip[3];
        _GameMusic.Play();
    }

    void PlayBossBattle(){
        _GameMusic.volume = _MusicVolume;
        _GameMusic.loop = true;
        _GameMusic.clip = _gameMusicClip[4];
        _GameMusic.Play();
    }

    void StopMusic(){
        _GameMusic.Stop();
    }

}
