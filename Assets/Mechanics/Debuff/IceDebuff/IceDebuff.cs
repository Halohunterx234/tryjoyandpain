using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : MonoBehaviour
{
    public buff buff_AI;
    
    // Start is called before the first frame update
    void Start()
    {
        buff_AI.init(this.gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {
        buff_AI.CD_Update();
    }
}
