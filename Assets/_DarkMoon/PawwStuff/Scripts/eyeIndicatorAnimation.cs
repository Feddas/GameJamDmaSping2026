using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;

using UnityEngine.AI;
using UnityEngine.Events;


public class eyeIndicatorAnimation : MonoBehaviour
{


    public float CurrentHealth { get; set; }
    public bool GameIsEnding { get; private set; }

    private Animator animator;
  
    public UnityAction onIsanityDamaged;
    public UnityAction onIsanityHeal;

    public Health m_Health;


    void awake()
    {
        EventManager.AddListener<AllObjectivesCompletedEvent>(OnAllObjectivesCompleted);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        DebugUtility.HandleErrorIfNullGetComponent<Health, eyeIndicatorAnimation>(m_Health, this, gameObject);
        animator = GetComponent<Animator>();
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnHealed += Heal;
        m_Health.OnDie += OnDie;
        //ObjectiveManager.OnAllObjectivesCompleted += OnGameWin;



        //CurrentHealth = MaxHealth;
        animator.SetBool("Insane", false);
        animator.SetBool("VeryInsane", false);
        animator.SetBool("Wary", false);

        

    }

    void OnAllObjectivesCompleted(AllObjectivesCompletedEvent evt) => OnGameWin();


    void OnGameWin()
    {

        Debug.Log("GAME won animation activated");
        animator.SetBool("GameWin", true);
        animator.SetBool("GameEnd", true);
    }

    void OnDie()
    {
        Debug.Log("game ended");

        animator.SetBool("GameEnd", true);
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
        if (currentHealth < 40)
        {
            Debug.Log("you are low sanity!!!" + currentHealth);
            // very low sanity
            animator.SetBool("Insane", true);
            animator.SetBool("VeryInsane", true);
            animator.SetBool("Wary", true);

        }
        else if (currentHealth < 65)
        {
            // low sanity
            animator.SetBool("Insane", true);
            animator.SetBool("VeryInsane", false);
            animator.SetBool("Wary", true);
        }

        else if (currentHealth < 90)
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
