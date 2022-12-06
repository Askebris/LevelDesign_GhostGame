using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplosiveBehaviour : MonoBehaviour
{
    float timerDetonate = 3f;
    float explosionRange = 5f;
    float explosionDamage = 10f;
    float explosionForce = 10f * 1000;
    [SerializeField] TextMeshPro timerText;
    [SerializeField] ParticleSystem explosionEffect;

    private void Awake()
    {
        DrawCircle(this.gameObject, explosionRange * 2, 0.05f);
    }

    private void Update()
    {
        timerDetonate -= Time.deltaTime;

        UpdateText();

        if (timerDetonate <= 0)
        {
            Detonate();
        }
    }
    public void Detonate()
    {
        print("boom");

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRange);

        foreach (Collider collider in colliders)
        {

            var damagableCol = collider.GetComponent<EnemyBehaviour>();

            if (damagableCol != null)
            {
                damagableCol.TakeDamage(explosionDamage);
            }

            var rigidbodyCol = collider.GetComponent<Rigidbody>();

            if (rigidbodyCol != null)
            {
                if (rigidbodyCol.tag == "Projectile") continue;
                rigidbodyCol.AddExplosionForce(explosionForce, gameObject.transform.position, explosionRange); //range can be set to 0 to be same amount of force regardless of distance to explosion
            }

        }

        Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);

    }

    //show overlapsphere in scenemode
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, explosionRange);
    }

    private void DrawCircle(GameObject gameObject, float radius, float lineWidth)
    {

        var segments = 360;
        var line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    private void UpdateText()
    {
        var timer = Mathf.Ceil(timerDetonate);
        timerText.text = timer.ToString();
    }
}
