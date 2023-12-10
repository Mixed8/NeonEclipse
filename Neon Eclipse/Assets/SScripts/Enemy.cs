using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100; //can bar�
    private Animator animator; //animator tan�mlama

    private UnityEngine.AI.NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //Animatoru �a��rma
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Nav Agent �a��r
        StartCoroutine(ApplyDamageAfterDelay(100f)); // 5 saniye gecikme ile hasar uygulama başlat
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount; //Damage al�nd���nda hp den d��me

        
        if(HP <= 0) //�lme animasyonu belirleme
        {
            int randomValue = Random.Range(0, 2); //0 1

            if(randomValue == 0)
            {
                animator.SetTrigger("DIE");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }

            //Dead Sound
            SoundManager.Instance.enemyChannel.PlayOneShot(SoundManager.Instance.enemyDeath);
        }
    }
    // İstenilen gecikme süresinden sonra hasar uygulama coroutine'u
    private IEnumerator ApplyDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Belirlenen süre kadar bekle

        if (HP > 0) // Eğer düşman hala canlıysa
        {
            TakeDamage(100); // 100 hasar uygula
        }
    }

    //Enemy Detection Attack Chasing Area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // Attack // Stop Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); // Detection(Start Casing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); // Stop Casing
    }

}
