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

    private void Awake()
    {
        starterAssets = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugPos.position = raycastHit.point;
            mouseWorldPos = raycastHit.point;
        }

        if (starterAssets.aim)
        {
            cinemachineVirtual.gameObject.SetActive(true);
            thirdPersonController.SetSensetivity(aimSensetivity);
            thirdPersonController.SetRotationOnMove(false);
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
        }
    }
}