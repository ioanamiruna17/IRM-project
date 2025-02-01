using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Înlocuiește "GameScene" cu numele scenei tale principale
        SceneManager.LoadScene("SampleScene");
    }

    public void StartTutorial()
    {
        // Înlocuiește "GameScene" cu numele scenei tale principale
        SceneManager.LoadScene("TutorialScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Funcționează doar în editor.
        Application.Quit(); // Funcționează în build-ul final.
    }
}