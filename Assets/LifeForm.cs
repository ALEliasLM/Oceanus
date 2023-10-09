using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class LifeForm : MonoBehaviour
{
    // Start is called before the first frame update
    public LifeformInfo.Lifeform LifeformType;
    
    public GameObject ScanEffect;
    public bool alreadyScanned = false;
    public const float scanNeed = 3;
    public float scanProgress = 0;
    public bool onScan;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        alreadyScanned = LifeformInfo.scanInfo[(int)LifeformType];
        if(onScan && !alreadyScanned)
        {
            ScanEffect?.SetActive(true);
            scanProgress += Time.deltaTime;
            if(scanProgress >= scanNeed )
            {
                alreadyScanned = true;
                AI_Spectrum.LaraTalkShow(LifeformInfo.Info[(int)LifeformType,1]);
                LifeformInfo.scanInfo[(int)LifeformType] = true;
            }
        }else
            ScanEffect?.SetActive(false) ;
    }
}

public static class LifeformInfo
{
    public enum Lifeform{
        ClownFish,
        EmperorAngelfish,
        SeaTurtle,
        WhaleShark,
        HammerheadShark
    }
    
    public static string[,] Info =
    {
        {"Clownfish", "analysis completed: Clownfish (Amphiprioninae) are small, brightly colored marine fish known for their symbiotic relationship with sea anemones. They exhibit protandrous hermaphroditism, changing their sex from male to female as needed. Clownfish primarily feed on zooplankton and small invertebrates and are not considered dangerous to us."},
        {"Emperor Angel fish", "analysis completed: The Emperor Angelfish (Pomacanthus imperator) is a striking marine species known for its vibrant blue and yellow coloration and distinctive pattern of concentric circles. These fish are herbivores, primarily feeding on sponges, algae, and small invertebrates. While they are not inherently dangerous to humans, they can become territorial and aggressive towards other fish in captivity, requiring carefullness "},
        {"Sea turtle", "analysis completed: Sea turtles (superfamily Chelonioidea) are marine reptiles characterized by their streamlined bodies, flipper-like limbs, and a bony shell covered in scales. They are primarily herbivorous, consuming seagrasses and algae, but some species also eat other simples lifeforms and small invertebrates. Sea turtles pose no direct danger;"},
        {"Whale Shark", "analysis completed: The whale shark (Rhincodon typus), with an average length of about 12 meters (40 feet) and a distinctive pattern of pale spots and stripes on its bluish-gray skin. This filter-feeding species primarily consumes plankton and small fish by swimming with its mouth wide open, filtering prey from the water. Despite its massive size, the whale shark is generally considered harmless, as it is a gentle, slow-moving filter feeder with no aggressive tendencies."},
        {"Hammerhead Shark", "analysis completed: The Hammerhead shark (Sphyrnidae) is characterized by its distinctive, flattened head with lateral extensions called cephalofoils, which provide enhanced sensory perception. It primarily preys on a diet of fish, rays, and cephalopods. While the Hammerhead's unique morphology makes it an efficient predator, it poses minimal danger."}
    };


    public static bool[] scanInfo =
    {
        false,false,false,false,false
    };
}
