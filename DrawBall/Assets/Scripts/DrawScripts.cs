using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawScripts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI drawRightText;
    public GameObject  linePrefab;
    public GameObject draw;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositionList;
    public List<GameObject> draws;
    [SerializeField] private GameObject[] backGrounds;
    [SerializeField] private GameManager gameManager;


    public bool keyDraw=false;
    public int drawRight;

    void Start()
    {
        drawRight = 3;
        drawRightText.text = drawRight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyDraw && drawRight != 0)
        {
            if (true) //Input.touchCount > 0
            {
                //Touch touch = Input.GetTouch(0); 
                if (Input.GetMouseButtonDown(0))//touch.phase==TouchPhase.Began
                {
                    DrawCreate();
                }
                if (Input.GetMouseButton(0)) //touch.phase == TouchPhase.Moved
                {
                    Vector2 FingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (Vector2.Distance(FingerPosition, fingerPositionList[^1]) > .1f)
                    {
                        DrawUpdate(FingerPosition);
                    }

              
                }
                if (fingerPositionList.Count != 0 && drawRight != 0)
                {
                    if (Input.GetMouseButtonUp(0)) //touch.phase == TouchPhase.Ended
                    {
                        drawRight--;
                        drawRightText.text = drawRight.ToString();

                    }

                }
            }
            
        }
            
        
    }

    void DrawCreate()
    {
        draw = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        draws.Add(draw);
        lineRenderer = draw.GetComponent<LineRenderer>();
        edgeCollider = draw.GetComponent<EdgeCollider2D>();
        fingerPositionList.Clear();
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositionList[0]);
        lineRenderer.SetPosition(1, fingerPositionList[1]);
        edgeCollider.points = fingerPositionList.ToArray();

        if (  gameManager.goalsNumbers %2==0)
        {
            lineRenderer.startColor = Color.magenta;
            lineRenderer.endColor = Color.magenta;

        }
        


    }
    void DrawUpdate(Vector2 ComingFingerPosition)
    {

        fingerPositionList.Add(ComingFingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, ComingFingerPosition);
        edgeCollider.points = fingerPositionList.ToArray();
    }


    public void GoOn()
    {
        foreach (var item in draws)
        {
            Destroy(item.gameObject);
        }
        draws.Clear();
        drawRight = 3;
        drawRightText.text = drawRight.ToString();
        BackGrounds();
       

    }
    int x = 1;
    void BackGrounds()
    {
        if (gameManager.goalsNumbers%2==0)
        {
            backGrounds[x - 1].SetActive(false);
            backGrounds[x].SetActive(true);

            x++;
            
        }
        if (x == 7)
        {
            x = 1;
        }

    }
}
