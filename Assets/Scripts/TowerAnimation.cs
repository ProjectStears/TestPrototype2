using UnityEngine;
using System.Collections;

public class TowerAnimation : MonoBehaviour
{
    private Animator animator;
    private float changetimer;

	// Use this for initialization
	void Start () {
	    animator = this.GetComponent<Animator>();
        animator.Play("Tower", -1, Random.Range(0.0f, 1.0f));
	    
        ChangeStuff();
    }
	
	// Update is called once per frame
	void Update () {
	    if (changetimer <= 0)
	    {
            ChangeStuff();
        }

	    changetimer -= Time.deltaTime;
	}

    void ChangeStuff()
    {
        animator.speed = Random.Range(0.7f, 1.3f);
        changetimer = Random.Range(10f, 20f);
    }
}
