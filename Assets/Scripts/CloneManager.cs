//
#if UNITY_EDITOR
using UnityEngine;
using ParrelSync;
using Mirror;

public class CloneManager : MonoBehaviour
{
    public bool isAutoStart;
    public NetworkRoomManager _networkManager;
    // Start is called before the first frame update
    void Start()
    {
        if (!isAutoStart) return;

        if (ClonesManager.IsClone()) {
            _networkManager.StartClient();
        } else {
            _networkManager.StartHost();
        }
    }
}
#endif