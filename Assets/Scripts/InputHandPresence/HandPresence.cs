﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    public List<GameObject> controllerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices) {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0) {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controllerPrefabs => controllerPrefabs.name == targetDevice.name);
            if(prefab) {
                spawnedController = Instantiate(prefab, transform);
            }else {
                Debug.LogError("Did not find corresponding controller prefab");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue) {
            Debug.Log("Pressing Primary Button");
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.01f) {
            Debug.Log("Trigger pressed " + triggerValue);
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero) {
            Debug.Log("Primary Touchpad " + primary2DAxisValue);

        }

    }
}
