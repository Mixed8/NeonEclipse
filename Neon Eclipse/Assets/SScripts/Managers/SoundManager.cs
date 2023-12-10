using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    // --- Enemy Audio --- \\
    public AudioClip enemyWalking;
    public AudioClip enemyChase;
    public AudioClip enemyAttack;
    public AudioClip enemyDeath;

    public AudioSource enemyChannel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            enemyChannel = GetComponent<AudioSource>();
            // Diðer baþlangýç ayarlarýný burada yapabilirsiniz
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
