using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // G�r�� alan�n�n yar��ap�
    public float radius;

    // G�r�� a��s�, derece cinsinden 0 ile 360 aras�nda
    [Range(0, 360)]
    public float angle;

    // Oyuncu referans�
    public GameObject playerRef;

    // Hedef maskesi
    public LayerMask targetMask;

    // Engel maskesi
    public LayerMask obstructionMask;

    // Oyuncuyu g�rebilir mi?
    public bool canSeePlayer;

    // Start fonksiyonu, ilk frame'den �nce �al���r
    private void Start()
    {
        // Oyuncu referans�n� 'Player' tag'ine sahip nesne olarak al�r
        playerRef = GameObject.FindGameObjectWithTag("Player");

        // FOV rutinini ba�lat�r
        StartCoroutine(FOVRoutine());
    }

    // G�r�� alan� kontrol� i�in s�rekli bir rutin
    private IEnumerator FOVRoutine()
    {
        // 0.2 saniyede bir FOV kontrol� yapar
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // G�r�� alan�n� kontrol eden fonksiyon
    private void FieldOfViewCheck()
    {
        // G�r�� alan�nda hedeflere �arpan collider'lar� al�r
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // E�er bir �eyler alg�land�ysa
        if (rangeChecks.Length != 0)
        {
            // En yak�n hedefi al�r
            Transform target = rangeChecks[0].transform;

            // Hedefe olan y�nelimi belirler
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Y�n ve hedef aras�ndaki a��y� belirler
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Hedefe olan mesafeyi hesaplar
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                RaycastHit hit;

                // E�er hi�bir engel yoksa
                if (!Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true; // Oyuncuyu g�rebilir
                }
                else
                {
                    canSeePlayer = false; // G�remiyor
                }
            }
            else
            {
                canSeePlayer = false; // G�remiyor
            }
        }
        // E�er bir �ey alg�lanmad�ysa ve oyuncuyu g�r�yorsa
        else if (canSeePlayer)
        {
            canSeePlayer = false; // G�rm�yor
        }
    }
}
