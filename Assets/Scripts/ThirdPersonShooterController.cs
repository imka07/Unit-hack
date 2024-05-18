using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;


public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    private StarterAssetsInputs starterAssets;
    [SerializeField] private float normalSensetivity;
    [SerializeField] private float aimSensetivity;
    private ThirdPersonController thirdPersonController;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugPos;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float damage;

    [SerializeField] private Transform vfxHit;
    [SerializeField] private Transform vfxRicochet;

    private void Awake()
    {
        starterAssets = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
  
    }

    private void Update()
    {
        if (GameManager.instance.isGameActive)
        {
            Vector3 mouseWorldPos = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            Transform hitTransform = null;

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugPos.position = raycastHit.point;
                mouseWorldPos = raycastHit.point;
                hitTransform = raycastHit.transform;
            }

            if (starterAssets.aim)
            {
                cinemachineVirtual.gameObject.SetActive(true);
                thirdPersonController.SetSensetivity(aimSensetivity);
                thirdPersonController.SetRotationOnMove(false);
                anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

                Vector3 aimWorldPos = mouseWorldPos;
                aimWorldPos.y = transform.position.y;
                Vector3 aimDir = (aimWorldPos - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
            }
            else
            {
                cinemachineVirtual.gameObject.SetActive(false);
                thirdPersonController.SetRotationOnMove(true);
                thirdPersonController.SetSensetivity(normalSensetivity);
                anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            }

            if (starterAssets.shoot)
            {
                CinemachineShaker.Instance.Shaker(2f, 0.1f);
                if (hitTransform != null)
                {
                    if (hitTransform.GetComponent<BulletTarget>() != null)
                    {
                        hitTransform.GetComponent<MonsterController>().TakeDamage(damage);
                        Instantiate(vfxHit, debugPos.position, Quaternion.identity);
                        audioSource.clip = clips[1];
                        audioSource.Play();
                    }
                    else
                    {
                        Instantiate(vfxRicochet, debugPos.position, Quaternion.identity);
                        audioSource.clip = clips[0];
                        audioSource.Play();
                    }
                }
                starterAssets.shoot = false;
            }
        }
    }
}