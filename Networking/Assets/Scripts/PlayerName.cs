using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{

    InputField nameInput;
    public string savedName = "player";

    void Start()
    {
        nameInput = GameObject.Find("NameInput").GetComponent<InputField>();
        RegisterName();
    }

    public void RegisterName()
    {
        if (nameInput.text != null)
        {
            savedName = nameInput.text;
        }
        else
        {
            savedName = "player";
        }
    }
}
