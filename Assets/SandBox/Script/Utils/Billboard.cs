using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool lockXZ;

    private void OnEnable()
    {
        LookPlayer();
    }

    private void Update()
    {
        LookPlayer();
    }

    void LookPlayer()
    {
        var target = Camera.main.transform.position;
        if (lockXZ)
            target = new Vector3(target.x, transform.position.y, target.z);

        transform.LookAt(target);
    }
}
