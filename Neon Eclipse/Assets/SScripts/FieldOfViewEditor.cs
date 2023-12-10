using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    // Sahne i�indeki g�r�� alan�n�n �izimini yapar
    private void OnSceneGUI()
    {
        // Hedeflenen FieldOfView bile�enini al�r
        FieldOfView fov = (FieldOfView)target;

        // G�r�� alan�n�n kenarlar�n� �izer
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // G�r�� a��s�n�n kenarlar�n� hesaplar ve �izer
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        // E�er oyuncu g�r�lebiliyorsa, bir �izgi �izer
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    // Verilen a��dan bir y�nlendirme vekt�r� hesaplar
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        float angle = eulerY + angleInDegrees;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
