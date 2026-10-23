using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    public int health = 4;
    private int maxHealth = 4;

    private bool isInvincible = false;
    private bool hasShield;
    private GameManager gameManager;
    private LifeHUD lifeHUD;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        lifeHUD = GameObject.Find("LifeHUD").GetComponent<LifeHUD>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (lifeHUD != null)
        {
           lifeHUD.UpdateLife(health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (PlayerController.Instance != null &&
        PlayerController.Instance.IsControlsReversed())
        {
            return;
        }


        if (isInvincible)
        {
            return;
        }

        if (hasShield)
        {
            hasShield = false;
            return;
        }

        health -= damage;
        lifeHUD.UpdateLife(health);
        if (health <= 0)
        {
            gameManager.GameOver();
        }

        isInvincible = true;
        StartCoroutine(InvincibilityCoroutine());
    }

    public void ActivateShield()
    {
        hasShield = true;
        Debug.Log("Shield activated!");
        //TODO  ativar o efeito visual do escudo
    }

    public void RestoreHealth()
    {
        if (health < maxHealth)
        {
            health++;
            lifeHUD.UpdateLife(health);
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Color transparente =
            new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                0.5f);
        // Atribuindo a cor transparente no SpriteRenderer. 
        spriteRenderer.color = transparente;
        // Aguardo 3 segundos
        yield return new WaitForSeconds(3);
        // Altero Transpar�ncia de volta par 1f;
        transparente.a = 1f;
        // Atribuo novamente a cor para o SpriteRenderer
        spriteRenderer.color = transparente;
        // Perco a invencibilidade;
        isInvincible = false;
    }

}
