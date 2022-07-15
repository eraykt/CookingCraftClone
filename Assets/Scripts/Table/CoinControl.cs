using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    bool isHittedToPlayer;

    public bool move { get; set; }

    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        if (move)
            MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + (Vector3.up / 2), GameManager.Instance.coinspeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, GameManager.Instance.coinspeed * Time.deltaTime);
       
        if (isHittedToPlayer)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHittedToPlayer = true;
        }
    }
}
