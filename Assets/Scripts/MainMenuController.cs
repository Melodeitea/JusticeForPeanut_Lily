using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Cette fonction sera appelée lorsque le joueur appuie sur le bouton "Démarrer"
    public void StartGame()
    {
        // Charge la scène du jeu (assurez-vous d'avoir ajouté la scène du jeu à la build)
        SceneManager.LoadScene("BasketScene");  // Remplacez "GameScene" par le nom de votre scène de jeu
    }

    // Cette fonction sera appelée pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}