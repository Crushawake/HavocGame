using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public enum Direction { Right, Left, Up, Down }; //right default. Direction something everything uses, so putting it here for all to use.

public class GameController : MonoBehaviour{

    public static GameController GCInst;

    private GameSaveData m_GameData;
    private GameOptionsData m_GameOptions;

    void Awake()
    {
        if (GCInst == null)
        {
            GCInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (GCInst != this) Destroy(gameObject);

        if (File.Exists(Application.persistentDataPath + "/options.dat"))
        {
            LoadOptions();
        }
        else                        //defaults
        {
            m_GameOptions.KbdControls.BasicAttackInput = KeyCode.Mouse0;
            m_GameOptions.KbdControls.Special1Input = KeyCode.Mouse1;
            m_GameOptions.KbdControls.Special2Input = KeyCode.Q;
            m_GameOptions.KbdControls.Special3Input = KeyCode.E;
            m_GameOptions.KbdControls.UpKeyInput = KeyCode.W;
            m_GameOptions.KbdControls.DownKeyInput = KeyCode.S;
            m_GameOptions.KbdControls.LeftKeyInput = KeyCode.A;
            m_GameOptions.KbdControls.RightKeyInput = KeyCode.D;
            m_GameOptions.KbdControls.PauseInput = KeyCode.Return;
            m_GameOptions.KbdControls.InventoryInput = KeyCode.Tab;
            
            m_GameOptions.ContControls.BasicAttackInput = KeyCode.Joystick1Button0;
            m_GameOptions.ContControls.Special1Input = KeyCode.Joystick1Button1;
            m_GameOptions.ContControls.Special2Input = KeyCode.Joystick1Button2;
            m_GameOptions.ContControls.Special3Input = KeyCode.Joystick1Button3;
            m_GameOptions.ContControls.UpKeyInput = KeyCode.Joystick1Button12;
            m_GameOptions.ContControls.DownKeyInput = KeyCode.Joystick1Button13;
            m_GameOptions.ContControls.LeftKeyInput = KeyCode.Joystick1Button14;
            m_GameOptions.ContControls.RightKeyInput = KeyCode.Joystick1Button15;
            m_GameOptions.ContControls.PauseInput = KeyCode.Joystick1Button9;
            m_GameOptions.ContControls.InventoryInput = KeyCode.Joystick1Button8;

            m_GameOptions.MasterVolume = 1.0f;
            m_GameOptions.SoundsVolume = 1.0f;
            m_GameOptions.MusicVolume = 1.0f;

            SaveOptions();
        }
    }

    void Update()
    {

    }

    public void LoadSave(int savenum)
    {
        if (File.Exists(Application.persistentDataPath + "/save" + savenum + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save" + savenum + ".dat", FileMode.Open);
            m_GameData = (GameSaveData)bf.Deserialize(file);
            file.Close();
        }
    }

   public void SaveFile(int savenum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save" + savenum + ".dat");
        bf.Serialize(file, m_GameData);
        file.Close();
    }
    
    public void LoadOptions()
    {
        if (File.Exists(Application.persistentDataPath + "/options.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.dat", FileMode.Open);
            m_GameOptions = (GameOptionsData)bf.Deserialize(file);
            file.Close();
        }
    }

    public void SaveOptions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +"/options.dat");
        bf.Serialize(file, m_GameData);
        file.Close();
    }

    public ControlScheme GetKbdControlScheme()
    {
        return m_GameOptions.KbdControls;
    }
    public ControlScheme GetContControlScheme()
    {
        return m_GameOptions.ContControls;
    }

    public void SetMasterVolume(float vol)
    {
        if (vol > 1.0f) m_GameOptions.MasterVolume = 1.0f;
        else if (vol < 0.0f) m_GameOptions.MasterVolume = 0.0f;
        else m_GameOptions.MasterVolume = vol;
    }

    public void SetSoundVolume(float vol)
    {
        if (vol > 1.0f) m_GameOptions.SoundsVolume = 1.0f;
        else if (vol < 0.0f) m_GameOptions.SoundsVolume = 0.0f;
        else m_GameOptions.SoundsVolume = vol;
    }

    public void SetMusicVolume(float vol)
    {
        if (vol > 1.0f) m_GameOptions.MusicVolume = 1.0f;
        else if (vol < 0.0f) m_GameOptions.MusicVolume = 0.0f;
        else m_GameOptions.MusicVolume = vol;
    }
}

public class ControlScheme
{
    public KeyCode UpKeyInput;                          //Key for "Up"
    public KeyCode DownKeyInput;                        //Key for "Down"
    public KeyCode LeftKeyInput;                        //Key for "Left"
    public KeyCode RightKeyInput;                       //Key for "Right"
    public KeyCode BasicAttackInput;                    //Key for basic attack
    public KeyCode Special1Input;                       //Key for first special attack
    public KeyCode Special2Input;                       //Key for second special attack
    public KeyCode Special3Input;                       //Key for third special attack
    public KeyCode PauseInput;                         //Key to bring up the pause screen
    public KeyCode InventoryInput;                     //Key to bring up the inventory
}

[Serializable]

class GameSaveData
{
    public int savenum;                                 //Save number
    public int LevelTrophyFlags1, LevelTrophyFlags2;    //Collected trophy flags
    /*
     trophy criteria goes here
     */
    public int Currency1, Currency2;                    //Currency amounts
    public string CurrentCharacter;                     //Current character.
    public short BillyXP;                               //Character XP amounts. 0 denotes character not yet unlocked.
    public short JillXP;
    public short RupertXP;
    public short JuniperXP;
    public short JoeXP;
    public short LeopoldXP;
    public int BillyUpgrades;                           //Character Upgrades/Skills. 4x byte amounts, one for basic and one for each of 3 abilities.
    public int JillUpgrades;
    public int RupertUpgrades;
    public int JuniperUpgrades;
    public int JoeUpgrades;
    public int LeopoldUpgrades;
    public int AreaFlags1, AreaFlags2, AreaFlags3;      //Area flags for level completion. Completing a level sets its flag, allows fast travel.
    public string CurrentLocation;                      //Location the game was saved.
    public Vector3 PosInLevel;                          //Location in that scene.

    public int PlayTime;                                //Amount of time played.
}

class GameOptionsData
{
    public float MasterVolume;                          //0.0f - 1.0f
    public float SoundsVolume;                          //0.0f - 1.0f;
    public float MusicVolume;                           //0.0f - 1.0f;
    public ControlScheme KbdControls;                   //Keyboard keycodes for all mappable buttons
    public ControlScheme ContControls;                  //Controller keycodes for all mappable buttons
}