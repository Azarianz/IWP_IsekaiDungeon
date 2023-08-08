using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class Unit_Viewer : MonoBehaviour
    {
        //Main Display
        public TextMeshProUGUI atk, def, spd, lvl, hp, mp, uname;
        public Image Char_Image, item1, item2, skill1, skill2, skill3;
        public GameObject stars, unitstat_window;
        public Slider expbar;

        //Full Stat Display
        public TextMeshProUGUI stat_health, stat_mana, stat_hregen, stat_mregen, stat_atk,
            stat_def, stat_spd, stat_acc, stat_crit, stat_melee, stat_range, stat_magic,
            stat_element, stat_fresist, stat_wresist, stat_eresist, stat_lresist, stat_debuff, stat_death;
        
        public void QuickDisplay(Agent_Data data)
        {
            CharacterStats stats = data.GetAgentStats();

            atk.text = "ATK \n" + stats.STAT_DAMAGE.ToString();
            def.text = "DEF \n" + stats.STAT_DEFENSE.ToString();
            spd.text = "SPD \n" + stats.STAT_SPEED.ToString();
            lvl.text = data.GetAgentLevel().ToString();
            hp.text = stats.STAT_MAXHEALTH.ToString();
            mp.text = stats.STAT_MAXMANA.ToString();
            uname.text = data.unit_Name;

            Char_Image.sprite = data.unit_icon;

            int star_rating = data.awaken_level;

            for (int s = 0; s < 5; s++)
            {
                if (s < star_rating)
                {
                    //Active Stars
                    stars.transform.GetChild(s).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    //Nonactive stars
                    stars.transform.GetChild(s).GetChild(0).gameObject.SetActive(false);
                }
            }

            LevelSystem levelSystem = data.GetLevelSystem();
            expbar.maxValue = levelSystem.GetXPToNextLevel();
            expbar.value = levelSystem.GetXP();

            fullStat(data);
        }

        public void fullStat(Agent_Data data)
        {
            CharacterStats stats = data.GetAgentStats();

            Debug.Log("Full Stats");
            stat_health.text = "Health: " + stats.STAT_MAXHEALTH.ToString();
            stat_mana.text = "Mana: " + stats.STAT_MAXMANA.ToString();
            stat_hregen.text = "Health Regen: " + stats.STAT_HEALTHREGEN.ToString() + "%";
            stat_mregen.text = "Mana Regen: " + stats.STAT_MANAREGEN.ToString() + "%";
            stat_atk.text = "Attack: " + stats.STAT_DAMAGE.ToString();
            stat_def.text = "Defense: " + stats.STAT_DEFENSE.ToString();
            stat_spd.text = "Speed: " + stats.STAT_SPEED.ToString();
            stat_acc.text = "Accuracy: " + stats.STAT_ACCURACY.ToString() + "%";
            stat_crit.text = "Crit Chance: " + stats.STAT_CRITCHANCE.ToString() + "%";
            stat_melee.text = "Melee Proficiency: " + stats.STAT_MELEEPROFICIENCY.ToString() + "%";
            stat_range.text = "Range Proficiency: " + stats.STAT_RANGEPROFICIENCY.ToString() + "%";
            stat_magic.text = "Magic Proficiency: " + stats.STAT_MAGICPROFICIENCY.ToString() + "%";
            stat_element.text = "Attack Element: " + stats.ATTACK_ELEMENT.ToString() + "%";
            stat_fresist.text = "Fire Resistance: " + stats.STAT_FIRE_RESISTANCE.ToString() + "%";
            stat_wresist.text = "Water Resistance: " + stats.STAT_WATER_RESISTANCE.ToString() + "%";
            stat_eresist.text = "Earth Resistance: " + stats.STAT_EARTH_RESISTANCE.ToString() + "%";
            stat_lresist.text = "Lightning Resistance: " + stats.STAT_LIGHTNING_RESISTANCE.ToString() + "%";
            stat_debuff.text = "Debuff Resistance: " + stats.STAT_DEBUFF_RESISTANCE.ToString() + "%";
            stat_death.text = "Death Resistance: " + stats.STAT_DEATH_RESISTANCE.ToString() + "%";
        }

        public void ToggleShow(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        public void ShowObj(GameObject obj)
        {
            obj.SetActive(true);
        }

        public void HideObj(GameObject obj)
        {
            obj.SetActive(false);
        }

    }
}
