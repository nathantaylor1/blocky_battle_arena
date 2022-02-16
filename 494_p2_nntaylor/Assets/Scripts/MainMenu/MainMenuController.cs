using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public InputField _inputField;
    public string seed = "Random";

    public void GrabFromInputField()
    {
        if (_inputField != null && _inputField.text != string.Empty)
        {
            seed = _inputField.text;
        }
    }
    
    public void StartGame()
    {
        GrabFromInputField();
        DataCarryOver.GameSeed = seed;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
