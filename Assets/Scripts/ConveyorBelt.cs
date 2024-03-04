using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Transform boxPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float length;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private bool isBeltActive;

    void Awake()
    {
        InitBeltLength();
    }

    #region Public Function
    public void SpawnBox()
    {
        Instantiate(boxPrefab);
        boxPrefab.position = spawnPoint.transform.position;
    }

    public void TurnOnOrOffBelt()
    {
        isBeltActive = !isBeltActive;
    }

    public void Termination()
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.layer != LayerMask.NameToLayer("Belt"))
                continue;

            Belt belt = child.GetComponent<Belt>();
            belt.KnockAllObjects();
        }
    }

    public bool IsBeltActive()
    {
        return isBeltActive;
    }

    public Vector3 GetBeltMoveDirection()
    {
        return speed * direction;
    }
    #endregion

    #region Private Function
    private void InitBeltLength()
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.layer != LayerMask.NameToLayer("Belt"))
                continue;

            Belt belt = child.GetComponent<Belt>();
            belt.SetBeltLength(length);
        }
    }
    #endregion
}