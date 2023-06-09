using System.Collections.Generic;
using UnityEngine;

// Add to the player to hold the key
public class Keychain : MonoBehaviour
{
    List<string> m_KeyTypeOwned = new List<string>();

    public void GrabbedKey(string keyType)
    {
        m_KeyTypeOwned.Add(keyType);
    }

    public bool HaveKey(string keyType)
    {
        return m_KeyTypeOwned.Contains(keyType);
    }

    public void UseKey(string keyType)
    {
        m_KeyTypeOwned.Remove(keyType);
    }
}