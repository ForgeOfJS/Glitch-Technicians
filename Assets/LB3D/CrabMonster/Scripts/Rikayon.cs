using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rikayon : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            animator.SetTrigger("Die");
        }
		//Randomize attacks with 5 available attacks
		if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetTrigger("Attack_1");
        }
		if (Input.GetKeyDown(KeyCode.R)) {
            animator.SetTrigger("Attack_2");
        }
		if (Input.GetKeyDown(KeyCode.F)) {
            animator.SetTrigger("Attack_3");
        }
		if (Input.GetKeyDown(KeyCode.G)) {
            animator.SetTrigger("Attack_4");
        }
		//wandering walk
		if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger("Walk_Cycle_2");
        }
		//chasing run
		if (Input.GetKeyDown(KeyCode.V)) {
            animator.SetTrigger("Walk_Cycle_1");
        }
		//maybe? Seems hard to stop animator to take damage.
		if (Input.GetKeyDown(KeyCode.C)) {
            animator.SetTrigger("Take_Damage_1");
        }


	}
}
