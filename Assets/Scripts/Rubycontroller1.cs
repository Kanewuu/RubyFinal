using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rubycontroller1 : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;
    public GameObject grenadePrefab;

    public ParticleSystem hea;
    public ParticleSystem damageParticles;
    public int health { get { return currentHealth; }}
    public int currentHealth;
    

    AudioSource audioSource;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    bool isDefeated = false;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    int cogs = 0;
    int grenades = 0;
    int maxGrenades = 3;
    float speedModifier = 1;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        audioSource= GetComponent<AudioSource>();
        GameManager = FindObjectOfType<GameManager>();

        ChangeCogs(4);
        ChangeGrenades(3);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchGrenade();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.CanRestart)
            {
                GameManager.RestartStage(GameManager.HasWon);
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * speedModifier * horizontal * Time.deltaTime;
        position.y = position.y + speed * speedModifier * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (isDefeated) return;

        if (amount < 0)
        {
            Instantiate(damageParticles, transform.position, Quaternion.identity);
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;

        }

        if(amount > 0)
        {
            Instantiate(hea, transform.position, Quaternion.identity);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if(currentHealth == 0 && !isDefeated)
        {
            isDefeated = true;
            GameManager.LooseStage();
        }
    }

    public void ChangeCogs(int amount)
    {
        if (cogs + amount < 0) return;
        cogs += amount;
        CogsUI.instance.SetValue(cogs);
    }

    public void ChangeGrenades(int amount)
    {
        if (grenades + amount < 0) return;
        grenades = Mathf.Clamp(grenades + amount, 0, maxGrenades);
        GrenadesUI.instance.SetValue(grenades);
    }

    public void SlowForSeconds(float amount, float seconds)
    {
        StartCoroutine(ApplySpeedModifierChangeForSeconds(amount, seconds));
    }

    void Launch()
    {
        if (cogs < 1) return;

        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        ChangeCogs(-1);
    }

    void LaunchGrenade()
    {
        if (grenades < 1) return;

        GameObject grenadeObject = Instantiate(grenadePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Granade grenade = grenadeObject.GetComponent<Granade>();
        grenade.Launch(lookDirection, 200);

        animator.SetTrigger("Launch");

        ChangeGrenades(-1);
    }

    public void StopMovement()
    {
        speed = 0;
    }

    private IEnumerator ApplySpeedModifierChangeForSeconds(float amount, float seconds)
    {
        speedModifier = amount;
        yield return new WaitForSeconds(seconds);
        speedModifier = 1;
    }

}

