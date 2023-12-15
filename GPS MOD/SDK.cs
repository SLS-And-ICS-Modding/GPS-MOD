using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GPS_MOD
{
    internal class SDK
    {
        public static CarManager carManager = null;
        public static PlayerController playerController = null;
        public static PlayerCamera PlayerCamera = null;
        public static Camera MainCamera => UnityEngine.Camera.main;
        public static bool IsInGame => CarManager.Instance != null;
        public static GameObject BeginnerCar => carManager.dadyCar;
        public static GameObject PorscheCar => carManager.porscheCar;
        public static GameObject JapaneseCar => carManager.japaneseCar;
        public static GameObject VanCar => carManager.vanCar;
        public static GameObject SuvCar => carManager.suvCar;
        

        
    }
}

