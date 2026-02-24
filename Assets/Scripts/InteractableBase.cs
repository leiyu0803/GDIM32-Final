using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("MainCamera"))
        {
            Locator.Player._interactItems.Add(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("MainCamera"))
        {
            Locator.Player._interactItems.Remove(gameObject);
        }
    }


    public abstract void Interact();


    /*[SerializeField] private LayerMask _lineOfSightLayers;
    //[SerializeField] private float _obstacleCheckDistance = 1.0f;
    //[SerializeField] private float _obstacleCheckRadius = 1.0f;
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private float _lineOfSightMaxDistance;
    [SerializeField] private Vector3 _raycastStartOffset;

    private string _playerTag = "Player";

    private Vector3 _raycastStart
    {
        get
        {
            return transform.TransformPoint(_raycastStartOffset);
        }
    }

    private Vector3 _raycastDir
    {
        get
        {
            return (Locator.Player.PlayerCenter - _raycastStart).normalized;
        }
    }
    private Vector3 _raycastHitLocation;
    private Vector3 _spherecastHitLocation;
    private bool _hasLineOfSightToPlayer;
    private Vector3 _meToPlayer;

    private void Update()
    {
        Debug.Log(IsPlayerLookingAt() && HasLineOfSightToPlayer());
    }

    private bool HasLineOfSightToPlayer()
    {
        _hasLineOfSightToPlayer = false;
        RaycastHit hitInfo;
        if (Physics.Raycast(_raycastStart, _raycastDir, out hitInfo, _lineOfSightMaxDistance, _lineOfSightLayers.value))
        {
            _raycastHitLocation = hitInfo.point;
            if (hitInfo.collider.gameObject.tag.Equals(_playerTag))
            {
                _hasLineOfSightToPlayer = true;
            }
        }

        return _hasLineOfSightToPlayer;
    }
    private bool IsPlayerLookingAt()
    {
        Vector3 playerPos = Locator.Player.transform.position;

        // 获取玩家朝向
        Vector3 playerForward = Locator.Player.transform.forward;
        // 获取从玩家指向目标的向量
        Vector3 playerToTarget = (transform.position - playerPos).normalized;

        // 使用点积判断玩家是否面向目标
        float dot = Vector3.Dot(playerToTarget, playerForward);

        // 点积大于等于 0 表示玩家面向目标（角度小于 90 度）
        return dot >= 0;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (_hasLineOfSightToPlayer)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(_raycastStart, _raycastDir * _lineOfSightMaxDistance);
        if (Locator.Player != null) Gizmos.DrawSphere(Locator.Player.PlayerCenter, 0.1f);
        Gizmos.DrawSphere(_raycastHitLocation, 0.1f);
    }*/
}
