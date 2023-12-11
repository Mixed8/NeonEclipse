using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    public GameObject[] xObjects; // x1, x2, x3, x4, x5, x6 boþ objelerini içeren dizi
    public Material greenMaterial; // Yeþil renk materyali
    public Material originalMaterial; // Baþlangýç materyali
    public bool canPressSpace = true;

    void Start()
    {
        //küplerin triggerlarýný açma
        ActivateAllCubeTriggers();
        // Her bir x objesi için iþlem yap
        Invoke("SelectRandomCubeAndChangeProperties", 0.1f); // 0.1 saniye bekletip sonra deðiþiklik yap

        // 3 saniye sonra rengi deðiþen küplerin rengini originalMaterial ile deðiþtiren fonksiyonu çaðýr
        Invoke("RevertColorChanges", 3f);
    }

    void Update()
    {
        // Space tuþuna basýldýðýnda ve canPressSpace true ise Start fonksiyonunu çaðýr
        if (Input.GetKeyDown(KeyCode.Space) && canPressSpace)
        {
            Start();
            // Space tuþuna basma hakkýný 4 saniye boyunca kapat
            StartCoroutine(DisableSpacePress());
        }
    }

    IEnumerator DisableSpacePress()
    {
        canPressSpace = false;
        yield return new WaitForSeconds(4f); // 4 saniye beklet
        canPressSpace = true; // Space tuþuna basma hakkýný tekrar aç
    }

    //Tüm küplerin Triggerlarýný açma
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

    //Küplerin Seçilmesi
    void SelectRandomCubeAndChangeProperties()
    {
        foreach (GameObject xObject in xObjects)
        {
            GameObject[] cubes = GetCubesInX(xObject);

            if (cubes.Length > 0)
            {
                GameObject selectedCube = cubes[Random.Range(0, cubes.Length)];
                Debug.Log("Seçilen cube: " + selectedCube.name);

                SetSelectedCubeProperties(selectedCube);
            }
        }
    }

    void SetSelectedCubeProperties(GameObject cube)
    {
        // Küpün renderer bileþenini al
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        // Küpün collider bileþenini al
        Collider cubeCollider = cube.GetComponent<Collider>();

        // Eðer renderer ve collider varsa
        if (cubeRenderer != null && cubeCollider != null)
        {
            // Küpün rengini yeþil olarak ayarla
            cubeRenderer.material = greenMaterial;

            // Collider'ýn isTrigger'ýný kapat
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
                    // Rengi deðiþen küplerin rengini originalMaterial ile deðiþtir
                    cubeRenderer.material = originalMaterial;
                }
            }
        }
    }

    GameObject[] GetCubesInX(GameObject xObject)
    {
        // x objesi içindeki tüm cube'larý bul
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
