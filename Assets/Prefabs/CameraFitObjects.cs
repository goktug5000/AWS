using UnityEngine;

public class CameraFitObjects : MonoBehaviour
{
    public Camera camera;
    public GameObject[] objectsToFit;
    public float padding = 0.3f;
    public float maxPadding = 1f;
    public float minCameraSize = 5f;

    void Update()
    {
        FitCameraToObjects();
    }

    void FitCameraToObjects()
    {
        if (objectsToFit.Length == 0)
            return;

        Bounds bounds = new Bounds(objectsToFit[0].transform.position, Vector3.zero);

        foreach (var obj in objectsToFit)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();

            if (objRenderer != null)
            {
                bounds.Encapsulate(objRenderer.bounds);
            }
        }

        Vector3 sizeWithPadding = bounds.size * (1 + Mathf.Min(padding, maxPadding));

        bounds.size = sizeWithPadding;

        Vector3 center = bounds.center;
        center.z = camera.transform.position.z;
        camera.transform.position = center;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float objectRatio = bounds.size.x / bounds.size.y;

        float requiredSize;
        if (screenRatio >= objectRatio)
        {
            requiredSize = bounds.size.y / 2f;
        }
        else
        {
            requiredSize = bounds.size.x / (2f * screenRatio);
        }

        camera.orthographicSize = Mathf.Max(requiredSize, minCameraSize);
    }
}
