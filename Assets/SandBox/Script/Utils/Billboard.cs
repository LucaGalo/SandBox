using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool lockXZ;

    private void Update()
    {
        var target = Camera.main.transform.position;
        if (lockXZ)
            target = new Vector3(target.x, transform.position.y, target.z);

        transform.LookAt(target);
    }
}
