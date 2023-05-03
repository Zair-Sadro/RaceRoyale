using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioYB _music;
    void Start()
    {
        _music.Play("Base");

    }

    //CarSwitch,Click
    public AudioYB _oneShot1;
    public void CarSwitch()
    {
        _oneShot1.PlayOneShot("switch");
    }
    public void Click()
    {

        _oneShot1.PlayOneShot("click");
    }

    //Lose,Coin,PickGas
    public AudioYB _oneShot2;
    public void Lose()
    {
        Play("lose", _oneShot2);

    }
    public void Coin()
    {
        _oneShot2.volume = 1f;
        _oneShot2.PlayOneShot("coin");

    }
    public void PickGas()
    {
        _oneShot2.volume = 0.07f;
        _oneShot2.PlayOneShot("gas");

    }

    private void Play(string sound, AudioYB source)
    {
        StartCoroutine(MainToZeroAndSound(sound, source));
    }
    IEnumerator MainToZeroAndSound(string sound, AudioYB source)
    {

        while (_music.volume > 0.05f)
        {
            _music.volume -= Time.deltaTime * 1;
            yield return null;
        }

        source.PlayOneShot(sound);
        yield return new WaitForSeconds(0.7f);
        _music.volume = 1;
    }


}
