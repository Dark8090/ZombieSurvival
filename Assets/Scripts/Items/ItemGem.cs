using UnityEngine;

public class ItemGem : PickupItem
{
    private Transform _target;

    private float DistanceToTarget
    {
        get
        {
            if (_target != null)
            {
                return Vector3.Distance(transform.position, _target.position);
            }
            return float.MaxValue;
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            if (DistanceToTarget <= 0.5f)
            {
                Use();
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.fixedDeltaTime * 10f);
        }
    }

    public override void Use()
    {
        GameManager.Instance.PlayerStats.AddExperience(PickupsItemData.Count);
        base.Use();
    }

    public void SetTarget(Transform target) => _target = target;



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Use();
    //    Destroy(gameObject);
    //}
}
