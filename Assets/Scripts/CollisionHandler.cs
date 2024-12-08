using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandeller : MonoBehaviour
{
    [SerializeField] float delay=1f;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip explosionSound;

    [SerializeField] ParticleSystem successParticless;
    [SerializeField] ParticleSystem crashParticless;
    
    

    bool isControllable=true;
    bool isCollidable=true;

    AudioSource audioSource;

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();

    }

    private void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
        {
            if(Keyboard.current.lKey.wasPressedThisFrame)
            {
                UpdateLevel();
            }

            else if(Keyboard.current.cKey.wasPressedThisFrame)
            {
                isCollidable = !isCollidable;
            }

        }

    private void OnCollisionEnter (Collision other)
    {
        if (!isControllable || !isCollidable)
            return;
        
        switch (other.gameObject.tag)
        {
            case "friendly":
                
                break;
        
            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }



        void StartSuccessSequence()
        {
            isControllable=false;
            audioSource.Stop();
            audioSource.PlayOneShot(successSound);
            successParticless.Play();
            GetComponent<Movement>().enabled=false;
            Invoke("UpdateLevel", delay);
        }

        void StartCrashSequence()
        {
            isControllable=false;
            audioSource.PlayOneShot(explosionSound);
            crashParticless.Play();
            GetComponent<Movement>().enabled=false;
            Invoke("ReloadLevel", delay);
        }

        void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        void UpdateLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene= currentScene+1 ;

            if (nextScene== SceneManager.sceneCountInBuildSettings)
                nextScene=0;

            SceneManager.LoadScene(nextScene);
        }
    
}
