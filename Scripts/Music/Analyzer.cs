using Godot;
using System;
using System.Collections.Generic;

public partial class Analyzer : Node2D
{
    [Export] private ColorRect _colorRect;

    private int _vuCount = 10;
    private float _freqMax = 20000.0f;
    private int _minDb = 60;

    private float _animationSpeed = 0.1f;
    private float _heightScale = 8.0f;

    private AudioEffectSpectrumAnalyzerInstance _analyzer;
    private Godot.Collections.Array _minValues = new();
    private Godot.Collections.Array _maxValues = new();

    public override void _Ready()
    {
        var effectInstance = AudioServer.GetBusEffectInstance(1, 0);
        _minValues.Resize(_vuCount+1);
        _maxValues.Resize(_vuCount+1);
        // Initialiser les valeurs à 0.0
        for (int i = 1; i < _vuCount+1; i++)
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
        var prevHz = 0.0f;
        Godot.Collections.Array data = new Godot.Collections.Array();

        for (int i = 1; i < _vuCount + 1; i++)
        {
            var hz = i * _freqMax / _vuCount;
            var f = _analyzer.GetMagnitudeForFrequencyRange(prevHz, hz);
            var energy = Mathf.Clamp((_minDb + Mathf.LinearToDb(f.Length())) / _minDb, 0, 1);
            data.Add(energy * _heightScale);
            prevHz = hz;
        }

        for (int i = 1; i < _vuCount + 1; i++)
        {
            float dataValue = (float)data[i-1];
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

        var mat = _colorRect.Material;
        (mat as ShaderMaterial)?.SetShaderParameter("freq_data", fft);
    }
}
