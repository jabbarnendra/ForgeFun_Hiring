using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private ConveyorBelt conveyorBelt;
    private List<GameObject> listObjectsOnBelt = new List<GameObject>();

    void Start()
    {
        if (conveyorBelt == null)
            conveyorBelt = transform.parent.GetComponent<ConveyorBelt>();
    }

    void Update()
    {
        if (conveyorBelt != null) {
            if (conveyorBelt.IsBeltActive())
            {
                for (int i = 0; i <= listObjectsOnBelt.Count - 1; i++)
                {
                    Rigidbody rigidbody = listObjectsOnBelt[i].GetComponent<Rigidbody>();
                    rigidbody.velocity = conveyorBelt.GetBeltMoveDirection();
                }
            }
            else
            {
                for (int i = 0; i <= listObjectsOnBelt.Count - 1; i++)
                {
                    Rigidbody rigidbody = listObjectsOnBelt[i].GetComponent<Rigidbody>();
                    rigidbody.constraints = RigidbodyConstraints.None;
                }
            }
        };
    }

    #region Public Function
    public void SetBeltLength(float length)
    {
        float beltWidth = 2;

        transform.localPosition = new Vector3(length / 2, 0, 0);
        transform.localScale = new Vector3(length, 1, beltWidth);
    }

    public void KnockAllObjects()
    {
        Vector3 knockVelocity = new Vector3(0, 0, 10);

        for (int i = 0; i <= listObjectsOnBelt.Count - 1; i++)
        {
            Rigidbody rigidbody = listObjectsOnBelt[i].GetComponent<Rigidbody>();
            rigidbody.velocity = knockVelocity;
            rigidbody.constraints = RigidbodyConstraints.None;
        }

        listObjectsOnBelt = new List<GameObject>();
    }
    #endregion

    #region Private Function
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (!listObjectsOnBelt.Contains(obj))
        {
            // Freeze Z rotation, so it wont spin around
            Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;

            listObjectsOnBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;

        listObjectsOnBelt.Remove(collision.gameObject);
    }
    #endregion
}