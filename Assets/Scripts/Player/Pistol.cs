using System;
using TMPro;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private Camera camera;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClipSO shootSound;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float bulletRange;
    [SerializeField] private float maxAmmo;
    [SerializeField] private float reloadTime;
    private float currentReloadTime;
    [SerializeField] private TextMeshProUGUI reloadText;
    private float ammo;
    

    private void Start()
    {
        ammo = maxAmmo;
        currentReloadTime = 0;
        reloadText.gameObject.SetActive(false);
        InputManager.Instance.OnAttackPerformed += InstanceOnOnAttackPerformed;
    }
    private void InstanceOnOnAttackPerformed(object sender, EventArgs e)
    {
        if (Player.Instance.canMove)
        {
            Shoot();
        }
    }

    private void HandleReload()
    {
        if (ammo > 0) return;
        reloadText.gameObject.SetActive(true);
        if (currentReloadTime >= reloadTime)
        {
            ammo = maxAmmo;
            reloadText.gameObject.SetActive(false);
            currentReloadTime = 0;
        }
        currentReloadTime += Time.deltaTime;
    }

    private void Update()
    {
        HandlePosition();
        HandleReload();
    }

    private void HandlePosition()
    {
        Vector2 direction = (camera.ScreenToWorldPoint(Input.mousePosition) - pivot.position);
        Vector3 delta = direction.normalized * distance;
        transform.position = pivot.position + (delta);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        spriteRenderer.flipY = direction.x < 0;
    }

    private void Shoot()
    {
        if (ammo <= 0) return;
        ammo--;
        spriteRenderer.GetComponent<Animator>().Play("shoot", 0, 0f);
        SoundManager.Instance.SpawnTempSoundSourceAtWorldSpacePoint(transform.position, shootSound.GetRandomAudioClipReference());
        
        Vector2 direction = (camera.ScreenToWorldPoint(Input.mousePosition) - pivot.position);

        Ray2D ray = new Ray2D(transform.position, direction.normalized);
        RaycastHit2D hit= Physics2D.Raycast(transform.position, direction.normalized, bulletRange, targetLayer);
        if (hit)
        {
            hit.collider.TryGetComponent<Enemy>(out Enemy enemy);
            enemy.TakeDamage(damage);
        }
    }
}
