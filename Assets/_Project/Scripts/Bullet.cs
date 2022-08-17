using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public int Damage;
    public float speed = 4;
    public Vector3 Position;
    public ParticleSystem psDeath;

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(speed * Time.deltaTime * Position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(psDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

/*

Ray ray = GameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
RaycastHit hit;
if (Physics.Raycast(ray, out hit, 800f, ~layer_mask))
{
    mouseWorldPosition = hit.point;
    Vector3 targetDirection = mouseWorldPosition - transform.position;
    // Debug.LogWarning(targetDirection);
    targetDirection.y = 0;
    Quaternion newRotation = Quaternion.LookRotation(targetDirection);
    rb.MoveRotation(newRotation);

}*/