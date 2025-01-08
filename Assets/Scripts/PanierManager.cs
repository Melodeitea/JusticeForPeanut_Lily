using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanierManager : MonoBehaviour
{
    public bool PanierMit;
    public Color newColor = Color.green;
    private Renderer rend;
    private Coroutine changeColorCoro;

    public ScoreSystem scoreSystem;

    //Permet de savoir dès le lancement quel est le renderer//
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    //Foncion et action de la coroutine//
    IEnumerator ChangeColor(Vector3 newPos, float duration, Color newColor)
    {
        //si la couleur est null alors stop//
        if (rend == null)
            yield break;

        //on attend quelques secondes//
        yield return new WaitForSeconds(0.3f);

        //on change la couleur//
        rend.material.color = newColor;

        Vector3 startPos = transform.position;
        float time = 0;

        //quand le temps est inférieur à la durée, on effectue les actions de la fonction//
        while (time < duration)
        {
            time += Time.deltaTime;

            //Lerp
            transform.position = Vector3.Lerp(startPos, newPos, time / duration);

            //si le temps dépasse la durée, on stop//
            yield return null;
        }

        transform.position = newPos;
        changeColorCoro = null;
    }

    //la fonction déclenchée quand le ballon rencontre la zone de collision du panier//
    public void OnDunk()
    {
        if (changeColorCoro != null)
            return;

        if (scoreSystem != null)
        {
            scoreSystem.AugmenteScore();
        }
        //nouvelle position du panier après que le ballon soit rentré//
        Vector3 newPos = transform.position + Vector3.right * 4;
        changeColorCoro = StartCoroutine(ChangeColor(newPos, 1, newColor));
    }
}
