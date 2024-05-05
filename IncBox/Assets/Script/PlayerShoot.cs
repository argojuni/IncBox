using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPerfab;

    public void shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AudioManager.Instance.PlaySFX("shoot");
            Instantiate(bulletPerfab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
