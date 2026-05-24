using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;

using UnityEngine.AI;
using UnityEngine.Events;


public class eyeIndicatorAnimation : MonoBehaviour
{


    public float CurrentHealth { get; set; }
    private Animator animator;
  
    public UnityAction onIsanityDamaged;
    public UnityAction onIsanityHeal;

    public Health m_Health;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        DebugUtility.HandleErrorIfNullGetComponent<Health, eyeIndicatorAnimation>(m_Health, this, gameObject);
        animator = GetComponent<Animator>();
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnHealed += Heal;

     

        //CurrentHealth = MaxHealth;
        animator.SetBool("Insane", false);
        animator.SetBool("VeryInsane", false);
        animator.SetBool("Wary", false);

        

    }



    public void Heal(float healAmount)
    {

        checkInsanity(m_Health.CurrentHealth);
        
        
    }


    void OnDamaged(float damage, GameObject damageSource)
    {
        // test if the damage source is the player

        checkInsanity(m_Health.CurrentHealth);
        Debug.Log(m_Health.CurrentHealth);  

        onIsanityDamaged?.Invoke();

        animator.SetTrigger("Hit");
       


    }

    // Update is called once per frame
    void Update()
    {

        // Start the coroutine
        //StartCoroutine(WaitAndExecute());
        
        //checkInsanity(CurrentHealth);
    }

    // Must return IEnumerator
    IEnumerator WaitAndExecute()
    {
        Debug.Log("Waiting started...");

        // Pause execution for 5 seconds
        yield return new WaitForSeconds(2f);

        Debug.Log("5 seconds have passed!");

        // Put your post-delay code here (e.g., turn off an animation parameter)
    }


    void checkInsanity(float currentHealth)
    {
        if (currentHealth < 30)
        {
            Debug.Log("you are low sanity!!!" + currentHealth);
            // very low sanity
            animator.SetBool("Insane", true);
            animator.SetBool("VeryInsane", true);
            animator.SetBool("Wary", true);

        }
        else if (currentHealth < 60)
        {
            // low sanity
            animator.SetBool("Insane", true);
            animator.SetBool("VeryInsane", false);
            animator.SetBool("Wary", true);
        }

        else if (CurrentHealth < 80)
        {
            // wary
            animator.SetBool("Insane", false);
            animator.SetBool("VeryInsane", false);
            animator.SetBool("Wary", true);
        }
        else
        {
            // sane
            animator.SetBool("Insane", false);
            animator.SetBool("VeryInsane", false);
            animator.SetBool("Wary", false);
        }
    }
}
