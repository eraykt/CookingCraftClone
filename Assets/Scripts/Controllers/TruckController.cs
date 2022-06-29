using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public Animator animation;

    [SerializeField] MineController mine;

    [SerializeField] Transform StackTransform;

    public int truckIndex = 8;


    public float ArrivingTime;
    bool isTruckGone;
    public bool isTruckNeeded = true;

    private void Awake()
    {
        animation = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(TruckArriving());
        //Time.timeScale = 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectingZone"))
        {
            mine.isTruckArrived = true;
        }
    }

    public void PlayLeavingAnimation()
    {
        truckIndex = 8;
        animation.SetTrigger("leave");
        isTruckGone = true;
    }

    void ReloadBoxes()
    {
        foreach (Transform box in StackTransform)
        {
            box.gameObject.SetActive(true);
        }
    }

    public IEnumerator TruckArriving()
    {
        while (true)
        {
            if (isTruckGone && isTruckNeeded)
            {
                yield return new WaitForSeconds(11f + ArrivingTime);
                ReloadBoxes();
                animation.Play("Arriving");
                Debug.Log(isTruckNeeded);

                isTruckGone = false;
                yield return null;
            }
            yield return null;
        }
    }

}
