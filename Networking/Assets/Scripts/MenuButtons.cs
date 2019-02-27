using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void ShowObj(GameObject objToShow)
    {
        objToShow.SetActive(true);
    }

    public void HideObj(GameObject objToHide)
    {
        objToHide.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
