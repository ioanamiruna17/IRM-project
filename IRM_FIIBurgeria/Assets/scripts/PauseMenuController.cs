using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource backgroundMusic; 

    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volumeSlider.value; 
            volumeSlider.onValueChanged.AddListener(SetVolume); 
        }
    }

    public void SetVolume(float value)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = value;
        }
    }

    public GameObject pauseMenuUI; 
    private bool isPaused = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;          
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit(); 
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;         
        isPaused = true;
    }
}