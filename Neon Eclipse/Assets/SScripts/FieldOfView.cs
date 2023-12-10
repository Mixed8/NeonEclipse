using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Görüþ alanýnýn yarýçapý
    public float radius;

    // Görüþ açýsý, derece cinsinden 0 ile 360 arasýnda
    [Range(0, 360)]
    public float angle;

    // Oyuncu referansý
    public GameObject playerRef;

    // Hedef maskesi
    public LayerMask targetMask;

    // Engel maskesi
    public LayerMask obstructionMask;

    // Oyuncuyu görebilir mi?
    public bool canSeePlayer;

    // Start fonksiyonu, ilk frame'den önce çalýþýr
    private void Start()
    {
        // Oyuncu referansýný 'Player' tag'ine sahip nesne olarak alýr
        playerRef = GameObject.FindGameObjectWithTag("Player");

        // FOV rutinini baþlatýr
        StartCoroutine(FOVRoutine());
    }

    // Görüþ alaný kontrolü için sürekli bir rutin
    private IEnumerator FOVRoutine()
    {
        // 0.2 saniyede bir FOV kontrolü yapar
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Görüþ alanýný kontrol eden fonksiyon
    private void FieldOfViewCheck()
    {
        // Görüþ alanýnda hedeflere çarpan collider'larý alýr
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // Eðer bir þeyler algýlandýysa
        if (rangeChecks.Length != 0)
        {
            // En yakýn hedefi alýr
            Transform target = rangeChecks[0].transform;

            // Hedefe olan yönelimi belirler
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Yön ve hedef arasýndaki açýyý belirler
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Hedefe olan mesafeyi hesaplar
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                RaycastHit hit;

                // Eðer hiçbir engel yoksa
                if (!Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true; // Oyuncuyu görebilir
                }
                else
                {
                    canSeePlayer = false; // Göremiyor
                }
            }
            else
            {
                canSeePlayer = false; // Göremiyor
            }
        }
        // Eðer bir þey algýlanmadýysa ve oyuncuyu görüyorsa
        else if (canSeePlayer)
        {
            canSeePlayer = false; // Görmüyor
        }
    }
}
