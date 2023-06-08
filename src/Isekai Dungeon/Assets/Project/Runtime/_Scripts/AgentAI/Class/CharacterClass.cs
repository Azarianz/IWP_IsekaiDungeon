using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass : ScriptableObject
{
    public string className;
    public List<Ability> abilities;
}
