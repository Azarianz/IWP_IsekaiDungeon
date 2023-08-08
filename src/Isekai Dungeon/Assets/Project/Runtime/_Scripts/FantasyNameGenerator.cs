using UnityEngine;

public class FantasyNameGenerator : MonoBehaviour
{
    public string GenerateRandomName(string race)
    {
        switch (race)
        {
            case "Human":
                return GenerateHumanName();
            case "Elf":
                return GenerateElfName();
            case "Dwarf":
                return GenerateDwarfName();
            default:
                return null;
        }
    }

    public string GenerateHumanName()
    {
        // Lists of syllables to construct human names
        // Lists of syllables to construct human names
        string[] prefixes = { "Al", "Be", "Ced", "Dan", "Ed", "Fran", "Gil", "Hen", "Ian", "Jack", "Ken", "Liam", "Mike", "Neil", "Owen", "Paul", "Quin", "Ron", "Sam", "Tom", "Will", "Xan", "Yan", "Zan", "Alex", "Ben", "Chris", "Dean", "Evan", "Finn", "George" };
        string[] suffixes = { "a", "e", "i", "o", "u", "y", "son", "ley", "well", "ford", "ton", "man", "land", "wood", "shire", "son", "ford", "bell", "wood", "man", "ward", "son", "dale", "lock", "ville", "stone", "ridge", "wood", "brook", "burn" };

        // Randomly select a prefix and a suffix
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string suffix = suffixes[Random.Range(0, suffixes.Length)];

        // Combine the prefix and suffix to form the final name
        string name = prefix + suffix;
        Debug.Log(name);
        return name;
    }

    public string GenerateElfName()
    {
        string[] prefixes = { "Aer", "Calen", "Elor", "Fael", "Galad", "Ithil", "Lareth", "Mith", "Nen", "Quen", "Raen", "Sel", "Thal", "Van", "Yll", "Zel", "Bael", "Cael", "Dael", "Eld", "Faer", "Gael", "Hael", "Iriel", "Jael", "Kael", "Laer", "Mael", "Nael" };
        string[] suffixes = { "ara", "eth", "ion", "wyn", "ael", "orin", "lind", "thir", "endil", "iel", "anor", "beth", "lith", "mir", "nin", "riel", "nor", "en", "el", "dan", "wen", "ara", "lyn", "dir", "ran", "ion", "ael", "ron", "il" };
        
        // Randomly select a prefix and a suffix
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string suffix = suffixes[Random.Range(0, suffixes.Length)];

        // Combine the prefix and suffix to form the final name
        string name = prefix + suffix;
        Debug.Log(name);
        return name;
    }

    public string GenerateDwarfName()
    {
        // Lists of syllables to construct dwarven names
        string[] prefixes = { "Bor", "Dor", "Dur", "Gim", "Thra", "Thor", "Kor", "Mor", "Nor", "Oin", "Gloin", "Throin", "Bal", "Glo", "Dain", "Balin", "Fim", "Rur", "Nur", "Grum", "Brund", "Dval", "Gar", "Hund", "Kurg", "Orin", "Thrur", "Vond", "Buld", "Dvor" };
        string[] suffixes = { "in", "ar", "gar", "ur", "or", "li", "in", "rim", "ak", "grim", "in", "in", "ar", "il", "ak", "or", "dim", "ur", "rin", "urk", "bon", "dan", "in", "ron", "sun", "vun", "dun", "run", "zun", "ton" };

        // Randomly select a prefix and a suffix
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string suffix = suffixes[Random.Range(0, suffixes.Length)];

        // Combine the prefix and suffix to form the final name
        string name = prefix + suffix;
        Debug.Log(name);
        return name;
    }
}
