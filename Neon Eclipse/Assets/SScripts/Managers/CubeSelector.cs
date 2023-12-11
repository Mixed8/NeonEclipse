using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    public GameObject[] xObjects; // x1, x2, x3, x4, x5, x6 bo� objelerini i�eren dizi
    public Material greenMaterial; // Ye�il renk materyali
    public Material originalMaterial; // Ba�lang�� materyali
    public bool canPressSpace = true;

    void Start()
    {
        //k�plerin triggerlar�n� a�ma
        ActivateAllCubeTriggers();
        // Her bir x objesi i�in i�lem yap
        Invoke("SelectRandomCubeAndChangeProperties", 0.1f); // 0.1 saniye bekletip sonra de�i�iklik yap

        // 3 saniye sonra rengi de�i�en k�plerin rengini originalMaterial ile de�i�tiren fonksiyonu �a��r
        Invoke("RevertColorChanges", 3f);
    }

    void Update()
    {
        // Space tu�una bas�ld���nda ve canPressSpace true ise Start fonksiyonunu �a��r
        if (Input.GetKeyDown(KeyCode.Space) && canPressSpace)
        {
            Start();
            // Space tu�una basma hakk�n� 4 saniye boyunca kapat
            StartCoroutine(DisableSpacePress());
        }
    }

    IEnumerator DisableSpacePress()
    {
        canPressSpace = false;
        yield return new WaitForSeconds(4f); // 4 saniye beklet
        canPressSpace = true; // Space tu�una basma hakk�n� tekrar a�
    }

    //T�m k�plerin Triggerlar�n� a�ma
    void ActivateAllCubeTriggers()
    {
        foreach (GameObject xObject in xObjects)
        {
            GameObject[] cubes = GetCubesInX(xObject);

            foreach (GameObject cube in cubes)
            {
                Collider cubeCollider = cube.GetComponent<Collider>();
                if (cubeCollider != null)
                {
                    cubeCollider.isTrigger = true;
                }
            }
        }
    }

    //K�plerin Se�ilmesi
    void SelectRandomCubeAndChangeProperties()
    {
        foreach (GameObject xObject in xObjects)
        {
            GameObject[] cubes = GetCubesInX(xObject);

            if (cubes.Length > 0)
            {
                GameObject selectedCube = cubes[Random.Range(0, cubes.Length)];
                Debug.Log("Se�ilen cube: " + selectedCube.name);

                SetSelectedCubeProperties(selectedCube);
            }
        }
    }

    void SetSelectedCubeProperties(GameObject cube)
    {
        // K�p�n renderer bile�enini al
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        // K�p�n collider bile�enini al
        Collider cubeCollider = cube.GetComponent<Collider>();

        // E�er renderer ve collider varsa
        if (cubeRenderer != null && cubeCollider != null)
        {
            // K�p�n rengini ye�il olarak ayarla
            cubeRenderer.material = greenMaterial;

            // Collider'�n isTrigger'�n� kapat
            cubeCollider.isTrigger = false;
        }

    }

    void RevertColorChanges()
    {
        foreach (GameObject xObject in xObjects)
        {
            GameObject[] cubes = GetCubesInX(xObject);

            foreach (GameObject cube in cubes)
            {
                Renderer cubeRenderer = cube.GetComponent<Renderer>();
                if (cubeRenderer != null)
                {
                    // Rengi de�i�en k�plerin rengini originalMaterial ile de�i�tir
                    cubeRenderer.material = originalMaterial;
                }
            }
        }
    }

    GameObject[] GetCubesInX(GameObject xObject)
    {
        // x objesi i�indeki t�m cube'lar� bul
        List<GameObject> cubeList = new List<GameObject>();
        foreach (Transform child in xObject.transform)
        {
            if (child.CompareTag("Cube"))
            {
                cubeList.Add(child.gameObject);
            }
        }
        return cubeList.ToArray();
    }
}
