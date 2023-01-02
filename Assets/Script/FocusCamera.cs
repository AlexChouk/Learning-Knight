using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    [SerializeField] private GameObject knight;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(knight.transform.position.x +10, transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(knight.transform.position.x +10, transform.position.y, -10);
    }
}
