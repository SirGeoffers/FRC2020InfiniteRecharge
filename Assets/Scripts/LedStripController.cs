using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedStripController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer[] leds = null;
    private Material[] ledMaterials = null;

    [SerializeField]
    private Color colorOn = Color.white;

    [SerializeField]
    private Color colorOff = Color.black;

    private static int[,] nodePatterns1 =
    {
        { -1, -1, -1, -1, -1, -1 },
        { 14, 15, 16, 17, -1, -1 },
        { 11, 12, 13, 18, 19, 20 },
        {  9, 10, 21, 22, -1, -1 },
        {  7,  8, 23, 24, -1, -1 },
        {  5,  6, 25, 26, -1, -1 },
        {  4, 27, -1, -1, -1, -1 },
        {  3, 28, -1, -1, -1, -1 },
        {  2, 29, -1, -1, -1, -1 },
        {  1, 30, -1, -1, -1, -1 }
    };

    private static int[,] nodePatterns2_3 =
    {
        { -1, -1 },
        { 15, 16 },
        { 15, 16 },
        { 14, 17 },
        { 14, 17 },
        { 13, 18 },
        { 13, 18 },
        { 12, 19 },
        { 12, 19 },
        { 11, 20 },
        { 11, 20 },
        { 10, 21 },
        {  9, 22 },
        {  8, 23 },
        {  7, 24 },
        {  6, 25 },
        {  5, 26 },
        {  4, 27 },
        {  3, 28 },
        {  2, 29 },
        {  1, 30 }
    };

    private void Awake()
    {
        ledMaterials = new Material[leds.Length];
        for (int i = 0; i < leds.Length; i++)
        {
            ledMaterials[i] = leds[i].material;
        }

        Set(1, 0);
    }

    public void Set(int stage, int capacityProgress)
    {
        // Turn all off
        for (int i = 1; i <= 30; i++)
        {
            SetLed(i, false);
        }

        int maxCapacity = (stage == 1) ? 9 : 20;
        capacityProgress = Mathf.Min(capacityProgress, maxCapacity);

        int[,] pattern = (stage == 1) ? nodePatterns1 : nodePatterns2_3;
        int height = pattern.GetLength(0);
        int width = pattern.GetLength(1);
        
        // Turn specific lights on
        for (int i = 0; i <= capacityProgress; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int id = pattern[i, j];
                SetLed(id, true);
            }
        }
    }

    private void SetLed(int id, bool isOn)
    {
        if (id < 0) return;
        ledMaterials[id - 1].color = isOn ? colorOn : colorOff;
    }

    private void Start()
    {
        StartCoroutine(TestCR());
    }

    private IEnumerator TestCR()
    {
        int capacity = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            Set(2, capacity);
            capacity++;
        }
    }
}
