using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
	[SerializeField] Animator animator;

	private Vector3 curpos; 
	private Vector3 lastpos;

    private void Update()
    {
        curpos = transform.position;
        float _speed = (Vector3.Magnitude(curpos - lastpos) / Time.deltaTime);
        lastpos = curpos;

        if (_speed >= 1)
        {
           animator.SetBool("ShouldMove", true);

        }
        else {
            animator.SetBool("ShouldMove", false);
        }
    }
}