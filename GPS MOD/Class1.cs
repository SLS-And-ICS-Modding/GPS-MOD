using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Michsky.UI.ModernUIPack.UIManagerButton;

namespace GPS_MOD
{
    public class Class1:MelonMod
    {
        Dictionary<string, Vector3> ItemPos = new Dictionary<string, Vector3>
            {
                        {"House 1", new Vector3(1304.0f, 29.4f, 1851.1f)},
                        {"House 2", new Vector3(961.4f, 20.9f, 955.2f)},
                        {"House 3", new Vector3(330.1f, 19.2f, 1270.8f)},
                        {"House 4", new Vector3(298.1f, 18.2f, 318.9f)},
                        {"House 5", new Vector3(305.7f, 20.8f, 262.6f)},
                        {"Car Dealership", new Vector3(330.4f, 19.7f, 1506.0f)},
                        {"ZUber Job", new Vector3(436.7f, 19.2f, 1218.1f)},
                        {"Delivery Job", new Vector3(323.5f, 19.2f, 1233.6f)},
                        {"Pet Shop(Dogs)", new Vector3(415.2f, 19.2f, 1198.1f)},
                        {"BTC Miner Shop", new Vector3(315.5f, 19.2f, 1347.6f)},
                        {"Pizza Job", new Vector3(322.5f, 19.2f, 1430.6f)},
                        {"Taxi Job", new Vector3(364.3f, 18.9f, 1469.4f)},
                        {"ATM1", new Vector3(375.0f, 19.2f, 1323.2f)},
                        {"Cashier Job", new Vector3(370.9f, 19.2f, 1319.1f)},
                        {"Ice Cream Guy", new Vector3(1311.9f, 28.3f, 1851.9f)},
                        {"Pet Shop (Cats)", new Vector3(1310.3f, 28.3f, 1811.9f)},
                        {"Dealer Guy", new Vector3(1346.5f, 28.5f, 1803.4f)},
                        {"Mosi House", new Vector3(1328.7f, 22.8f, 849.0f)},
                        /*{"Dady Car(this is how dev called it)(starter car)", SDK.BeginnerCar.transform.position },
                        {"Japanese Car", SDK.JapaneseCar.transform.position },
                        { "Van", SDK.VanCar.transform.position},  temporary removed due to crashing mod on load
                        { "Suv", SDK.SuvCar.transform.position},
                        {" Porsche", SDK.PorscheCar.transform.position },*/
                        { "Gas Station 1", new Vector3(722.7f, 22.2f, 522.7f) },
                        { "Gas Station 2", new Vector3(434.8f, 19.0f, 1158.9f)}
            };
        public static Texture2D lineTex;
        KeyCode MENU_KEY = KeyCode.Insert;
        bool bMenu = false;
        string curdest = "";
        bool UseNav = false;
        GameObject lineObject = new GameObject("LineObject");
        LineRenderer lineRenderer = null; // im unsure if it works(at least when im making this)
        static int CallsOnGUI = 0;
        static int CallsOnUpdate = 0;
        public override void OnApplicationStart()
        {
            lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Standard"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.blue;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
        public override void OnApplicationQuit()
        {
            
        }
        public override void OnGUI()
        {
            if (SDK.carManager == null)
                return;
            
            if (bMenu)
            {
                
                foreach (var dest in ItemPos)
                {
                    if (GUILayout.Button($"Navigate To {dest.Key}"))
                    {
                        UseNav = true;
                        curdest = dest.Key;
                    }
                }
            }
            if(UseNav == true && curdest != "")
            {

                // drawing line
                // doing some timeout to not make 2 milions calls per sec, we don't want to fry our cpu(at least me)
                if (CallsOnGUI >= 200)
                {
                    Vector3 w2s = SDK.MainCamera.WorldToScreenPoint(FindPosByString(curdest, ItemPos));
                    CallsOnGUI = 0;
                    Vector3[] positions = new Vector3[2];
                    positions[0] = new Vector2(Screen.width / 2, Screen.height);
                    positions[1] = new Vector2(w2s.x, Screen.height - w2s.y);
                    lineRenderer.positionCount = positions.Length;
                    lineRenderer.SetPositions(positions);
                }
                else
                {
                    CallsOnGUI++;
                }






            }
        }
        public Vector3 FindPosByString(string name, Dictionary<string, Vector3> dict)
        {
            if (dict.ContainsKey(name))
            {
                return dict[name];
            }
            else
            {
                // this shouldn't really happen but just in case
                WarningTipManager.Instance.ShowWarning($"An error occured, FindPosByString failed with value {name}\n report this in discord ticket");
                return Vector3.zero;
            }
        }
        public override void OnUpdate()
        {
            if(Input.GetKeyDown(MENU_KEY))
            {
                bMenu = !bMenu;
            }
            // doing this also to not turn our toaster into an chernobyl reactor
            if(CallsOnUpdate >= 200)
            {
                if(SDK.IsInGame)
                {
                    SDK.carManager = UnityEngine.Object.FindObjectOfType<CarManager>();
                    SDK.playerController = UnityEngine.Object.FindObjectOfType<PlayerController>();
                    SDK.PlayerCamera = UnityEngine.Object.FindObjectOfType<PlayerCamera>();
                }
                CallsOnUpdate = 0;
            } else
            {
                CallsOnUpdate++;
            }
                
        }
    }
}
