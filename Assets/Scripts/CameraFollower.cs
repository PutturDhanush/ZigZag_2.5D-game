using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public GameObject player;
    Vector3 playerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        playerPosition = player.transform.position;
        offset = transform.position - playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        transform.position = offset + playerPosition;
    }
}
