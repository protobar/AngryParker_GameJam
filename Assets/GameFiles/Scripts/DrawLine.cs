using UnityEngine;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<Vector3> points;
    public Camera mainCamera;
    public float drawDistance = 10f; 
    public GameObject carPrefab;
    public GameObject carSpawnPos;

    void Start()
    {
        carPrefab.transform.position = carSpawnPos.transform.position;
        carPrefab.transform.rotation = carSpawnPos.transform.rotation;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = 0;
        points = new List<Vector3>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, drawDistance))
            {
                Vector3 hitPoint = hit.point;
                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], hitPoint) > 0.1f)
                {
                    points.Add(hitPoint);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearLines();
        }
    }

    public void ClearLines()
    {
        carPrefab.transform.position = carSpawnPos.transform.position;
        carPrefab.transform.rotation = carSpawnPos.transform.rotation;

        points.Clear();
        lineRenderer.positionCount = 0;
    }

    public List<Vector3> GetPoints()
    {
        return points;
    }
}
