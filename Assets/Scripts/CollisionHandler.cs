using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelayNextLevel;
    [SerializeField] float invokeDelayReloadLevel;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip passAudio;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Omygod its so hard");
                break;
            case "Fuel":
                Debug.Log("Fuel on emergency");
                break;
            case "Finish":
                StartPassLevelSequence();
                break;
            default:
                StartCrashSequence();
                //SceneManager.LoadScene("Sandbox");
                break;
        }
    }
    void AudioMethod(AudioClip audio) 
    {
        audioSource.PlayOneShot(audio);
    }

    private void StartPassLevelSequence()
    {
        // TODO add sfx and particles
        AudioMethod(passAudio);

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", invokeDelayNextLevel);
    }

    void StartCrashSequence()
    {
        //TODO add sfx and particles
        AudioMethod(crashAudio);

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", invokeDelayReloadLevel);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex; nextScene++;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
            nextScene = 0;

        SceneManager.LoadScene(nextScene);
    }
}
