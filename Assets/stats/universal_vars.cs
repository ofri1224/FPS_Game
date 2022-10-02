using UnityEngine;
[ExecuteAlways]
public class universal_vars : MonoBehaviour
{
    #region singleton
    public static universal_vars instance;
    private void Awake() {
        instance=this;
    }
    #endregion
    public float Gravity=20f;
    public LayerMask EntityLayer;
    public LayerMask ObstacleLayer;
    public LayerMask PickablesLayer;
    public LayerMask ShootableLayers;

    // public enum AmmoType {
    //     Pistol,
    //     rifle,
    //     Shotgun,
    //     sniper,
    //     special
    // }
}
