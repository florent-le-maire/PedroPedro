using System;
using Godot;

namespace PedroBulletV2.Scripts.Pattern;

public partial class PatternManager : Node2D
{
	[Export] private Analyzer _analyzer;
	[Export] private Pattern _pattern;
	private bool _triggerBass;
	private Timer _timerBass;
	private bool _triggerMedium;
	private Timer _timerMedium;
	private bool _triggerTreble;
	private Timer _timerTreble;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timerBass = new Timer();
		_timerMedium = new Timer();
		_timerTreble = new Timer();
		AddChild(_timerBass);
		AddChild(_timerMedium);
		AddChild(_timerTreble);
		if (_pattern == null  || _analyzer == null)
		{
			throw new Exception("Null pattern or analyser"); 
		}
		_analyzer.AnalyzerSignal += GetSignal;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	
	void GetSignal(int countBass,int countMedium,int countTreble)
	{
		_triggerBass = false;
		_triggerMedium = false;
		_triggerTreble = false;
		if (countBass > 5)
		{
			_triggerBass = true;
		}
		if (countMedium > 0)
		{
			_triggerMedium = true;
		}
		if (countTreble > 5)
		{
			_triggerTreble = true;
		}
	}

	public void CreatePattern(int measure)
	{
		if (_triggerBass && _timerBass.TimeLeft <= 0)
		{
			GD.Print("Bass",_timerBass.TimeLeft);
			var duration = 2;
			_pattern.GenerateCirclePattern(new PatternSchema("CirclePulse", 1, 100, 30, duration, 0.1f, 0, 1));
			_timerBass.WaitTime = duration;
			_timerBass.OneShot = true;
			_timerBass.Start();
		}
		if (_triggerMedium && _timerMedium.TimeLeft <= 0)
		{
			GD.Print("Medium",_timerMedium.TimeLeft);
			var durationMedium = 20;
			_pattern.GenerateCirclePattern(new PatternSchema("RotationMove", 1, 75, 30, durationMedium, 0.5f, 200, 1));
			_timerMedium.WaitTime = durationMedium;
			_timerMedium.OneShot = true;
			_timerMedium.Start();
		}
		if (_triggerTreble && _timerTreble.TimeLeft <= 0)
		{
			GD.Print("Treble",_timerTreble.TimeLeft);

			var durationTreble = 3;
			_pattern.GenerateCirclePattern(new PatternSchema("Trumpet", 1, 50, 1, 3, 0.01f, 360, -1));
			_timerTreble.WaitTime = durationTreble;
			_timerTreble.OneShot = true;
			_timerTreble.Start();
			
		}
	}
}