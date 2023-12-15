using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    void Update()
    {

        float xDir = 0.0f;
        float yDir = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            yDir = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yDir = -1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xDir = -1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xDir = 1.0f;
        }

        Vector2 moveDirection = new Vector2(xDir, yDir).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime);
        SpacebarCheck();
    }

    public GameObject ProjectilePrefab;
   

    
    private WeaponClass currentWeapon = WeaponClass.None;

    void SpacebarCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireCurrentSelectedWeapon();
        }
    }
    public enum WeaponClass
    {
        None,
        Rifle,
        Shotgun,
        Grenade
    }
    void FireCurrentSelectedWeapon()
    {
        switch (currentWeapon)
        {
            case WeaponClass.Rifle:
                FireRifle();
                break;
            
            case WeaponClass.Shotgun:
                FireShotgun();
                break;
            
            case WeaponClass.Grenade:
                FireGrenade();
                break;
            
            default:
                
                break;
        }
    }

    void FireRifle()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 10; 
        Destroy(projectile, 1.0f); 
    }

    void FireSingleShotgunBullet(Vector2 direction)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 7.50f;
        Destroy(projectile, 1.0f);
    }

    void FireShotgun()
    {
        FireSingleShotgunBullet(transform.right); 
        FireSingleShotgunBullet(Quaternion.Euler(0, 0, 30) * transform.right); 
        FireSingleShotgunBullet(Quaternion.Euler(0, 0, -30) * transform.right); 
    }

    void FireGrenade()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 5; 
        Destroy(projectile, 1.0f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rifle"))
        {
            SwitchWeaponClass(WeaponClass.Rifle);

            SwitchSpriteRendererColor(Color.red);
        }

        else if (collision.CompareTag("Shotgun"))
        {
            SwitchWeaponClass(WeaponClass.Shotgun);

            SwitchSpriteRendererColor(Color.green);
        }

        else if (collision.CompareTag("Grenade"))
        {
            SwitchWeaponClass(WeaponClass.Grenade);

            SwitchSpriteRendererColor(Color.blue);
        }

        Debug.Log(name + " is colliding with " + collision.name);
    }

    private Color playerColor = Color.white;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void SwitchSpriteRendererColor(Color colorSwitch)
    {
        playerColor = colorSwitch;

        spriteRenderer.color = playerColor;
    }

    private void SwitchWeaponClass(WeaponClass weaponSwitch)
    {
        currentWeapon = weaponSwitch;
    }
}


/*public class WeaponClassDefine : MonoBehaviour
{

    public enum WeaponClass
    {
        None,
        Rifle,
        Shotgun,
        Grenade
    }
        
        private Color playerColor = Color.white;
        private WeaponClass currentWeapon = WeaponClass.None;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        

        private void SwitchSpriteRendererColor(Color colorSwitch)
        {
            playerColor = colorSwitch;
            
            spriteRenderer.color = playerColor; 
        }

        private void SwitchWeaponClass(WeaponClass weaponSwitch)
        {
            currentWeapon = weaponSwitch;
        }
    }*/
