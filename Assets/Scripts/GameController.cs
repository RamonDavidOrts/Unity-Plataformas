using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int vidas;
    int puntos;

    int itemsRestantes;

    [SerializeField] UnityEngine.UI.Text textoGameOver = null;
    [SerializeField] UnityEngine.UI.Text textoVidas = null;
    [SerializeField] UnityEngine.UI.Text textoPuntos = null;
    [SerializeField] UnityEngine.UI.Text textoItems = null;

    // Start is called before the first frame update
    void Start()
    {
        vidas = FindObjectOfType<GameStatus>().vidas;
        textoVidas.text = "Vidas: " + vidas;
        puntos = FindObjectOfType<GameStatus>().puntos;
        textoPuntos.text = "Puntos: " + puntos;

        itemsRestantes = FindObjectsOfType<Diamond>().Length;
        textoItems.text = "Items: " + itemsRestantes;
        textoGameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnotarItemRecogido()
    {
        puntos += 10;
        FindObjectOfType<GameStatus>().puntos = puntos;
        textoPuntos.text = "Puntos: " + puntos;

        itemsRestantes--;
        textoItems.text = "Items: " + itemsRestantes;
        if (itemsRestantes <= 0)
        {
            AvanzarNivel();
        }
    }

    void PerderVida()
    {
        vidas--;
        FindObjectOfType<GameStatus>().vidas = vidas;
        FindObjectOfType<Player>().SendMessage("Recolocar");
        textoVidas.text = "Vidas: " + vidas;

        if ( vidas <= 0 )
        {
            StartCoroutine(TerminarPartida());
        }
    }

    void AvanzarNivel()
    {
        FindObjectOfType<GameStatus>().nivelActual++;
        if (FindObjectOfType<GameStatus>().nivelActual 
            > FindObjectOfType<GameStatus>().nivelMasAlto)
        {
            FindObjectOfType<GameStatus>().nivelActual = 1;
        }
        SceneManager.LoadScene("Nivel" + 
            FindObjectOfType<GameStatus>().nivelActual);
    }

    IEnumerator TerminarPartida()
    {
        textoGameOver.enabled = true;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
