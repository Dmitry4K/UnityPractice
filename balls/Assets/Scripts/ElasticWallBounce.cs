using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElasticWallBounce : MonoBehaviour
{
    [SerializeField]
    private Rigidbody obj;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collObj = collision.collider.gameObject;
        string name = collObj.name;
        if(name.Contains("wall"))
        {
            //Debug.Log($"collision with object: {name}");
            Vector3 newVelocityDirection = Vector3.Reflect(obj.velocity, collObj.transform.position).normalized;
            float velocityPrevStrength = obj.velocity.magnitude;
            obj.velocity = newVelocityDirection * velocityPrevStrength;
        }
    }
}
