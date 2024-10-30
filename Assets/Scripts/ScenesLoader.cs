using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public Slider barraProgreso;
    public TextMeshProUGUI textoCarga;

    void Start()
    {
        PlayerPrefs.SetString("CargarEscena", "Terreno");
        CargarEscena(PlayerPrefs.GetString("CargarEscena"));

    }

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(CargarEscenaConProgreso(nombreEscena));
    }

    private IEnumerator CargarEscenaConProgreso(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);

            if (barraProgreso != null)
                barraProgreso.value = progreso;

            if (textoCarga != null)
                textoCarga.text = "Cargandurris...." + (progreso + 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
