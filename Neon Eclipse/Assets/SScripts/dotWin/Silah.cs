using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Silah : MonoBehaviour
{
    public bool gameStart = false; //oyunun ba�lay�p ba�lamad���n� kontrol et.
    public float TrueCount = 0f; // Do�ru say�s�n� hesapl�yor
    public float TrueGoal = 10f; // 6 do�ruda g�rev tamam
    public TextMeshProUGUI RandomLetter; // harflerin ��kaca�� text
    public TextMeshProUGUI GeriSayim; // geri say�m nesnesi
    public TextMeshProUGUI loseText; // Oyunu jaybetti�inde
    public float fadeDuration = 3f; // Kaybolma s�resi
    public RectTransform areaSize; // Image koy bu imagein rangini artt�r o range i�inde obje hareket eder
    public GameObject hackPanel; // Oyun panelini ba�la

    

    float minDistance = 101f; // Min. uzakl�k
    float maxDistance = 250f; // Max. uzakl�k
    bool countdownStarted = false; // Geriye say�m�n ba�lay�p ba�lamad���n� kontrol etmek i�in
    float countdownTimer = 1f; // Geriye say�m zamanlay�c�s�
    int countdown = 4; // Geriye say�m de�eri

    Vector3 originalPos; // TextMeshPro'nun orijinal pozisyonunu tutmak i�in

    bool keyPressed = false; // Tu�a bas�l�p bas�lmad���n� kontrol etmek i�in

    public bool enemyHacked = false;

    void Start()
    {
        originalPos = RandomLetter.transform.position;
    }

    string RandomizeLetter()
    {
        // Rastgele bir harf d�nd�r
        char randomChar = (char)Random.Range('a', 'z');
        while (randomChar == 'e')
        {
            randomChar = (char)Random.Range('a', 'z');
        }
        return randomChar.ToString(); // Harfi string olarak d�nd�r
    }

    void Update()
    {
        
        if (gameStart)
        {
            if (!keyPressed && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.E)) //Tu�lama durumu
            {
                keyPressed = true; // Tu�a bas�ld���n� i�aretle

                char pressedKey = Input.inputString.ToLower()[0]; //Tu� de�eri

                //E�erki tu�a dogru bas�l�rsa
                if (pressedKey == RandomLetter.text.ToLower()[0])
                {
                    TrueCount += 1;
                    Debug.Log(TrueCount);

                    if (TrueCount >= TrueGoal)//Goala ula�t���nda (KAZANDI�INDA)
                    {
                        gameStart = false;
                        enemyHacked = true;
                        Time.timeScale = 1f;
                        hackPanel.SetActive(false);
                    }
                    else
                    {
                        ChangeLetterAndMove(); //Letter de�i�
                    }
                    ResetCountdown(); // Geriye say�m� s�f�rla
                }
                //yanl�� bas�l�rsa
                else
                {
                    gameEnd();
                }
            }
            //Geri Say�m
            if (countdownStarted)
            {
                countdownTimer -= Time.unscaledDeltaTime; // Zamanlay�c�y� azalt
                GeriSayim.text = countdownTimer.ToString();
                if (countdownTimer <= 0f)
                {
                    gameEnd();
                    countdown--; // Geriye say
                    countdownTimer = 3f; // Zamanlay�c�y� s�f�rla
                }
            }
        }
        
    }

    void gameEnd()
    {
        TrueCount = 0f; // Do�ru say�s�n� hesapl�yor
        TrueGoal = 10f; // 6 do�ruda g�rev tamam
        fadeDuration = 3f; // Kaybolma s�resi
        Time.timeScale = 1f;
        loseText.gameObject.SetActive(true);
        StartCoroutine(FadeText());
        hackPanel.SetActive(false);
        gameStart = false;
    }

    void ChangeLetterAndMove()
    {
        keyPressed = false; // Tu�a bas�l�p bas�lmad���n� s�f�rla

        RandomLetter.text = RandomizeLetter(); // Rastgele bir harf ata

        float distance = Random.Range(minDistance, maxDistance);

        Vector3 targetPos = originalPos + Random.insideUnitSphere * distance;

        if (RectTransformUtility.RectangleContainsScreenPoint(areaSize, targetPos) && RectTransformUtility.RectangleContainsScreenPoint(areaSize.parent as RectTransform, targetPos))
        {
            RandomLetter.transform.position = targetPos;
        }
    }

    

    void ResetCountdown()
    {
        Debug.Log("cooldown resetlendi");
        Debug.Log(countdown);
        countdownTimer = 3; // Geriye say�m� s�f�rla
        Debug.Log(countdown);
        countdownStarted = true; // Geriye say�m ba�lad�
        GeriSayim.gameObject.SetActive(true); // Geri say�m metnini g�ster
    }

    // Lose text mesh pronun kaybolmas�
    public void StartFadeOut()
    {
        StartCoroutine(FadeText());
    }

    System.Collections.IEnumerator FadeText()
    {
        float elapsedTime = 0f;
        Color initialColor = loseText.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            loseText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // E�er tamamen kaybolmad�ysa s�f�rla
        loseText.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    }


    //Enemy Etkile�imi
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("EnemyBack")) // E�er girilen obje Enemy tag'ine sahipse
        {
            Debug.Log("Enemyinin i�indeyiz");
            // Dokunulan objenin en �st objesine ula��n
            Transform rootObject = other.transform.root;

            // En �st objenin i�indeki FieldOfView bile�enini al�n
            FieldOfView enemyFieldOfView = rootObject.GetComponent<FieldOfView>();
            UnityEngine.AI.NavMeshAgent enemyAgent = rootObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            //Enemy Scriptine Ula�
            Enemy enemyScript =  rootObject.GetComponent<Enemy>();
            // Burada E tu�una bas�ld���nda ve Enemy bizi g�rm�yorsa 
            if (Input.GetKeyDown(KeyCode.E) && !enemyFieldOfView.canSeePlayer)
            {
                // Zaman� durdur
                gameStart = true;
                Time.timeScale = 0f;
                enemyAgent.speed = 0f; // Enemy'nin h�z�n� s�f�ra ayarla
                hackPanel.SetActive(true);
            }
            
            if(enemyHacked)
            {
                enemyScript.TakeDamage(100); // 100 hasar uygula
                other.gameObject.SetActive(false);
            }
        }
    }

}
