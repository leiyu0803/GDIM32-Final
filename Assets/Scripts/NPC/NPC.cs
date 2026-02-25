using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceTest : MonoBehaviour
{
    public float moveDuration = 1f;

    private List<TransformStep> steps = new List<TransformStep>();
    [Header("Debug")]
    [Space]
    [SerializeField] int x_rot;
    [Space]
    [Header("Attributes")]
    [SerializeField] Animator _animator;
    void Start()
    {
        transform.position = new Vector3(-6.62099981f, 0.101999998f, 1.78400004f);
        // 初始化步骤
        steps.Add(new TransformStep(
            new Vector3(-6.62099981f, 0.101999998f, 1.78400004f),
            new Vector3(x_rot, 180, 0)));

        steps.Add(new TransformStep(
            new Vector3(-6.62099981f, 0.101999998f, 0.904999971f),
            null));

        steps.Add(new TransformStep(
            null,
            new Vector3(x_rot, 90, 0)));

        steps.Add(new TransformStep(
            new Vector3(-3.898f, 0.101999998f, 0.904999971f),
            null));

        steps.Add(new TransformStep(
            null,
            new Vector3(x_rot, 180, 0)));

        steps.Add(new TransformStep(
            new Vector3(-3.33699989f, 0.101999998f, -0.141000003f),
            null));

        steps.Add(new TransformStep(
            new Vector3(-3.06999993f, 0.101999998f, -0.828999996f),
            new Vector3(x_rot, 157.234085f, 0)));

        steps.Add(new TransformStep(
            new Vector3(0.167999998f, 0.101999998f, -0.661000013f),
            new Vector3(x_rot, 90, 0)));

        steps.Add(new TransformStep(
            null,
            new Vector3(x_rot, 0, 0)));

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        foreach (var step in steps)
        {
            yield return StartCoroutine(MoveTo(step));
        }
    }

    IEnumerator MoveTo(TransformStep step)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 targetPos = step.position ?? startPos;
        Quaternion targetRot = step.rotation != null
            ? Quaternion.Euler(step.rotation.Value)
            : startRot;

        bool willMove = startPos != targetPos;

        // 如果真的会移动
        if (willMove)
            _animator.SetBool("ShouldMove", true);

        float time = 0;

        while (time < moveDuration)
        {
            float t = time / moveDuration;

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;

        // 移动结束
        if (willMove)
            _animator.SetBool("ShouldMove", false);
    }
    private void Update()
    {
        Debug.Log(_animator.GetBool("ShouldMove"));
    }
}

public class TransformStep
{
    public Vector3? position;
    public Vector3? rotation;

    public TransformStep(Vector3? pos, Vector3? rot)
    {
        position = pos;
        rotation = rot;
    }
}