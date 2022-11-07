using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _playerAudioSource;
    [SerializeField] AudioClip[] _playerAudioClips;

    //Play Sound for OnPLayerDamage
    public void OnPlayerDamageSound() {
        _playerAudioSource.PlayOneShot(_playerAudioClips[0]);
    }

    //Play Sound for OnPlayerDeath
    public void OnPlayerDeathSound() {
        _playerAudioSource.PlayOneShot(_playerAudioClips[1]);
    }
}
