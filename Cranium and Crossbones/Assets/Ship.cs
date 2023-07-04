using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_hull_integrity;
    [SerializeField] protected float m_sail_integrity;
    [SerializeField] protected int m_manpower;

    protected float max_hull, max_sail;
    protected int max_manpower;

    [SerializeField] private Sprite[] hull_sprites;
    [SerializeField] private Sprite[] sail_sprites;

    [SerializeField] private SpriteRenderer hull_renderer;
    [SerializeField] private SpriteRenderer sail_renderer;

    protected Dictionary<string, string> stats;

    // Start is called before the first frame update
    public virtual void Initialize(float speed, float hull, float sail, int manpower)
    {

        m_speed = speed;
        m_hull_integrity = hull;
        m_sail_integrity = sail;
        m_manpower = manpower;

        max_hull = m_hull_integrity;
        max_sail = m_sail_integrity;
        max_manpower = m_manpower;

        hull_renderer.sprite = hull_sprites[0];
        sail_renderer.sprite = sail_sprites[0];
    }

    public virtual void TakeHullDamage(int damage)
    {
        m_hull_integrity -= damage;

        Debug.Log("Hull %: " + m_hull_integrity / max_hull);

        if(m_hull_integrity/max_hull < 0.1)
        {
            hull_renderer.sprite = hull_sprites[2];
        }else if(m_hull_integrity / max_hull < 0.7)
        {
            hull_renderer.sprite = hull_sprites[1];
        }

        Debug.Log("Hull Damaged, Hull Integrity: " + m_hull_integrity.ToString());
    }

    public virtual void TakeSailDamage(int damage)
    {
        m_sail_integrity -= damage;

        if (m_sail_integrity / max_sail < 0.1)
        {
            sail_renderer.sprite = sail_sprites[2];
        }
        else if (m_sail_integrity / max_sail < 0.7)
        {
            sail_renderer.sprite = sail_sprites[1];
        }

        Debug.Log("Sail Damaged, Sail Integrity: " + m_sail_integrity.ToString());
    }

    public virtual void TakeManpowerDamage(int damage)
    {
        m_manpower -= (int)Mathf.Ceil(damage * (max_hull / m_hull_integrity));

        Debug.Log("Manpower Left: " + m_manpower);
    }

    public void LoadShipStats(string shiptype)
    {

        stats = JSONParser.ParseFromFile(shiptype+"ShipStats");

        Initialize(
            float.Parse(stats["speed"]),
            int.Parse(stats["hull"]),
            int.Parse(stats["sail"]),
            int.Parse(stats["manpower"])
            );
    }
}
