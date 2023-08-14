using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossCultist : Cultist
{
    //behaviour states
    public bool isMoving;
    [Header("Shockwave")]
    [Range(0f, 20f)]
    public float minCD;
    [Range(0f, 20f)]
    public float maxCD;
    //cd to do a shockwave attack
    private float shockwaveCD;
    [Range(0f, 20f)]
    public float shockwaveSize;
    [Range(0f, 20f)]
    public float shockwaveGrowth;
    [Range(0f, 50f)]
    public float jumpStrength;

    //gameobject references
    public GameObject circle, innercircle, outercircle;

    //layermask
    public LayerMask playerLayer;

    private void Awake()
    {
        isMoving = true;
        shockwaveCD = generateCD(minCD, maxCD);
    }

    private void Update()
    {
        //1: Shockwave Attack
        if (CD >= shockwaveCD)
        {
            isMoving = false;
            CD = 0;
            StartCoroutine(Shockwave());
        }
        //2: Chase player
        if (isMoving)
        {
            Vector3 playerDir = player.transform.position - this.transform.position;
            rb.velocity = playerDir.normalized * moveSpeed;
            CD += Time.deltaTime;
        }
        //3: In the middle of shockwaving, continue to not do anything
        else
        {
            return;
        }

        if (this.transform.position.x > player.transform.position.x && this.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
        else if (this.transform.position.x < player.transform.position.x && this.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
    }
    
    //shockwave attack
    private IEnumerator Shockwave()
    {
        WaitForEndOfFrame W = new WaitForEndOfFrame();
        //Sign that the miniboss is jumping
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector2(0, 20);
        //start circle
        Vector3 playerPos = player.transform.position;
        float shockwaveMultipler = shockwaveGrowth * Time.deltaTime;
        GameObject warningcircle = Instantiate(circle, playerPos, Quaternion.identity);
        outercircle = warningcircle.GetComponentsInChildren<Transform>()[1].gameObject;
        innercircle = warningcircle.GetComponentsInChildren<Transform>()[2].gameObject;
        innercircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        outercircle.transform.localScale = new Vector3(shockwaveSize, shockwaveSize, shockwaveSize);
        while (innercircle.transform.localScale.x <= shockwaveSize + 0.2f)
        {
            innercircle.transform.localScale += new Vector3(shockwaveMultipler, shockwaveMultipler, shockwaveMultipler);
            yield return W;
        }
        print("landing");
        rb.velocity = Vector3.zero;
        StartCoroutine(Land(playerPos, warningcircle));
    }

    private IEnumerator Land(Vector3 playerPos, GameObject circle)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        transform.position = playerPos + Vector3.up * 20;
        float dist = 0,
            totalDist = Vector2.Distance(transform.position, playerPos);

        while (dist < totalDist)
        {
            float d = moveSpeed * Time.deltaTime * jumpStrength;
            transform.position = Vector3.MoveTowards(this.transform.position, playerPos, d);
            dist += d;
            yield return w;
        }
        //if player is within the circle
        isMoving = true;
        bool withincircle = Physics2D.OverlapCircle(transform.position, (shockwaveSize + 0.1f)/2, playerLayer);
        if (withincircle)
        {
            Entity entity = player.GetComponent<Entity>();
            entity.GetDamaged(3);
        }
        Destroy(circle);
    }
    //generate CD
    private float generateCD(float num1, float num2)
    {
        return (Random.Range(num1, num2));
    }
}
