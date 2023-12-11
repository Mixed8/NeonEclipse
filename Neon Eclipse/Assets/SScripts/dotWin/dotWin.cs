using UnityEngine;
using DG.Tweening;

public class dotWin : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;
    public Transform obj3;
    public Transform obj4;
    public Transform obj5;
    public Transform obj6;

    [SerializeField]
    float floorLevelY = 5f;

    void Start()
    {
        DOTween.Init();

        // Ýlk Sequence'in 1 saniye sonra baþlamasý
        DOVirtual.DelayedCall(1f, () =>
        {
            Sequence sequence1 = DOTween.Sequence();
            sequence1.Append(obj1.DOMoveY(obj1.position.y + floorLevelY, 0.5f))
                .Append(obj1.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence1.Play();
        });

        // Ýkinci Sequence'in 3 saniye sonra baþlamasý
        DOVirtual.DelayedCall(1.3f, () =>
        {
            Sequence sequence2 = DOTween.Sequence();
            sequence2.Append(obj2.DOMoveY(obj2.position.y + floorLevelY, 0.5f))
                .Append(obj2.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence2.Play();
        });

        // Üçüncü Sequence'in 5 saniye sonra baþlamasý
        DOVirtual.DelayedCall(1.6f, () =>
        {
            Sequence sequence3 = DOTween.Sequence();
            sequence3.Append(obj3.DOMoveY(obj3.position.y + floorLevelY, 0.5f))
                .Append(obj3.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence3.Play();
        });

        // Dördüncü Sequence'in 5 saniye sonra baþlamasý
        DOVirtual.DelayedCall(1.9f, () =>
        {
            Sequence sequence4 = DOTween.Sequence();
            sequence4.Append(obj4.DOMoveY(obj4.position.y + floorLevelY, 0.5f))
                .Append(obj4.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence4.Play();
        });

        // Beþinci Sequence'in 5 saniye sonra baþlamasý
        DOVirtual.DelayedCall(2.2f, () =>
        {
            Sequence sequence5 = DOTween.Sequence();
            sequence5.Append(obj5.DOMoveY(obj5.position.y + floorLevelY, 0.5f))
                .Append(obj5.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence5.Play();
        });

        // Altýncý Sequence'in 5 saniye sonra baþlamasý
        DOVirtual.DelayedCall(2.5f, () =>
        {
            Sequence sequence6 = DOTween.Sequence();
            sequence6.Append(obj6.DOMoveY(obj6.position.y + floorLevelY, 0.5f))
                .Append(obj6.DOShakePosition(0.5f, 0.2f))
                .AppendInterval(0f);
            sequence6.Play();
        });

        // Devam eden Sequence'lerin süresini ve baþlangýç zamanýný burada ekleyebilirsiniz.
    }
}
