using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayPeriod = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] ParticleSystem finishEffect;
    private AudioSource audioSource;
    private bool isTransitioning = false;

    private MeshRenderer textRenderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textRenderer = GameObject.Find("hbdText").GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "Launch Pad")
            return;

        if (isTransitioning)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into Friendly object");
                break;
            case "fuel":
                Debug.Log("You've obtained fuel");
                break;
            case "finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crashSound);
            crashEffect.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadScene", delayPeriod);
    }

    private void StartFinishSequence()
    {
            isTransitioning = true;
            textRenderer.enabled = true;
            audioSource.Stop();
            audioSource.PlayOneShot(finishSound);
            finishEffect.Play();
            GetComponent<Movement>().enabled = false;
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex > lastSceneIndex ? 0 : nextSceneIndex);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
