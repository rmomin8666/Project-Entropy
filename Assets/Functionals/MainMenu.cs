using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Need this library to use SceneManager.

public class MainMenu : MonoBehaviour
{
 //[SerializeField] private int numb = 1; //This is used later to load scene. It works by adding current scene location (a number) thus giving a new number (a new scene)
 // Moving this to a new script so the value is assigned to button rather than parent.

  /*  public void PlayGame()
    {
        //You can find the scene menu in Build setting...
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + numb); //can just put a number to load specific scene

     }

*/
    

    public void QuitGame()
    { 
        Debug.Log("Quit!");
        Application.Quit();
    }
}