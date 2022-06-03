
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Il y a d�j� un BuildManager dans la sc�ne!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefabs;


    public GameObject BuildEffect;


    private TurretBlueprint turretToBuild;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Pas assez d'argent pour cela");
            return;
        }

        PlayerStats.money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(BuildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        Debug.Log("L'objet a �t� achet�, il vous reste : " + PlayerStats.money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

}
