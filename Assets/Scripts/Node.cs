using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color notEnoughMoneyColor;
    public Color hoverColor;
    private Color startColor;
    public Vector3 positionOffset;
    public GameObject turret;
    private Renderer rend;

    private BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }


        if(turret != null)
        {
            Debug.Log("Impossible de construire ici, il y a déjà une tourelle");
            return;
        }

        buildManager.BuildTurretOn(this);

        Vector3 position = new Vector3(0, 1, 0);
        Vector3 rotation = new Vector3(90, 0, 0);
        turret.transform.position += position;
        turret.transform.localEulerAngles = rotation;

    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }

        if (buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
