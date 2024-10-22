using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        // Avancer et reculer
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }

        // Tirer un projectile
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            projectileShot();
            StartCoroutine(Cooldown());
        }
        UIscore.text = score.ToString();
    }

    void projectileShot()
    {
        // Créer le projectile
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Récupérer le Rigidbody2D du projectile et lui appliquer la vitesse
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f); // Temps d'attente avant de pouvoir tirer à nouveau
        canShoot = true;
    }
}
