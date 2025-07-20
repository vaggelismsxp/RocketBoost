using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelayNextLevel;
    [SerializeField] float invokeDelayReloadLevel;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    private void RespondToDebugKeys() 
    {
        if (Keyboard.current.lKey.isPressed) 
        {
            LoadNextLevel();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) {return;}

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
        audioSource.Stop();
        audioSource.PlayOneShot(audio);
    }

    private void StartPassLevelSequence()
    {
        isControllable = false;

        // TODO add sfx and particles
        AudioMethod(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", invokeDelayNextLevel);
    }

    void StartCrashSequence()
    {
        isControllable = false;

        //TODO add sfx and particles
        AudioMethod(crashSFX);
        crashParticles.Play();
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
