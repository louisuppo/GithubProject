using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public Animator anim;

    public Rigidbody2D rb;

    private Vector2 movement; // Pour stocker la direction de mouvement calculée

    void Update()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Appliquer le mouvement
            movement = direction;

            // Mettre à jour les paramètres d'animation en fonction de la direction
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);

            // Déplacer l'alien vers le joueur
            transform.position += (Vector3)movement * speed * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Déplacer l'alien en utilisant le Rigidbody2D
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
