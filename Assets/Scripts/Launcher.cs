using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] RocketManager rm;
    [SerializeField] bool target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rm = GameObject.Find("RocketManager").GetComponent<RocketManager>();
        if(target)
            rm.AddTarget(gameObject);
        else
            rm.AddLauncher(gameObject);
    }
    private void OnDestroy()
    {
        if(target)
            rm.RemoveTarget(gameObject);
        else
            rm.RemoveLauncher(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
