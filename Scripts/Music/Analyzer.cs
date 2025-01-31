using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Range = System.Range;

public partial class Analyzer : Node2D
{
    [Export] private ColorRect _colorRect;

    private int _vuCountBass = 10;
    private int _vuCountMedium = 10;
    private int _vuCountTreble = 10;
    private int _vuCount = 0;
    private int[] _freqRangeBass = { 20, 200 };
    private int[] _freqRangeMedium = { 200, 2000 };
    private int[] _freqRangeTreble = { 2000, 20000 };
    private int _minDb = 60;

    private float _animationSpeed = 0.1f;
    private float _heightScale = 8.0f;

    private AudioEffectSpectrumAnalyzerInstance _analyzer;
    private Godot.Collections.Array _minValues = new();
    private Godot.Collections.Array _maxValues = new();

    public override void _Ready()
    {
        _vuCount = _vuCountBass + _vuCountMedium + _vuCountTreble;
        var effectInstance = AudioServer.GetBusEffectInstance(1, 0);
        _minValues.Resize(_vuCount + 1);
        _maxValues.Resize(_vuCount + 1);
        // Initialiser les valeurs à 0.0
        for (int i = 1; i < _vuCount + 1; i++)
        {
            _minValues[i] = 0.0;
            _maxValues[i] = 0.0;
        }

        // Vérifie si l'instance peut être castée en AudioEffectSpectrumAnalyzerInstance
        if (effectInstance is AudioEffectSpectrumAnalyzerInstance spectrumAnalyzer)
        {
            _analyzer = spectrumAnalyzer;
        }
        else
        {
            GD.PrintErr("L'effet à cet emplacement n'est pas un AudioEffectSpectrumAnalyzer.");
        }
    }

    public override void _Process(double delta)
    {
        Godot.Collections.Array data = new Godot.Collections.Array();
        data = GetSpectrumDataForSpecifiedRange(data, _vuCountBass, _freqRangeBass[0], _freqRangeBass[1]);
        data = GetSpectrumDataForSpecifiedRange(data, _vuCountMedium, _freqRangeMedium[0], _freqRangeMedium[1]);
        data = GetSpectrumDataForSpecifiedRange(data, _vuCountTreble, _freqRangeTreble[0], _freqRangeTreble[1]);
        for (int i = 1; i < _vuCount + 1; i++)
        {
            float dataValue = (float)data[i - 1];
            float maxValue = (float)_maxValues[i];
            float minValue = (float)_minValues[i];
            if (dataValue > maxValue)
            {
                _maxValues[i] = dataValue;
            }
            else
            {
                _maxValues[i] = Mathf.Lerp(maxValue, dataValue, _animationSpeed);
            }

            if (dataValue <= 0.0f)
            {
                _minValues[i] = Mathf.Lerp(minValue, 0.0f, _animationSpeed);
            }
        }

        var fft = new Godot.Collections.Array();
        for (int i = 1; i < _vuCount + 1; i++)
        {
            float maxValue = (float)_maxValues[i];
            float minValue = (float)_minValues[i];
            fft.Add(Mathf.Lerp(minValue, maxValue, _animationSpeed));
        }

        GetSignal(fft);

        var mat = _colorRect.Material;
        (mat as ShaderMaterial)?.SetShaderParameter("freq_data", fft);
    }

    private Godot.Collections.Array GetSpectrumDataForSpecifiedRange(Godot.Collections.Array data,int vuCount, int freqMin, int freqMax)
    {
        float prevHz = freqMin;

        for (int i = 1; i < vuCount + 1; i++)
        {
            var hz = i * freqMax / vuCount;
            var f = _analyzer.GetMagnitudeForFrequencyRange(prevHz, hz);
            var energy = Mathf.Clamp((_minDb + Mathf.LinearToDb(f.Length())) / _minDb, 0, 1);
            data.Add(energy * _heightScale);
            prevHz = hz;
        }
        return data;
    }

    private void GetSignal(Godot.Collections.Array data)
    {
        var indexBass = _vuCountBass;
        var indexMedium = indexBass + _vuCountMedium;
        var indexTreble = indexMedium + _vuCountTreble;
        var arrayBass = data[new Range(0, indexBass)];
        var arrayMedium = data[new Range(indexBass, indexMedium)];
        var arrayTreble = data[new Range(indexMedium, indexTreble)];
        var maxBass = arrayBass.Max();
        var maxMedium = arrayMedium.Max();
        var maxTreble = arrayTreble.Max();
        var averageBass = GetAverage(arrayBass);
        var averageMedium = GetAverage(arrayMedium);
        var averageTreble = GetAverage(arrayTreble);
        var countBass = 0;
        var countMedium = 0;
        var countTreble = 0;
        var thresholdBass = 0.5;
        var thresholdMedium = 0.5;
        var thresholdTreble = 0.2;
        for (int i = 0; i < indexBass; i++)
        {
            if (thresholdBass < data[i].AsDouble())
            {
                countBass ++;
            }
        }
        for (int i = indexBass; i < indexMedium; i++)
        {
            if (thresholdMedium < data[i].AsDouble())
            {
                countMedium ++;
            }
        }
        for (int i = indexMedium; i < indexTreble; i++)
        {
            if (thresholdTreble < data[i].AsDouble())
            {
                countTreble ++;
            }
        }


        GD.Print("Bass : " + countBass + " Sur :"+ _vuCountBass);
        GD.Print("Medium : " + countMedium + " Sur :"+ _vuCountMedium);
        GD.Print("Treble : " + countTreble + " Sur :"+ _vuCountTreble);
    }

    private float GetAverage(Godot.Collections.Array data)
    {
        var value = 0.0f;
        for (int i = 0; i < data.Count; i++)
        {
            value += data[i].AsInt16();
        }
        return value/data.Count;
    }
}