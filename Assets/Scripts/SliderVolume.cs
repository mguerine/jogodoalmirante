using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderVolume : MonoBehaviour {
    public static SliderVolume sliderInstance;
    public Slider slider;

    private void Awake() {
        // Singleton for instantiate game manager only once
        if (sliderInstance == null) {
            sliderInstance = this;
        }
    }

    // Start is called before the first frame update
    public void Start() {
        slider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnEnable() {
        slider.onValueChanged.AddListener(OnValueChanged);
      }
    void OnDisabled() { slider.onValueChanged.RemoveAllListeners(); }
    void OnDestroy() { slider.onValueChanged.RemoveAllListeners(); }

    public void OnValueChanged(float value) {
        slider.value = value;
        AudioListener.volume = value;
    }
}
