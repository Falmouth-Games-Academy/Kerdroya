using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabledIfFound : MonoBehaviour
{
  public int levelIndex = 0;
  Button mainButton;
  void Start()
  {
    mainButton = GetComponent<Button>();
    if (AppProgression.levelCompleted[levelIndex] == true) mainButton.interactable = true;
  }
}
