using UnityEngine;
using Alteruna;
using Alteruna.Trinity;
using System;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] avatarPrefabs; // Array of avatar prefabs
    public Transform spawnPoint; // Spawn point for the avatar

    private void Start()
    {
        if (Multiplayer.Instance != null)
        {
            // Register the SelectAvatarPrefab method as a remote procedure
            Multiplayer.Instance.RegisterRemoteProcedure("SelectAvatarPrefab", (ushort fromUser, ProcedureParameters parameters, uint callID, ITransportStreamReader processor) => SelectAvatarPrefab(fromUser, parameters));
            Debug.Log("SelectAvatarPrefab registered as a remote procedure."); // Debug comment: Indicates successful registration of the remote procedure

            // Select the avatar prefab for the local user
            SelectAvatarPrefab(Multiplayer.Instance.Me.Index, null);

            // Check if the local user is the host
            if (Multiplayer.Instance.Me.IsHost)
            {
                Debug.Log("I'm the host."); // Debug comment: Indicates that the local user is the host
                Multiplayer.Instance.RegisterRemoteProcedure("PlayRemote", (ushort fromUser, ProcedureParameters parameters, uint callID, ITransportStreamReader processor) => PlayRemote(fromUser, parameters, callID, processor));
            }
        }
        else
        {
            Debug.LogWarning("Multiplayer.Instance is null.");
        }
    }

    private void SelectAvatarPrefab(ushort fromUser, ProcedureParameters parameters)
    {
        Debug.Log("SelectAvatarPrefab called with fromUser: " + fromUser);

        if (Multiplayer.Instance == null)
        {
            Debug.LogError("Multiplayer.Instance is null in SelectAvatarPrefab.");
            return;
        }

        User user = Multiplayer.Instance.GetUser(fromUser);
        if (user == null)
        {
            Debug.LogError("User is null for fromUser: " + fromUser);
            return;
        }

        int prefabIndex = user.Index % avatarPrefabs.Length; // Calculate prefab index based on user index

        if (prefabIndex >= 0 && prefabIndex < avatarPrefabs.Length)
        {
            GameObject avatarPrefab = avatarPrefabs[prefabIndex];
            InstantiateAvatar(avatarPrefab);
            Debug.Log($"Selected avatar prefab index: {prefabIndex} for user {user.Name}."); // Debug comment: Indicates the selected avatar prefab index
        }
        else
        {
            Debug.LogWarning("Invalid prefab index."); // Debug comment: Warns if the prefab index is invalid
        }
    }

    private void InstantiateAvatar(GameObject avatarPrefab)
    {
        // Instantiate the avatar prefab at the spawn point
        GameObject avatar = Instantiate(avatarPrefab, spawnPoint.position, spawnPoint.rotation);

        // Optionally, you can parent the avatar to the AvatarController or any other GameObject
        avatar.transform.parent = transform;
    }

    private void PlayRemote(ushort fromUser, ProcedureParameters parameters, uint callID, ITransportStreamReader processor)
    {
        if (Multiplayer.Instance.Me.IsHost)
        {
            // Do host-specific actions
            Debug.Log("I'm the host."); // Debug comment: Indicates that the local user is the host
        }
        else
        {
            // Do client-specific actions
            Debug.Log("I'm not the host."); // Debug comment: Indicates that the local user is not the host
        }
    }
}
