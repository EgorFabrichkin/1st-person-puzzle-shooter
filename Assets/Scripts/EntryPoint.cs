#nullable enable

using GameCore.Players;
using UnityEngine;
using Utils;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerController player = null!;

    #region MONO
    
    private void Awake()
    {
        player.EnsureNotNull("Player not found");
    }

    #endregion
}
