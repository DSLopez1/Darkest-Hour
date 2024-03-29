using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject firstObj;
    [SerializeField] GameObject optionsObj;

    public void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }


    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Main Story");
    }


    public void OnOptionsButtonClicked()
    {
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsObj);
        
    }

    public void OptionsBack()
    {
        EventSystem.current.SetSelectedGameObject(firstObj);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
