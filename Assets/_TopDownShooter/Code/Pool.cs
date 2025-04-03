using System;
using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> objectList;

    private void Start()
    {
        AddObjectToPool(poolSize);
    }
    private void AddObjectToPool(int amount)
    {
        for (int index = 0; index < amount; index++)
        {
            GameObject actualObj = Instantiate(prefab);
            actualObj.SetActive(false);
            objectList.Add(actualObj);
            actualObj.transform.parent = transform;
        }

    }

    public GameObject RequestObject()
    {
        for (int index = 0; index < objectList.Count; index++)
        {
            if (!objectList[index].activeSelf)
            {
                objectList[index].SetActive(true);
                return objectList[index];
            }
        }
        AddObjectToPool(1);
        objectList[objectList.Count - 1].SetActive(true);
        return objectList[objectList.Count - 1];
    }

    public void TurnOffAllObjects()
    {
        for (int index = 0; index < objectList.Count; index++)
        {
            objectList[index].SetActive(false);
        }
    }

    public bool CheckForActiveObj()
    {
        for (int index = 0; index < objectList.Count; index++)
        {
            if (objectList[index].activeSelf) return false;
            Debug.Log("There Is an Object Active");
        }
        Debug.Log("There Is Not an Object Active");
        return true;
    }
}   
