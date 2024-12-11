using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{

    public void QuitGame()

    {

        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}