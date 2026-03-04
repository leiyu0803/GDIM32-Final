using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
	List<Transform> _moveInTargetLocations;
	List<Transform> _moveOutTargetLocations;

    public delegate void Arrived(NPCMovement self);
    public event Arrived onArrived;

    NavMeshAgent NavMeshAgent;

    void Start()
    {
        _moveInTargetLocations = PathManager.Instance._moveInTargetLocations;
        _moveOutTargetLocations = PathManager.Instance._moveOutTargetLocations;
        GameController.OnOrderCompleted += Moveout;
        NavMeshAgent = this.GetComponent<NavMeshAgent>();

        if (GameController.Instance != null)
        {
            GameController.Instance.RegisterNPC(this);
        }

        StartCoroutine(SetMoveInDestination());
    }

    private IEnumerator SetMoveInDestination()
    {
        foreach (Transform targetLocation in _moveInTargetLocations)
        {
            NavMeshAgent.SetDestination(targetLocation.position);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, targetLocation.position) < 0.5f);
        }
        NavMeshAgent.SetDestination(transform.position);
        onArrived?.Invoke(this);
        this.GetComponent<InteractableNPC>().Activate();
    }
    // Face Look At Target
    public void FaceTarget(Transform target)
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }

    private void Moveout()
    {
        StartCoroutine(SetMoveOutDestination());

    }
    private IEnumerator SetMoveOutDestination()
    {
        foreach (Transform targetLocation in _moveOutTargetLocations)
        {
            NavMeshAgent.SetDestination(targetLocation.position);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, targetLocation.position) < 0.5f);
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameController.OnOrderCompleted -= Moveout;
    }
}
