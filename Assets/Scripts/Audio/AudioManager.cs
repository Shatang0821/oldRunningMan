using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    [SerializeField] float minPitch = 0.9f;

    [SerializeField] float maxPitch = 1.1f;

    /// <summary>
    /// �����o��
    /// </summary>
    /// <param name="audioData">���f�[�^</param>
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip,audioData.volueme);
    }

    // Used for repeat-player SFX
    /// <summary>
    /// Pitch�������_���ɕύX���ĉ����o��
    /// </summary>
    /// <param name="audioData">���f�[�^</param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
        PlaySFX(audioData);
    }

    /// <summary>
    /// �������̉����������_���ɗ���
    /// </summary>
    /// <param name="audioData">���f�[�^�z��</param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
}

/// <summary>
/// AudioClip��volueme���܂Ƃ߂�N���X
/// </summary>
[System.Serializable] 
public class AudioData
{
    /// <summary>
    /// ����
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// ����
    /// </summary>
    public float volueme;
}
