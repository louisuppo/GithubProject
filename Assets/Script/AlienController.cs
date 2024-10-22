using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    //public GameObject player; // Référence à l'objet joueur

    void Update()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Déplacer l'ennemi vers le joueur
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
