using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Ourhands : MonoBehaviour
{
    //pubic values to set in unity, private used on in script
    public GameObject OurHandPrefab;
    public InputDeviceCharacteristics ourControllerCharacteristics;

    private InputDevice ourDevice;
    private Animator ourHandAnimator;

    // Start is called before the first frame update
    void Start()
    {
        IntitializeOurHand();
    }

    void IntitializeOurHand()
    {
        //check for our controllerscharacteristics
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ourControllerCharacteristics, devices);

        //If Device identified, Instantiate a Hand
        if (devices.Count > 0)
        {
            ourDevice = devices[0];

            GameObject newHand = Instantiate(OurHandPrefab, transform);
            ourHandAnimator = newHand.GetComponent <Animator>();
        }
    }

    //Update is called once per frame
    void Update()
    {
    // Change Animate position or re-initialize
    if (ourDevice.isValid)
    {
        UpdateOurHand();
    }
    else
    {
         IntitializeOurHand();
    }
}
    void UpdateOurHand()
    {
          if (ourDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
    { 
       ourHandAnimator.SetFloat("Trigger", triggerValue);
    }
    else
    {
        ourHandAnimator.SetFloat("Trigger", 0);
        }

    if (ourDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
    {
       ourHandAnimator.SetFloat("Grip", gripValue);
    }
    else
    {
       ourHandAnimator.SetFloat("Grip", 0);
    }
  }
}
