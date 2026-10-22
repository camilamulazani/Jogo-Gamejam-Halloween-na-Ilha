using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerHealth : MonoBehaviour
{
    public int health = 4;

    private bool isInvincible = false;
    private GameManager gameManager;
    private LifeHUD lifeHUD;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        lifeHUD = GameObject.Find("LifeHUD").GetComponent<LifeHUD>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (lifeHUD != null)
        {
           lifeHUD.UpdateLife(health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            lifeHUD.UpdateLife(health);
            if (health <= 0)
            {
                gameManager.GameOver();
            }
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine());
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
        isInvincible = false;
    }

}
