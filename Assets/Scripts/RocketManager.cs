using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoBehaviour
{
    List<GameObject> Launchers;
    List<GameObject> Targets;
    List<GameObject> Rockets;
    [SerializeField] GameObject RocketPrefab; 
    [SerializeField] float timer;
    float time;
    [SerializeField] float flightHeight;
    bool _coroutineRunning = false;
    void Awake()
    {
        time = timer;  
        Launchers = new List<GameObject>();
        Targets = new List<GameObject>();
        Rockets = new List<GameObject>();
    }
    public void AddLauncher(GameObject launcher)
    {
        float alignment = Vector3.Dot(launcher.transform.up, Vector3.up);
        if (alignment > 0.9f)
        {
            Launchers.Add(launcher);
        }
        else
        {
            Destroy(launcher);
        }
    }
    public void RemoveLauncher(GameObject launcher)
    {
        Launchers.Remove(launcher);
    }
    public void AddTarget(GameObject target)
    {
        Targets.Add(target);
    }
    public void RemoveTarget(GameObject target)
    {
        Targets.Remove(target);
    }
    public void RemoveRocket(GameObject rocket)
    {
        Rockets.Remove(rocket);
    }
    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            time = timer;
            if (!_coroutineRunning)
                StartCoroutine(FireRockets());
        }
        time -= Time.deltaTime;
    }
    //void FireRockets()
    //{
    //    foreach(GameObject launcher in Lauchers)
    //    {
    //        foreach (GameObject target in Targets)
    //        {
    //            GameObject rocket = Instantiate(RocketPrefab, launcher.transform.position, Quaternion.identity);
    //            Rockets.Add(rocket);
    //            FlightPath fp = rocket.GetComponent<FlightPath>();
    //            fp.rm = this;
    //            fp.flightHeight = launcher.transform.position + Vector3.up;
    //            fp.target = target.transform.position;
    //        }
    //    }
    //}
    IEnumerator FireRockets()
    {
        _coroutineRunning = true;

        // Snapshot the lists at the start of this firing sequence
        List<GameObject> launchers = new List<GameObject>(Launchers);
        List<GameObject> targets = new List<GameObject>(Targets);
        foreach (GameObject target in targets)
        {
            // Skip if this target was removed mid-sequence
            if (!Targets.Contains(target)) continue;
            foreach (GameObject launcher in launchers)
            {
                // Skip if this launcher was removed mid-sequence
                if (!Launchers.Contains(launcher)) continue;



                GameObject rocket = Instantiate(RocketPrefab, launcher.transform.position, Quaternion.identity);
                Rockets.Add(rocket);
                FlightPath fp = rocket.GetComponent<FlightPath>();
                fp.rm = this;
                fp.flightHeight = launcher.transform.position + Vector3.up;
                fp.target = target.transform.position;

            }
            yield return new WaitForSeconds(0.5f);
        }

        _coroutineRunning = false;
    }
}
