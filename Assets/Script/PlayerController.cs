using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private bool canShoot = true;

    public int score = 0;
    public TextMeshProUGUI UIscore;

    public Animator anim;

    public GameObject uiheart;

    bool canMove = true;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            anim.SetBool("Gauche", false);
        if (Input.GetKeyUp(KeyCode.D))
            anim.SetBool("Droite", false);
        if (Input.GetKeyUp(KeyCode.W))
            anim.SetBool("Avance", false);
        if (Input.GetKeyUp(KeyCode.S))
            anim.SetBool("Recule", false);


        // Rotation
        if (Input.GetKey(KeyCode.A) && canMove)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            anim.SetBool("Gauche", true);
        }

        if (Input.GetKey(KeyCode.D)&& canMove)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            anim.SetBool("Droite", true);
        }

        // Avancer et reculer
        if (Input.GetKey(KeyCode.W)&& canMove)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            anim.SetBool("Avance", true);
        }
        if (Input.GetKey(KeyCode.S)&& canMove)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
            anim.SetBool("Recule", true);
        }
    

        // Tirer un projectile
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            projectileShot();
            StartCoroutine(Cooldown());
            if (GetComponent<SpriteRenderer>().enabled == false)
                SceneManager.LoadScene(0);

        }
        UIscore.text = string.Format("{0:0000}",score);
    }

    void projectileShot()
    {
        // Cr�er le projectile
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // R�cup�rer le Rigidbody2D du projectile et lui appliquer la vitesse
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f); // Temps d'attente avant de pouvoir tirer � nouveau
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            canMove=false;
            Destroy(uiheart);
        }
    }
}
