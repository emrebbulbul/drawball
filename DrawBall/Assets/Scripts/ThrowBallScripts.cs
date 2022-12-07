using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallScripts : MonoBehaviour
{
    
    [SerializeField] private GameObject[] balls;
    [SerializeField] private Transform throwBallCenter;
    [SerializeField] private GameObject hoop;
    [SerializeField] private GameObject[] hoopdots;
    

    int activeBallIndex;
    int randomHoopPointIndex;
    bool keylock;

    public void GamePlay()
    {
        StartCoroutine(ThrowBallSystem()); 
    }
    IEnumerator ThrowBallSystem()
    {
        while (true)
        {
            if (!keylock)
            {
                yield return new WaitForSeconds(.5f);
                balls[activeBallIndex].transform.position = throwBallCenter.position;
                balls[activeBallIndex].SetActive(true);
                float Angle = Random.Range(70f, 110f);
                
                Vector3 Pos = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;
                
                balls[activeBallIndex].GetComponent<Rigidbody2D>().AddForce(750 * Pos);
               
                

                if (activeBallIndex != balls.Length - 1)
                {
                    activeBallIndex++;
                }
                else
                {
                    activeBallIndex = 0;
                }
                yield return new WaitForSeconds(1f);
                randomHoopPointIndex = Random.Range(0, hoopdots.Length - 1);
                hoop.transform.position = hoopdots[randomHoopPointIndex].transform.position;
                hoop.SetActive(true);
                keylock = true;
                Invoke("TimeSystem", 5f);
            }
            else
            {
                yield return null;
            }
        }

        
    }
   
    public void GoOn()
    {
        keylock = false;
        hoop.SetActive(false);
        CancelInvoke();

    }
    public void BallStop()
    {
        StopAllCoroutines();

    }
    void TimeSystem()
    {
        if (keylock)
        {
            GetComponent<GameManager>().GameOver();
        }

    }
}
