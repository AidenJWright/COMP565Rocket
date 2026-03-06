using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class RocketManager : MonoBehaviour
{
    List<GameObject> Lauchers;
    List<GameObject> Targets;
    List<GameObject> Rockets;

    [SerializeField] float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    public void AddLauncher(GameObject launcher)
    {
        Lauchers.Add(launcher);
    }
    public void RemoveLauncher(GameObject launcher)
    {
        Lauchers.Remove(launcher);
    }
    public void AddTarget(GameObject target)
    {
        Targets.Add(target);
    }
    public void RemoveTarget(GameObject target)
    {
        Targets.Remove(target);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
