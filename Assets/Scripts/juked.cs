using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class juked : MonoBehaviour
{
    public GameObject DBorDL;
    private bool gotup = false;
    public int getupTime;
    public float speed;
    public Vector3 direction;
    private Vector3 endpos;
    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(getupTime);
        gotup = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f),transform.position.y + UnityEngine.Random.Range(-1f, 1f),0);
        transform.position = Vector3.MoveTowards(transform.position, direction, Time.deltaTime * 1);
        if(gotup == true)
        {
            GameObject go = Instantiate(DBorDL) as GameObject;
            go.transform.position = transform.position;
            go.name = this.name;
            Destroy(gameObject);
        }
    }
}
 