using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<Vector3> points;
    public Camera mainCamera;
    public float drawDistance = 10f;
    public GameObject carPrefab;
    public GameObject carSpawnPos;
    private bool isDrawing;
    private bool canDraw;
    public float lineSmoothValue = 0.5f;

    void Start()
    {
        
        carSpawnPos = LevelManager.instance.currentCarPos;
        //print("Got = " + carSpawnPos.name);
        carPrefab.transform.position = carSpawnPos.transform.position;
        carPrefab.transform.rotation = carSpawnPos.transform.rotation;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = 0;
        points = new List<Vector3>();
        canDraw = true;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (canDraw && Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElementWithTag("BlockLine"))
            {
                return; // Prevent drawing if the pointer is over a UI element with the tag "BlockLine"
            }
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, drawDistance))
            {
                Vector3 hitPoint = hit.point;
                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], hitPoint) > lineSmoothValue)
                {
                    points.Add(hitPoint);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                    MoveCube.launchAllowed = true;

                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
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
        canDraw = true;
        isDrawing = false;
    }

    public List<Vector3> GetPoints()
    {
        return points;
    }

    private bool IsPointerOverUIElementWithTag(string tag)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
