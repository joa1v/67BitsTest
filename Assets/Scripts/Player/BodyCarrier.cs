using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCarrier : MonoBehaviour
{
    [SerializeField] private Transform _startBodyPosition;
    [SerializeField] private float _bodiesYPositionOffset;
    [Header("Inertia Values")]
    [SerializeField, Tooltip("Use values between 0.1 and 0.75")] private Vector2 _inertiaRateRange;

    private int _bodyCarryCapacity = 1;
    private Vector3 _positionToPlaceBody;
    private List<Transform> _bodies = new List<Transform>();

    public delegate void BodiesBeingCarryEventHandler(int bodyCount);
    public event BodiesBeingCarryEventHandler OnBodiesChanged;

    public int BodyCarryCapacity { get => _bodyCarryCapacity; set => _bodyCarryCapacity = value; }
    public int BodyCount => _bodies.Count;

    private void Start()
    {
        _positionToPlaceBody = Vector3.zero;
    }

    private void FixedUpdate()
    {
        SetBodyRotations();
        SetBodyPositions();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isNpc = collision.gameObject.tag == "NPCBodyPart";
        NPC npc = null;

        if (isNpc)
        {
            npc = collision.transform.GetComponentInParent<NPC>();
        }

        if (npc && npc.IsKnockedDown && !npc.IsBeingCarried && _bodies.Count < _bodyCarryCapacity)
        {
            npc.DisableRagdoll();
            AddBody(npc.transform);
            npc.IsBeingCarried = true;
        }
    }

    private void AddBody(Transform body)
    {
        _bodies.Add(body);
        OnBodiesChanged?.Invoke(_bodies.Count);
        body.position = _startBodyPosition.position + _positionToPlaceBody;
        _positionToPlaceBody.y += _bodiesYPositionOffset;
    }

    public void DropBodies(Transform dropArea)
    {
        foreach (var body in _bodies)
        {
            body.transform.parent = null;
            body.position = dropArea.position;
            _positionToPlaceBody.y -= _bodiesYPositionOffset;
        }

        _bodies.Clear();
        OnBodiesChanged?.Invoke(0);
    }

    private void SetBodyPositions()
    {
        if (_bodies.Count > 0)
        {
            _bodies[0].position = Vector3.Lerp(_bodies[0].position, _startBodyPosition.position + (_startBodyPosition.up * _bodiesYPositionOffset), _inertiaRateRange.x);
        }

        for (int i = 1; i < _bodies.Count; i++)
        {
            float rate = Mathf.Lerp(_inertiaRateRange.x, _inertiaRateRange.y, i / _positionToPlaceBody.y);
            _bodies[i].position = Vector3.Lerp(_bodies[i].position, _bodies[i - 1].position + (_bodies[i - 1].up * _bodiesYPositionOffset), rate);
        }
    }

    private void SetBodyRotations()
    {
        for (int i = 1; i < _bodies.Count; i++)
        {
            float rate = Mathf.Lerp(_inertiaRateRange.x, _inertiaRateRange.y, i / _positionToPlaceBody.y);

            Vector3 currentPos = _bodies[i].position;
            Vector3 firstPos = _bodies[i - 1].position;

            Vector3 dist = firstPos - currentPos;

            dist.Normalize();

            float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle + 90);
            _bodies[i].rotation = Quaternion.Lerp(_bodies[i].rotation, rotation, rate);
        }
    }
}
