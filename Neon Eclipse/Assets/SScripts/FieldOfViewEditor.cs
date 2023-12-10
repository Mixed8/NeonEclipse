using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    // Sahne içindeki görüþ alanýnýn çizimini yapar
    private void OnSceneGUI()
    {
        // Hedeflenen FieldOfView bileþenini alýr
        FieldOfView fov = (FieldOfView)target;

        // Görüþ alanýnýn kenarlarýný çizer
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // Görüþ açýsýnýn kenarlarýný hesaplar ve çizer
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        // Eðer oyuncu görülebiliyorsa, bir çizgi çizer
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    // Verilen açýdan bir yönlendirme vektörü hesaplar
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        float angle = eulerY + angleInDegrees;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
